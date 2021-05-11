
using MongoDB.Driver;
using RestPay.Repositories;

namespace RestPay.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public bool MockUsers()
		{
			try
			{
				_userRepository.MockUsers();
				return true;
			}
			catch (MongoException /*ex*/)
			{
				return false;
			}
		}
	}
}
