
using RestPay.Dtos;
using RestPay.Models;
using RestPay.Repositories;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly string _transactionAuthenticatorApi;

		public TransactionService(IUserRepository userRepository, ITransactionRepository transactionRepository , ITransactionAuthenticatorSettings transactionAuthenticatorSettings)
		{
			_userRepository = userRepository;
			_transactionRepository = transactionRepository;
			_transactionAuthenticatorApi = transactionAuthenticatorSettings.ApiUrl;
		}

		public async Task<bool> Transfer(TransactionDto transaction)
		{
			var transferSuccess = await _userRepository.TransferAsync(transaction.Payer, transaction.Payee, transaction.Value);
			if (transferSuccess)
			{
				await _transactionRepository.InsertTransaction(transaction.Payer, transaction.Payee, transaction.Value);
				return true;
			}
			return false;
		}

		private bool AuthenticateTransaction(User payer, User payee, decimal value)
		{

			return false;
		}

	}
}
