using System;
using System.Collections.Generic;

namespace Backend.Validator
{
    public sealed class ClassTypeValidator : IClassTypeValidator
    {
        private readonly HashSet<string> _allClassTypes = new();
        private static ClassTypeValidator _instance = new();

        private ClassTypeValidator()
        {
            
        }

        static ClassTypeValidator()
        {
            
        }

        public static ClassTypeValidator Instance => _instance;

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
                throw new ArgumentNullException();
            }
            
            _allClassTypes.Add(classType);
        }
    
    }
}