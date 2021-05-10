using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RestPay.Models
{
	public class Transaction
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("de")]
		[BsonRepresentation(BsonType.ObjectId)]
		public string PayerId { get; set; }

		[BsonElement("para")]
		[BsonRepresentation(BsonType.ObjectId)]
		public string PayeeId { get; set; }

		[BsonElement("valor")]
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal Value { get; set; }

		[BsonElement("quando")]
		[BsonRepresentation(BsonType.DateTime)]
		public DateTime DateTime { get; set; }
	}
}
