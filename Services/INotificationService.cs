using System.Threading.Tasks;

namespace RestPay.Services
{
	public interface INotificationService
	{
		public Task<bool> DispatchDepositNotification(string payee, decimal value);
	}
}
