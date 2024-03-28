using System;
using Backend.Exceptions;
using Backend.Validator;

namespace Backend.Type
{
    public class ClassType : IType
    {
        public EType GetEType => EType.Class;
        public string TypeName { get; }

        public ClassType(string classType) : this(classType, ClassTypeValidator.Instance)
        {
            
        }
        protected ClassType(string classType, IClassTypeValidator validator) //Only for test purpose
        {
            if (!validator.IsClassType(classType))
            {
                throw new NotClassNameException();
            }

            TypeName = classType;
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
    }
}