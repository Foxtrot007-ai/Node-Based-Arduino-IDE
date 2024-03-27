using System;
using System.Collections.Generic;

namespace Backend.Validator
{
    public static class ClassTypeValidator
    {
        private static HashSet<String> _allClassTypes = new HashSet<string>();

        public static bool IsClassType(String className)
        {
            return _allClassTypes.Contains(className);
        
        }

        public static HashSet<String> GetAllClassTypes()
        {
            return _allClassTypes;
        }

        public static void AddClassType(String classType)
        {
            if (classType is null)
            {
                throw new ArgumentNullException();
            }
            
            _allClassTypes.Add(classType);
        }
    
    }
}