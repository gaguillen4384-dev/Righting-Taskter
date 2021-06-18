using LiteDB;

namespace LiteDbDriver
{
    /// <summary>
    /// An interface for object id in graph dbs.
    /// </summary>
    //TODO: This is not working because I can't make a Bson using the ObjectId methods.
    public class BsonId : ObjectId
    {
        // Not sure if this is the proper way to interface a 3rd party.
        public ObjectId Id { get; }

        public BsonId(string id):base() 
        {
            Id = new ObjectId(id);
        }

    }
}
