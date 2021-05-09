
using RestPay.Dtos;
using RestPay.Models;
using RestPay.Repositories;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly string _transactionAuthenticatorApi;

		public TransactionService(ITransactionRepository transactionRepository , ITransactionAuthenticatorSettings transactionAuthenticatorSettings)
		{
			_transactionRepository = transactionRepository;
			_transactionAuthenticatorApi = transactionAuthenticatorSettings.ApiUrl;
		}

		public async Task<bool> Transfer(TransactionDto transaction)
		{
			return await _transactionRepository.TransferAsync(transaction.Payer, transaction.Payee, transaction.Value);
		}

		private bool AuthenticateTransaction(User payer, User payee, decimal value)
		{

			return false;
		}

	}
}
