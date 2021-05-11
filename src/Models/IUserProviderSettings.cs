namespace RestPay.Models
{
	public interface IUserProviderSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string UserCollectionName { get; set; }
	}
}