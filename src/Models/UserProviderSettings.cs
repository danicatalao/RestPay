namespace RestPay.Models
{
	public class UserProviderSettings : IUserProviderSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string UserCollectionName { get; set; }
	}
}