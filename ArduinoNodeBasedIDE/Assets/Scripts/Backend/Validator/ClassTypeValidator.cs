using System;
using System.Collections.Generic;

namespace Backend.Validator
{
    public sealed class ClassTypeValidator : IClassTypeValidator
    {
        private readonly HashSet<string> _allClassTypes = new();

        private ClassTypeValidator()
        {
            
        }

        static ClassTypeValidator()
        {
            
        }

        public static ClassTypeValidator Instance { get; } = new();

        public bool IsClassType(string className)
        {
            return _allClassTypes.Contains(className);
        
        }

        public HashSet<string> GetAllClassTypes()
        {
            return _allClassTypes;
        }

        public void AddClassType(string classType)
        {
            if (classType is null)
            {
                throw new ArgumentNullException(null,"classType cannot be null.");
            }
            
            _allClassTypes.Add(classType);
        }
    
    }
}