using LiteDB;
using System;

namespace LiteDbDriver
{
    public class BaseDocument
    {
        /// <summary>
        /// The unique identifier for the object. Can only be retrieved, not set.
        /// </summary>
        public ObjectId ID { get; } = ObjectId.NewObjectId();

        /// <summary>
        /// The date the object got created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date the object got updated.
        /// </summary>
        public DateTime? DateUpdated { get; set; } = null;

        [BsonCtor]
        public BaseDocument(ObjectId _id, DateTime dateCreated, DateTime? dateUpdated)
        {
            ID = _id;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }
    }
}

