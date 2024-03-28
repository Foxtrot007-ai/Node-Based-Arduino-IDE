﻿using System;
using System.Collections.Generic;
using Backend.Validator;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Validator
{
    [TestFixture]
    [TestOf(typeof(ClassTypeValidator))]
    [Category("Type")]
    public class ClassTypeValidatorTest
    {
        [Test]
        [Order(0)]
        public void IsClassTypeEmptySet()
        {
            //given
            //when
            //then
            Assert.False(ClassTypeValidator.Instance.IsClassType("test"));
        }

        [Test]
        [Order(1)]
        public void AddNullTypeFail()
        {
            //given
            //when
            var exception = Assert.Throws<ArgumentNullException>(() => ClassTypeValidator.Instance.AddClassType(null));
            //then
            Assert.AreEqual("classType cannot be null.", exception.Message);
        }

        [Test]
        [Order(2)]
        public void GetClassTypesEmpty()
        {
            //given
            //when
            //then
            Assert.IsEmpty(ClassTypeValidator.Instance.GetAllClassTypes());
        }
        
        [Test]
        [Order(3)]
        public void IsClassTypeSuccess()
        {
            //given
            ClassTypeValidator.Instance.AddClassType("test1");
            //when
            //then
            Assert.True(ClassTypeValidator.Instance.IsClassType("test1"));
        }

        [Test]
        [Order(4)]
        public void GetClassTypes()
        {
            //given
            var expectTypes = new HashSet<string> { "test1", "test2", "test3"};
            ClassTypeValidator.Instance.AddClassType("test2");
            ClassTypeValidator.Instance.AddClassType("test3");
            //when
            var classTypes = ClassTypeValidator.Instance.GetAllClassTypes();
            //then
            Assert.AreEqual(3, classTypes.Count);
            Assert.AreEqual(classTypes, expectTypes);
        }
    }
}
