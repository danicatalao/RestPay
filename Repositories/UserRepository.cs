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
	}
}
