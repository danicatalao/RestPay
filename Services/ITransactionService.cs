using RestPay.Dtos;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public interface ITransactionService
	{
		public Task<bool> Transfer(TransactionDto transaction);
	}
}
