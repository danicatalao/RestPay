using RestPay.Models;
using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public interface IUserRepository
	{
		public NormalPerson GetNormalPerson(string id);

		public LegalPerson GetLegalPerson(string id);
	}
}
