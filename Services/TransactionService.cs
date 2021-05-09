
using RestPay.Dtos;
using RestPay.Models;
using RestPay.Repositories;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IUserRepository _userRepository;
		private readonly string _transactionAuthenticatorApi;

		public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ITransactionAuthenticatorSettings transactionAuthenticatorSettings)
		{
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
			_transactionAuthenticatorApi = transactionAuthenticatorSettings.ApiUrl;
		}

		public async Task<bool> Transfer(TransactionDto transaction)
		{
			var isPayerValid = IsPayerValidAsync(transaction.Payer);
			var isPayeeValid = IsPayeeValid(transaction.Payee);
			if (isPayerValid && isPayeeValid)
			{
				return await _transactionRepository.TransferAsync(transaction.Payer, transaction.Payee, transaction.Value);
			}
			else
			{
				return false;
			}
		}

		private bool AuthenticateTransaction(User payer, User payee, decimal value)
		{
			
			return false;
		}

		private bool IsPayerValidAsync(string id)
		{
			var user = _userRepository.GetNormalPerson(id);
			if (user is null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		private bool IsPayeeValid(string Payee)
		{
			return true;
		}
	}
}
