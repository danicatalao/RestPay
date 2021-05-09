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

		private readonly IMongoCollection<User> _users;

		public UserRepository(IRestPayDatabaseSettings settings)
		{
			_client = new MongoClient(settings.ConnectionString);
			_database = _client.GetDatabase(settings.DatabaseName);
			_users = _database.GetCollection<User>(settings.UserCollectionName);
		}

		public NormalPerson GetNormalPerson(string id)
		{
			var query = _users.AsQueryable<User>().OfType<NormalPerson>().Where(user => user.Id == id);
			var a = query.ToList();
			return query.FirstOrDefault();
		}

		public LegalPerson GetLegalPerson(string id)
		{
			var query = _users.AsQueryable<User>().OfType<LegalPerson>().Where(user => user.Id == id);
			var a = query.ToList();
			return query.FirstOrDefault();
		}
	}
}
