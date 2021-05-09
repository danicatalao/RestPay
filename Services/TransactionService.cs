
using RestPay.Dtos;
using RestPay.Models;
using RestPay.Repositories;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly IUserRepository _userRepository;

		private readonly string _transactionAuthenticatorApi;

		public TransactionService(IUserRepository userRepository, ITransactionAuthenticatorSettings transactionAuthenticatorSettings)
		{
			_userRepository = userRepository;
			_transactionAuthenticatorApi = transactionAuthenticatorSettings.ApiUrl;
		}

		public Task<bool> Transfer(TransactionDto transaction)
		{
			return _userRepository.TransferAsync(transaction.Payer, transaction.Payee, transaction.Value);
		}

		private bool AuthenticateTransaction(User payer, User payee, decimal value)
		{

			return false;
		}

	}
}
