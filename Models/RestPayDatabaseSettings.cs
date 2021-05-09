namespace RestPay.Models
{
	public class RestPayDatabaseSettings : IRestPayDatabaseSettings
	{
		public string UserCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}
