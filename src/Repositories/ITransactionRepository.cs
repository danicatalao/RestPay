using System.Threading.Tasks;

namespace RestPay.Repositories
{
	public interface ITransactionRepository
	{
		public Task<bool> TransferAsync(string payerId, string payeeId, decimal value);
	}
}
