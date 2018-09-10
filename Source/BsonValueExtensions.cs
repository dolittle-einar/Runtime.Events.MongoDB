using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Collections;
using Dolittle.PropertyBags;
using Dolittle.Runtime.Events.Store;
using MongoDB.Bson;

namespace Dolittle.Runtime.Events.MongoDB
{

    /// <summary>
    /// Extensions to convert <see cref="BsonValue">Bson Values</see> into domain specific types and dotnet types
    /// </summary>
    public static class BsonValueExtensions 
    {
        /// <summary>
        /// Converts a <see cref="BsonValue" /> into a <see cref="DateTimeOffset" />
        /// </summary>
        /// <param name="value">The <see cref="BsonValue" /></param>
        /// <returns>The corresponding <see cref="DateTimeOffset" /></returns>
        public static DateTimeOffset AsDateTimeOffset(this BsonValue value)
        {
            return new DateTimeOffset(value.AsInt64, new TimeSpan(0,0,0));
        }

        /// <summary>
        /// Converts a <see cref="BsonValue" /> into a <see cref="CommitId" />
        /// </summary>
        /// <param name="value">The <see cref="BsonValue" /></param>
        /// <returns>The corresponding <see cref="CommitId" /></returns>
        public static CommitId ToCommitId(this BsonValue value)
        {
            return value.AsGuid;
        }

        /// <summary>
        /// Converts a <see cref="BsonValue" /> into a <see cref="UInt64" />
        /// </summary>
        /// <param name="value">The <see cref="BsonValue" /></param>
        /// <returns>The corresponding <see cref="UInt64" /></returns>
        public static ulong ToUlong(this BsonValue value)
        {
            return Convert.ToUInt64(value.ToDouble());
        }

        /// <summary>
        /// Converts a <see cref="BsonValue" /> into a <see cref="UInt32" />
        /// </summary>
        /// <param name="value">The <see cref="BsonValue" /></param>
        /// <returns>The corresponding <see cref="UInt32" /></returns>
        public static uint ToUint(this BsonValue value)
        {
            return Convert.ToUInt32(value.ToInt64());
        }

        /// <summary>
        /// Converts a <see cref="BsonValue" /> into an <see cref="EventStream" />
        /// </summary>
        /// <param name="value">The <see cref="BsonValue" /></param>
        /// <returns>The corresponding <see cref="EventStream" /></returns>
        public static EventStream ToEventStream(this BsonValue value)
        {
            var list = new List<EventEnvelope>();
            foreach(var val in value.AsBsonArray)
            {
                list.Add(val.AsBsonDocument.ToEventEnvelope());
            }
            return new EventStream(list);
        }

        /// <summary>
        /// Converts a <see cref="BsonValue" /> into an <see cref="PropertyBag" />
        /// </summary>
        /// <param name="value">The <see cref="BsonValue" /></param>
        /// <returns>The corresponding <see cref="PropertyBag" /></returns>        
        public static PropertyBag ToPropertyBag(this BsonValue value)
        {
            var bsonAsDictionary = value.AsBsonDocument.ToDictionary();
            var nonNullDictionary = new NullFreeDictionary<string,object>();
            bsonAsDictionary.ForEach(kvp =>
            {
                if(kvp.Value != null)
                    nonNullDictionary.Add(kvp);
            });
            var propertyBag = new PropertyBag(nonNullDictionary);
            return propertyBag;
        }
    }
}