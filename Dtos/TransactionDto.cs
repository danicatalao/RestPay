using System;

namespace RestPay.Dtos
{
	public class TransactionDto
	{
		public Decimal Value { get; set; }

		public string Payer { get; set; }

		public string Payee{ get; set; }
}
}
