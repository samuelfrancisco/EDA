using System.Collections.Generic;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;

namespace EDA.Poc.Infraestrutura.ReadModel
{
    public class ReadModelProcessedEventEventEqualityComparer : IEqualityComparer<IReadModelProcessedEvent>
    {
        public bool Equals(IReadModelProcessedEvent x, IReadModelProcessedEvent y)
        {
            if (object.Equals(x, null) && object.Equals(y, null)) return true;

            if (object.Equals(x, null) || object.Equals(y, null)) return false;

            return x.ReadModelTypeFullName == y.ReadModelTypeFullName && x.ReadModelIdentity == y.ReadModelIdentity
                && x.EventTypeFullName == y.EventTypeFullName && x.EventId == y.EventId;
        }

        public int GetHashCode(IReadModelProcessedEvent obj)
        {
            return string.Format("{0}_{1}_{2}_{3}", obj.ReadModelTypeFullName, obj.ReadModelIdentity, obj.EventTypeFullName, obj.EventId).GetHashCode();
        }
    }
}
