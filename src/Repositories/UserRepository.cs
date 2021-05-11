using MongoDB.Driver;
using RestPay.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestPay.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly MongoClient _client;

		private readonly IMongoDatabase _database;

		private readonly IMongoCollection<User> _users;

		public UserRepository(IUserProviderSettings settings)
		{
			_client = new MongoClient(settings.ConnectionString);
			_database = _client.GetDatabase(settings.DatabaseName);
			_users = _database.GetCollection<User>(settings.UserCollectionName);
		}

		public string GetUserEmail(string id)
		{
			return _users.Find(user => user.Id == id).First().Email;
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

		public void MockUsers()
		{
			CreateIndexes();
			var userList = new List<User>
			{
				new NormalPerson()
				{
					Name = "Red Guy",
					Cpf = "12345678911",
					Password = "$2y$12$1x//ASzyeZ0Tegxk3eObe.zsEjE4pfpVF/pzyYn9SEeEjz5UrBhBa",
					Email = "redguy@gmail.com",
					Wallet = new decimal(5000.00)
				},
				new NormalPerson()
				{
					Name = "Yellow Guy",
					Cpf = "12345678912",
					Password = "$2y$12$/1HVewZglsliW9FLoP0.jeLQNkF1K0CWU1HdV7OqLDfotr6ELkrBO",
					Email = "yellowguy@gmail.com",
					Wallet = new decimal(5000.00)
				},
				new NormalPerson()
				{
					Name = "Duck",
					Cpf = "12345678913",
					Password = "$2y$12$tjQiNdcnQvj3k2RWM5VNY.dLe4sEBNcBwUOox/q76jjkDy.jm2ZBC",
					Email = "duck@gmail.com",
					Wallet = new decimal(5000.00)
				},
				new LegalPerson()
				{
					Name = "Tony the Clock",
					Cnpj = "12345678912345",
					Password = "$2y$12$BrUGsxFcQepDqFctxVEx6eR5NRyNM28p8LaX8jLLg/kDlnergnqrS",
					Email = "tonytheclock@gmail.com",
					Wallet = new decimal(20000.00)
				},
				new LegalPerson()
				{
					Name = "Colin the Computer",
					Cnpj = "12345678912346",
					Password = "$2y$12$WU/MTfVJ2AM8/.zhG/NJq.QK7j5UryAtPgzvQZs9fTLN0pdeiMFCC",
					Email = "colinthecomputer@gmail.com",
					Wallet = new decimal(20000.00)
				},
				new LegalPerson()
				{
					Name = "Sketchbook",
					Cnpj = "123456789123457",
					Password = "$2y$12$rv87/eQpjh.mJsqSVrW7he3cTPRl0MAFQ/XN3zqRx5N.uOT/Jzz0y",
					Email = "sketchbook@gmail.com",
					Wallet = new decimal(20000.00)
				}
			};
			_users.InsertMany(userList);
		}

		private void CreateIndexes()
		{
			var emailKey = Builders<User>.IndexKeys.Ascending("email");
			var socialNumberkeys = Builders<User>.IndexKeys.Ascending("cpf").Ascending("cnpj");
			var indexOptions = new CreateIndexOptions { Unique = true };
			var modelList = new List<CreateIndexModel<User>>
			{
				new CreateIndexModel<User>(emailKey, indexOptions),
				new CreateIndexModel<User>(socialNumberkeys, indexOptions)
			};
			_users.Indexes.CreateMany(modelList);
		}
	}
}
