using System;
using System.Linq;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Store.Specs;
using Dolittle.Applications;
using Moq;
using Dolittle.Runtime.Events.Specs.MongoDB;

namespace Dolittle.Runtime.Events.Store.MongoDB.Specs
{
    public class SUTProvider : IProvideTheEventStore
    {
        public IEventStore Build() => new test_mongodb_event_store(new a_mongo_db_connection(),given.a_logger());
    }
}