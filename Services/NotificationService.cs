using RestPay.Dtos;
using RestPay.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestPay.Services
{
	public class NotificationService : INotificationService
	{
		private readonly string _notificationApi;
		private readonly IHttpClientFactory _clientFactory;

		private const string NOTIFICATION_SUCCESS_MESSAGE = "Success";

		public NotificationService
		(
			INotificationSettings notificationApi,
			IHttpClientFactory clientFactory
		)
		{
			_notificationApi = notificationApi.ApiUrl;
			_clientFactory = clientFactory;
		}

		public async Task<bool> DispatchDepositNotification(string payee, decimal value)
		{
			var response = await PostNotification(payee, value);
			if (response.IsSuccessStatusCode)
			{
				using var responseStream = await response.Content.ReadAsStreamAsync();
				var responseDto = await JsonSerializer.DeserializeAsync<TransactionAuthenticationDto>(responseStream);
				if (responseDto.message.Equals(NOTIFICATION_SUCCESS_MESSAGE))
				{
					return true;
				}
			}
			return false;
		}

		private async Task<HttpResponseMessage> PostNotification(string email, decimal value)
		{
			var body = new StringContent
			(
				JsonSerializer.Serialize(new NotificationDto { Email = email, Value = value }),
				Encoding.UTF8,
				"application/json"
			);
			var client = _clientFactory.CreateClient();
			return await client.PostAsync(_notificationApi, body);
		}
}
