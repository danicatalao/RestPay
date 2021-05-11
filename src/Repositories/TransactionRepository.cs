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

		public TransactionRepository(IMongoDBSettings settings)
		{
			_client = new MongoClient(settings.ConnectionString);
			_database = _client.GetDatabase(settings.DatabaseName);
			_transactions = _database.GetCollection<Transaction>(settings.TransactionCollectionName);
		}
		
		public async Task InsertTransaction(string from, string to, decimal value)
		{
			var transaction = new Transaction { PayerId = from, PayeeId = to, Value = value, DateTime = DateTime.UtcNow };
			await _transactions.InsertOneAsync(transaction);
		}
	}
}
