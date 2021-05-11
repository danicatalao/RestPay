using RestPay.Models;
using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public interface IUserRepository
	{
		public string GetUserEmail(string id);
		public NormalPerson GetNormalPerson(string id);
		public LegalPerson GetLegalPerson(string id);
		public void MockUsers();
	}
}
