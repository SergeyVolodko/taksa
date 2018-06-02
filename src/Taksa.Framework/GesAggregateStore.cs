using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using EventStore.ClientAPI.SystemData;

namespace Taksa.Framework
{
	using static String;

	public class GesAggregateStore : IAggregateStore
	{
		private const int MaxReadSize = 4096;

		//private static readonly ILog Log = LogProvider.For<GesAggregateStore>();
		private readonly IEventStoreConnection connection;

		private readonly GetStreamName getStreamName;
		private readonly ISerializer serializer;
		private readonly TypeMapper typeMapper;
		private readonly UserCredentials userCredentials;

		public GesAggregateStore(
			GetStreamName getStreamName,
			IEventStoreConnection connection,
			ISerializer serializer,
			TypeMapper typeMapper,
			UserCredentials userCredentials = null)
		{
			this.getStreamName = getStreamName ?? throw new ArgumentNullException(nameof(connection));
			this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
			this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
			this.typeMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));
			this.userCredentials = userCredentials;
		}

		/// <summary>
		///     Loads and returns an aggregate by id, from the store.
		/// </summary>
		public async Task<T> Load<T>(string aggregateId, CancellationToken cancellationToken = default)
			where T : Aggregate, new()
		{
			if (IsNullOrWhiteSpace(aggregateId))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(aggregateId));

			var stream = getStreamName(typeof(T), aggregateId);
			var aggregate = new T();

			var nextPageStart = 0L;
			do
			{
				var page = await connection.ReadStreamEventsForwardAsync(
					stream, nextPageStart, MaxReadSize, false, userCredentials);

				aggregate.Load(page.Events.Select(resolvedEvent =>
				{
					var dataType = typeMapper.GetType(resolvedEvent.Event.EventType);
					var data = serializer.Deserialize(resolvedEvent.Event.Data, dataType);
					return data;
				}).ToArray());

				nextPageStart = !page.IsEndOfStream ? page.NextEventNumber : -1;
			} while (nextPageStart != -1);

			//Log.Info("Loaded {aggregate} changes from stream {stream}", aggregate, stream);

			return aggregate;
		}

		/// <summary>
		///     Saves changes to the store.
		/// </summary>
		public async Task<(long NextExpectedVersion, long LogPosition, long CommitPosition)> Save<T>(
			T aggregate, CancellationToken cancellationToken = default)
			where T : Aggregate
		{
			if (aggregate == null)
				throw new ArgumentNullException(nameof(aggregate));

			var changes = aggregate.GetChanges()
				.Select(e => new EventData(
					Guid.NewGuid(),
					typeMapper.GetTypeName(e.GetType()),
					serializer.IsJsonSerializer,
					serializer.Serialize(e),
					null))
				.ToArray();

			if (!changes.Any())
			{
				//Log.Warn("{Id} v{Version} aggregate has no changes.", aggregate.Id, aggregate.Version);
				return default;
			}

			var stream = getStreamName(typeof(T), aggregate.Id.ToString());

			WriteResult result;
			try
			{
				result = await connection.AppendToStreamAsync(stream, aggregate.Version, changes, userCredentials);
			}
			catch (WrongExpectedVersionException)
			{
				var page = await connection.ReadStreamEventsBackwardAsync(stream, -1, 1, false, userCredentials);
				throw new WrongExpectedStreamVersionException(
					$"Failed to append stream {stream} with expected version {aggregate.Version}. " +
					$"{(page.Status == SliceReadStatus.StreamNotFound ? "Stream not found!" : $"Current Version: {page.LastEventNumber}")}");
			}

			//Log.Info("Saved {aggregate} changes into stream {streamName}", aggregate, stream);

			return (
				result.NextExpectedVersion,
				result.LogPosition.CommitPosition,
				result.LogPosition.PreparePosition);
		}

		/// <summary>
		///     Returns the last version of the aggregate, if found.
		/// </summary>
		public async Task<long> GetLastVersionOf<T>(string aggregateId, CancellationToken cancellationToken = default)
			where T : Aggregate
		{
			if (IsNullOrWhiteSpace(aggregateId))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(aggregateId));

			var page = await connection.ReadStreamEventsBackwardAsync(
				getStreamName(typeof(T), aggregateId), long.MaxValue, 1, false, userCredentials);

			return page.LastEventNumber;
		}
	}
}