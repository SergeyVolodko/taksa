﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taksa.Api.Contracts;
using Taksa.Domain.Bribes;

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

		[HttpPost]
		//[Authorize]
		[Route("publish")]
		public Task<IActionResult> PublishBribe([FromBody]
			BribeCommands.V1.Publish request) =>
			HandleOrThrow(request, x => service.Handle(x));

		private async Task<IActionResult> HandleOrThrow<T>(T request, Func<T, Task> handler)
		{
			try
			{
				await handler(request);
				return Ok();
			}
			catch (Exceptions.BribeNotFoundException)
			{
				return NotFound();
			}
		}
	}
}