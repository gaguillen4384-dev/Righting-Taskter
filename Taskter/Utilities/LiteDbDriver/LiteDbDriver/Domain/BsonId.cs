using LiteDB;

namespace LiteDbDriver
{
    /// <summary>
    /// An interface for object id in graph dbs.
    /// </summary>
    public class BsonId : ObjectId
    {
        public static BsonId NewObjectId()
        {
            return (BsonId)ObjectId.NewObjectId();
        }
    }
}
