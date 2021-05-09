using RestPay.Models;
using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public interface IUserRepository
	{
		public Task<bool> TransferAsync(string payerId, string payeeId, decimal value);
	}
}
