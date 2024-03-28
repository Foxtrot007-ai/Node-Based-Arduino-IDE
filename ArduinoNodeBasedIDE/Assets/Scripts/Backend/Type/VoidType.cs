using System;
using Backend.API;

namespace Backend.Type
{
    public class VoidType : IMyType
    {
        public EType EType => EType.Void;
        public string TypeName => "void";

        protected bool Equals(VoidType other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VoidType) obj);
        }
        
        public static bool operator ==(VoidType left, VoidType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VoidType left, VoidType right)
        {
            return !Equals(left, right);
        }
        
        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}