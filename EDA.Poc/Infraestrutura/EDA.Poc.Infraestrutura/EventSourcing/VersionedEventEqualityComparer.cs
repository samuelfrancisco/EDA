using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Infraestrutura.EventSourcing
{
    public class VersionedEventEqualityComparer : IEqualityComparer<IVersionedEvent>
    {
        public bool Equals(IVersionedEvent x, IVersionedEvent y)
        {
            if (object.Equals(x, null) && object.Equals(y, null)) return true;

            if (object.Equals(x, null) || object.Equals(y, null)) return false;

            return x.Id == y.Id && x.GetType() == y.GetType();
        }

        public int GetHashCode(IVersionedEvent obj)
        {
            return string.Format("{0}_{1}", obj.GetType().FullName, obj.Id).GetHashCode();
        }
    }
}