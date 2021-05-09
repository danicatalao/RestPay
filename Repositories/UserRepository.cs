using MongoDB.Bson;
using MongoDB.Driver;
using RestPay.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly MongoClient _client;

		private readonly IMongoDatabase _database;

		private readonly IMongoCollection<NormalPerson> _normalPersons;

		private readonly IMongoCollection<LegalPerson> _legalPersons;

		private readonly IMongoCollection<User> _users;

		public UserRepository(IRestPayDatabaseSettings settings)
		{
			_client = new MongoClient(settings.ConnectionString);
			_database = _client.GetDatabase(settings.DatabaseName);
			_normalPersons = _database.GetCollection<NormalPerson>(settings.UserCollectionName);
			_legalPersons = _database.GetCollection<LegalPerson>(settings.UserCollectionName);
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
					var isPayerWalletValid = IsWalletValid(payerId);
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
			catch (Exception e)
			{
				var a = e.ToString();
				await session.AbortTransactionAsync();
				return false;
			}
			await session.CommitTransactionAsync();
			return true;
		}

		private bool IsWithdrawAllowed(string id, decimal value)
		{
			var balance = _users.Find(user => user.Id == id).First().Wallet;
			return balance >= value;
		}

		private bool IsWalletValid(string id)
		{
			var balance = _users.Find(user => user.Id == id).First().Wallet;
			return balance >= 0;
		}

		public decimal? GetWalletById(string id)
		{
			return _users.Find(user => user.Id == id).FirstOrDefault()?.Wallet;
		}
	}
}
