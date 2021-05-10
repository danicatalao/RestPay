using MongoDB.Driver;
using RestPay.Models;
using System;
using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly MongoClient _client;

		private readonly IMongoDatabase _database;

		private readonly IMongoCollection<Transaction> _transactions;

		private readonly IMongoCollection<User> _users;

		public TransactionRepository(IMongoDBSettings settings)
		{
			_client = new MongoClient(settings.ConnectionString);
			_database = _client.GetDatabase(settings.DatabaseName);
			_transactions = _database.GetCollection<Transaction>(settings.TransactionCollectionName);
			_users = _database.GetCollection<User>(settings.UserCollectionName);
		}

		public async Task<bool> TransferAsync(string payerId, string payeeId, decimal value)
		{
			using var session = await _client.StartSessionAsync();
			session.StartTransaction();
			try
			{
				if (IsWithdrawAllowed(payerId, value))
				{
					var withdrawResult = await _users.UpdateOneAsync(session, user => user.Id == payerId, Builders<User>.Update.Inc<Decimal>("Wallet", -value));
					var depositResult = await _users.UpdateOneAsync(session, user => user.Id == payeeId, Builders<User>.Update.Inc<Decimal>("Wallet", value));
					var isPayerWalletValid = IsWalletValid(session, payerId);
					await InsertTransaction(payerId, payeeId, value);
					if (!withdrawResult.IsAcknowledged || !depositResult.IsAcknowledged || !isPayerWalletValid)
					{
						throw new Exception();
					}
				}
				else
				{
					throw new Exception();
				}
			}
			catch (Exception /*e*/)
			{
				await session.AbortTransactionAsync();
				return false;
			}
			await session.CommitTransactionAsync();
			return true;
		}

		private async Task InsertTransaction(string from, string to, decimal value)
		{
			var transaction = new Transaction { PayerId = from, PayeeId = to, Value = value, DateTime = DateTime.UtcNow };
			await _transactions.InsertOneAsync(transaction);
		}

		private bool IsWithdrawAllowed(string id, decimal value)
		{
			var balance = _users.Find(user => user.Id == id).First().Wallet;
			return balance >= value;
		}

		private bool IsWalletValid(IClientSessionHandle session, string id)
		{
			var balance = _users.Find(session, user => user.Id == id).First().Wallet;
			return balance >= 0;
		}
	}
}
