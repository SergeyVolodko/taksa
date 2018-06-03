using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Taksa.Api.Projections;

namespace Taksa.Api.BribesApi
{
	//[Produces("application/json")]
	public class BribeReadController : Controller
	{
		[HttpGet]
		[Route("bribes/published")]
		public IList<BribeDocument> GetPublishedBribes()
		{
			return new List<BribeDocument>();
		}

		[HttpGet]
		[Route("bribes/created")]
		public IList<BribeDocument> GetCreatedBribes()
		{
			return new List<BribeDocument>();
		}
	}
}