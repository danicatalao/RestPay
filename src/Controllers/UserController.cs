using Microsoft.AspNetCore.Mvc;
using RestPay.Services;

namespace RestPay.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController
		(
			IUserService userService
		)
		{
			_userService = userService;
		}

		[HttpPost]
		[Route("Mockusers")]
		public IActionResult MockUsers()
		{
			if(_userService.MockUsers())
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
