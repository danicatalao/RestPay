using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestPay.Dtos;
using RestPay.Models;
using RestPay.Services;
using System;
using System.Threading.Tasks;

namespace RestPay.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly ILogger<TransactionController> _logger;

		private readonly ITransactionService _transactionService;

		public TransactionController(ILogger<TransactionController> logger, ITransactionService transactonService)
		{
			_logger = logger;
			_transactionService = transactonService;
		}

		[HttpPost]
		public async Task<IActionResult> Transaction([FromBody] TransactionDto transaction)
		{
			var success = await _transactionService.Transfer(transaction);
			if (success)
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
