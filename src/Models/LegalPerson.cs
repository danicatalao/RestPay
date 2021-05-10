using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestPay.Models
{
	public class LegalPerson : User
	{
		[BsonElement("cnpj")]
		public string Cnpj { get; set; }
	}
}
