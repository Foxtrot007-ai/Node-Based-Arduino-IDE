using Backend.API;
using Backend.Exceptions;
using Backend.Validator;

namespace Backend.Type
{
    public class ClassType : IType
    {
        public EType EType => EType.Class;
        public string TypeName { get; }

        public ClassType(string classType) : this(classType, ClassTypeValidator.Instance)
        {
            
        }
        protected ClassType(string classType, IClassTypeValidator validator) //Only for test purpose
        {
            if (!validator.IsClassType(classType))
            {
                throw new NotClassTypeException(classType);
            }

            TypeName = classType;
        }

        public bool CanBeCast(IType iMyType)
        {
            if (iMyType is not ClassType classType)
            {
                return false;
            }
            return classType == this;
        }
        public bool IsAdapterNeed(IType iMyType)
        {
            return false;
        }
        public string ToCode()
        {
            return char.ToUpper(TypeName[0]) + TypeName.Substring(1);
        }

        protected bool Equals(ClassType other)
        {
            return TypeName == other.TypeName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClassType) obj);
        }

        public static bool operator ==(ClassType left, ClassType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ClassType left, ClassType right)
        {
            return !Equals(left, right);
        }
        
        public override int GetHashCode()
        {
            return (TypeName != null ? TypeName.GetHashCode() : 0);
        }
    }
}