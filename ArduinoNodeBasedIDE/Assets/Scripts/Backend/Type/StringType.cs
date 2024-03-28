using Backend.API;

namespace Backend.Type
{
    public class StringType : IMyType
    {
        public EType EType => EType.String;
        public string TypeName => "string";

        protected bool Equals(StringType other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StringType) obj);
        }

        public static bool operator ==(StringType left, StringType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StringType left, StringType right)
        {
            return !Equals(left, right);
        }
        
        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}