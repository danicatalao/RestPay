using Microsoft.AspNetCore.Mvc;
using RestPay.Dtos;
using RestPay.Services;
using System.Threading.Tasks;

namespace RestPay.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		private readonly INotificationService _notificationService;

		public TransactionController
		(
			ITransactionService transactonService,
			INotificationService notificationService
		)
		{	
			_transactionService = transactonService;
			_notificationService = notificationService;
		}

		[HttpPost]
		public async Task<IActionResult> Transaction([FromBody] TransactionDto transaction)
		{
			var success = await _transactionService.Transfer(transaction);
			if (success)
			{
				await _notificationService.DispatchDepositNotification(transaction.Payee, transaction.Value);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
