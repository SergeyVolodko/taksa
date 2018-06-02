using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taksa.Api.Contracts;

namespace Taksa.Api.BribesApi
{
	[Route("/bribes")]
	public class BribeCommandsController : Controller
	{
		private readonly IBribeService service;

		public BribeCommandsController(
			IBribeService service)
		{
			this.service = service;
		}

		[HttpPost]
		[Route("")]
		public Task CreateBribe([FromBody]
			BribeCommands.V1.Create createCommand)
		{
			return service.Handle(createCommand);
		}
	}
}