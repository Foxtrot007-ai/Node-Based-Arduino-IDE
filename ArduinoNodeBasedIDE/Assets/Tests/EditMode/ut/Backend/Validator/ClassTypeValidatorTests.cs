using System;
using System.Collections.Generic;
using Backend.Validator;
using NUnit.Framework;

namespace ut.Type
{
    public class ClassTypeValidatorTests
    {
        [Test]
        public void IsClassTypeEmptySet()
        {
            //given
            //when
            //then
            Assert.False(ClassTypeValidator.IsClassType("test"));
        }

        [Test]
        public void AddNullTypeFail()
        {
            //given
            //when
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ClassTypeValidator.AddClassType(null));
            //then

        }

        [Test]
        [Order(1)]
        public void GetClassTypesEmpty()
        {
            //given
            //when
            //then
            Assert.IsEmpty(ClassTypeValidator.GetAllClassTypes());
        }
        
        [Test]
        [Order(2)]
        public void IsClassTypeSuccess()
        {
            //given
            ClassTypeValidator.AddClassType("test1");
            //when
            //then
            Assert.True(ClassTypeValidator.IsClassType("test1"));
        }

        [Test]
        [Order(3)]
        public void GetClassTypes()
        {
            //given
            HashSet<String> expectTypes = new HashSet<string>() { "test1", "test2", "test3"};
            ClassTypeValidator.AddClassType("test2");
            ClassTypeValidator.AddClassType("test3");
            //when
            HashSet<String> classTypes = ClassTypeValidator.GetAllClassTypes();
            //then
            Assert.AreEqual(3, classTypes.Count);
            Assert.AreEqual(classTypes, expectTypes);
        }
    }
}
