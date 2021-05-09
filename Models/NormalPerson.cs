using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestPay.Models
{
	public class NormalPerson : User
	{
		[BsonElement("cpf")]
		public string Cpf { get; set; }
	}
}
