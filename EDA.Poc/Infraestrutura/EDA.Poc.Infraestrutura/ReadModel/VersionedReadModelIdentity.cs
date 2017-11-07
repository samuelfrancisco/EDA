using System;

namespace EDA.Poc.Infraestrutura.ReadModel
{
    public class VersionedReadModelIdentity : IComparable, IEquatable<VersionedReadModelIdentity>, IComparable<VersionedReadModelIdentity>
    {
        public Guid Identity { get; set; }
        public long Version { get; set; }

        public VersionedReadModelIdentity(Guid identity, long version)
        {
            Identity = identity;
            Version = version;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", Identity, Version);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (Equals(obj, null)) return false;

            var identity = obj as VersionedReadModelIdentity;

            return !Equals(identity, null) && Equals(identity);
        }

        public bool Equals(VersionedReadModelIdentity other)
        {
            if (Equals(other, null)) return false;

            return Identity.Equals(other.Identity) && Version.Equals(other.Version);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var identity = obj as VersionedReadModelIdentity;

            if (Equals(identity, null)) throw new ArgumentException(string.Format("The argument must be of type {0}", typeof(VersionedReadModelIdentity).FullName));

            return CompareTo(identity);
        }

        public int CompareTo(VersionedReadModelIdentity other)
        {
            if (Equals(other, null)) return 1;

            return Identity == other.Identity
                       ? Version.CompareTo(other.Version)
                       : Identity.CompareTo(other.Identity);
        }

        public static bool operator ==(VersionedReadModelIdentity i1, VersionedReadModelIdentity i2)
        {
            if (Equals(i1, null) && Equals(i2, null)) return true;

            if (Equals(i1, null) || Equals(i2, null)) return false;

            return i1.Equals(i2);
        }

        public static bool operator !=(VersionedReadModelIdentity i1, VersionedReadModelIdentity i2)
        {
            return !(i1 == i2);
        }

        public static bool operator >(VersionedReadModelIdentity i1, VersionedReadModelIdentity i2)
        {
            if (Equals(i1, null) && Equals(i2, null)) return false;

            if (Equals(i1, null)) return false;

            if (Equals(i2, null)) return true;

            return i1.CompareTo(i2) > 0;
        }

        public static bool operator <(VersionedReadModelIdentity i1, VersionedReadModelIdentity i2)
        {
            if (Equals(i1, null) && Equals(i2, null)) return false;

            if (Equals(i1, null)) return true;

            if (Equals(i2, null)) return false;

            return i1.CompareTo(i2) < 0;
        }
    }
}