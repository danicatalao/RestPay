using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestPay.Models
{
	[BsonIgnoreExtraElements]
	[BsonDiscriminator(RootClass = true)]
	[BsonKnownTypes(typeof(NormalPerson), typeof(LegalPerson))]
	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("nome")]
		[BsonRepresentation(BsonType.String)]
		public string Name { get; set; }

		[BsonElement("senha")]
		[BsonRepresentation(BsonType.String)]
		public string Password { get; set; }

		[BsonElement("email")]
		[BsonRepresentation(BsonType.String)]
		public string Email { get; set; }

		[BsonElement("carteira")]
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal Wallet { get; set; }
	}
}
