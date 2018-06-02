using System.Threading.Tasks;
using Taksa.Api.Contracts;
using Taksa.Domain.Bribes;
using Taksa.Framework;

namespace Taksa.Api.BribesApi
{
	public interface IBribeService
	{
		Task Handle(BribeCommands.V1.Create command);
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
				(BribeName)command.service_name,
				(BribeAmount)command.amount,
				(BribeTimestamp)command.timestamp);

			return store.Save(bribe);
		}
	}
}
