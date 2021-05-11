using RestPay.Dtos;
using RestPay.Models;
using RestPay.Repositories;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IUserRepository _userRepository;
		private readonly string _transactionAuthenticatorApi;
		private readonly IHttpClientFactory _clientFactory;

		private const string AUTHENTICATE_SUCCESS_MESSAGE = "Autorizado";

		public TransactionService
		(
			ITransactionRepository transactionRepository, 
			IUserRepository userRepository, 
			ITransactionAuthenticatorSettings transactionAuthenticatorSettings, 
			IHttpClientFactory clientFactory
		)
		{
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
			_transactionAuthenticatorApi = transactionAuthenticatorSettings.ApiUrl;
			_clientFactory = clientFactory;
		}

		public async Task<bool> Transfer(TransactionDto transaction)
		{
			var isTransactionAuthorized = await AuthenticateTransactionAsync(transaction.Payer, transaction.Payee, transaction.Value);
			var isPayerValid = IsPayerValidAsync(transaction.Payer);
			var isPayeeValid = IsPayeeValid(transaction.Payee);
			if (isTransactionAuthorized && isPayerValid && isPayeeValid)
			{
				var transactionCommited = await _userRepository.TransferAsync(transaction.Payer, transaction.Payee, transaction.Value);
				if (transactionCommited)
				{
					try
					{
						await _transactionRepository.InsertTransaction(transaction.Payer, transaction.Payee, transaction.Value);
					}
					catch(Exception /*e*/)
					{}
				}
				return transactionCommited;
			}
			else
			{
				return false;
			}
		}

		private async Task<bool> AuthenticateTransactionAsync(string payer, string payee, decimal value)
		{
			var response = await PostAuthenticator(payer, payee, value);
			if (response.IsSuccessStatusCode)
			{
				using var responseStream = await response.Content.ReadAsStreamAsync();
				var responseDto = await JsonSerializer.DeserializeAsync<TransactionAuthenticationDto>(responseStream);
				if (responseDto.message.Equals(AUTHENTICATE_SUCCESS_MESSAGE))
				{
					return true;
				}
			}
			return false;
		}

		private async Task<HttpResponseMessage> PostAuthenticator(string payer, string payee, decimal value)
		{
			var body = new StringContent
			(
				JsonSerializer.Serialize(new TransactionDto { Payer = payer, Payee = payee, Value = value }),
				Encoding.UTF8,
				"application/json"
			);
			var client = _clientFactory.CreateClient();
			return await client.PostAsync(_transactionAuthenticatorApi, body);
		}

		private bool IsPayerValidAsync(string id)
		{
			var user = _userRepository.GetNormalPerson(id);
			return !(user is null);
		}

		private bool IsPayeeValid(string Payee)
		{
			return true;
		}
	}
}
