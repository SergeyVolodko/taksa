using System;
using System.Threading.Tasks;
using Taksa.Api.Contracts;
using Taksa.Domain.Bribes;
using Taksa.Framework;

namespace Taksa.Api.BribesApi
{
	public interface IBribeService
	{
		Task Handle(BribeCommands.V1.Create command);
		Task Handle(BribeCommands.V1.Publish command);
	}

	public class BribeService : IBribeService
	{
		private readonly IAggregateStore store;

		public BribeService(IAggregateStore store)
		{
			this.store = store;
		}

		public Task Handle(BribeCommands.V1.Create command)
		{
			var bribe = Bribe.Create(
				(ServiceName)command.service_name,
				(BribeAmount)command.amount,
				(BribeTimestamp)command.timestamp);

			return store.Save(bribe);
		}

		public Task Handle(BribeCommands.V1.Publish command) =>
			HandleUpdate(command.bribe_id, bribe =>
				bribe.Publish(
					new ServiceCategory(command.category_local, command.category_international),
					DateTimeOffset.UtcNow)
			);

		private async Task HandleUpdate(Guid id, Action<Bribe> update)
		{
			var ad = await store.Load<Bribe>(id.ToString());
			update(ad);
			await store.Save(ad);
		}
	}
}
