namespace RestPay.Models
{
	public interface IMongoDBSettings
	{
		public string DatabaseName { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
		public string UserCollectionName { get; set; }
		public string TransactionCollectionName { get; set; }
		public string ConnectionString { get; }
	}
}
