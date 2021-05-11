using RestPay.Models;
using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public interface IUserRepository
	{
		public Task<bool> TransferAsync(string payerId, string payeeId, decimal value);
		public string GetUserEmail(string id);
		public NormalPerson GetNormalPerson(string id);
		public LegalPerson GetLegalPerson(string id);
		public void MockUsers();
	}
}
