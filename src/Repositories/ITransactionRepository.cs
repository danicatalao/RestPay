using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public interface ITransactionRepository
	{
		public Task InsertTransaction(string from, string to, decimal value);
	}
}
