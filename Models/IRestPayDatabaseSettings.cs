namespace RestPay.Models
{
	public interface IRestPayDatabaseSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string UserCollectionName { get; set; }

		public string TransactionCollectionName { get; set; }
	}
}
