using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Runtime.Events.Store.MongoDB;
using Mongo2Go;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Dolittle.Applications;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Specs.MongoDB;

namespace Dolittle.Runtime.Events.Store.MongoDB.Specs
{

    public class test_mongodb_event_store : EventStore
    {
        private readonly a_mongo_db_connection _database_runner;
        public test_mongodb_event_store(a_mongo_db_connection database_runner, ILogger logger): base(new EventStoreMongoDBConfiguration(database_runner.Connection, logger), logger)
        {
            _database_runner = database_runner;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _database_runner.Dispose();
                disposedValue = true;
            }
        }
    }
}