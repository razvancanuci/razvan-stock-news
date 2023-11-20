using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.DataAccess.Entities;

#nullable disable

public class ApiData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("symbol")]
    public string Symbol { get; set; }

    [BsonElement("regularMarketPrice")]
    public double RegularMarketPrice { get; set; }
    
    [BsonElement("prediction")]
    [BsonIgnoreIfNull]
    public double Prediction { get; set; }
}