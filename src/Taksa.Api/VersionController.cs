using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Taksa.Api
{
	public class VersionController : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		[Route("")]
		[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
		public IActionResult GetCurrentApiVersion()
		{
			return Ok(Assembly.GetExecutingAssembly().GetName().Version.ToString());
		}
	}
}
