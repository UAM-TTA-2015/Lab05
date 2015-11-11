﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UamTTA.Storage;

namespace UamTTA.Tests
{
    [TestFixture]
    public class InMemoryRepositoryTests
    {
        private InMemoryRepository<TestModel> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new InMemoryRepository<TestModel>();
        }

        private class TestModel : ModelBase
        {
//            public IEnumerable<int> Ids { get; set; }

            public int SomeIntAttribute { get; set; }

            public string SomeStringAttribute { get; set; }
        }

        [Test]
        public void Persist_Should_Return_Copy_Of_Transient_Object_With_Id_Assigned()
        {
            var someTransientModel = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            TestModel result = _sut.Persist(someTransientModel);

            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.Not.SameAs(someTransientModel));
            Assert.That(result.SomeIntAttribute, Is.EqualTo(someTransientModel.SomeIntAttribute));
            Assert.That(result.SomeStringAttribute, Is.EqualTo(someTransientModel.SomeStringAttribute));
            Assert.That(result.Id.HasValue);
        }

        [Test]
        public void Persist_Should_Return_Copy_Of_Persited_Object_With_Id_Same_Id_Assigned()
        {
            var somePersistedModel = new TestModel { Id = 100, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            TestModel result = _sut.Persist(somePersistedModel);

            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.Not.SameAs(somePersistedModel));
            Assert.That(result.SomeIntAttribute, Is.EqualTo(somePersistedModel.SomeIntAttribute));
            Assert.That(result.SomeStringAttribute, Is.EqualTo(somePersistedModel.SomeStringAttribute));
            Assert.That(result.Id, Is.EqualTo(somePersistedModel.Id));
        }

        [Test]
        public void Subsequent_Persist_Calls_Objects_Should_Assign_Different_Id()
        {
            var someTransientModel = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            TestModel result1 = _sut.Persist(someTransientModel);
            TestModel result2 = _sut.Persist(someTransientModel);

            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.Not.Null);

            Assert.That(result1, Is.Not.SameAs(someTransientModel));
            Assert.That(result2, Is.Not.SameAs(someTransientModel));
            Assert.That(result1, Is.Not.SameAs(result2));
            Assert.That(result1.Id, Is.Not.Null);
            Assert.That(result2.Id, Is.Not.Null);
            Assert.That(result1.Id, Is.Not.EqualTo(result2.Id));
        }

        [Test]
        public void Persisted_Data_Should_Be_Accesible_By_Id_Via_FindById()
        {
            var someTransientModel = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            TestModel persisted = _sut.Persist(someTransientModel);
            TestModel actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual.Id, Is.EqualTo(persisted.Id));
            Assert.That(actual.SomeIntAttribute, Is.EqualTo(persisted.SomeIntAttribute));
            Assert.That(actual.SomeStringAttribute, Is.EqualTo(persisted.SomeStringAttribute));
        }

        [Test]
        public void Persisted_Object_With_Already_Existing_Id_Should_Evict_Previus_Data()
        {
            var someTransientModel = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            TestModel persisted = _sut.Persist(someTransientModel);
            var anotherWithSameId = new TestModel { Id = persisted.Id, SomeIntAttribute = 1121210, SomeStringAttribute = "xd^grrr" };
            _sut.Persist(anotherWithSameId);
            TestModel actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual.Id, Is.EqualTo(persisted.Id));
            Assert.That(actual.SomeIntAttribute, Is.EqualTo(anotherWithSameId.SomeIntAttribute));
            Assert.That(actual.SomeStringAttribute, Is.EqualTo(anotherWithSameId.SomeStringAttribute));
        }

        [Test]
        public void FindById_Should_Return_NUll_When_Object_Og_Given_Id_Was_Not_Found()
        {
            TestModel actual = _sut.FindById(4475438);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void Remove_Should_Remove_Item_Of_Same_Id_From_Storage()
        {
            var someTransientModel = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            TestModel persisted = _sut.Persist(someTransientModel);
            var anotherWithSameId = new TestModel { Id = persisted.Id };
            _sut.Remove(anotherWithSameId);

            TestModel actual = _sut.FindById(persisted.Id.Value);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetAll_Returns_Empty_Collection_When_Repository_Is_Empty()
        {
            var result = _sut.GetAll();

            Assert.That(result.Any(), Is.False);
            //CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void GetAll_Returns_All_Items()
        {
            var model1 = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };
            var model2 = new TestModel { Id = null, SomeIntAttribute = 12, SomeStringAttribute = "BlaBla" };

            _sut.Persist(model1);
            _sut.Persist(model2);
            var result = _sut.GetAll();

            Assert.That(result.Count(), Is.EqualTo(2));
            CollectionAssert.AllItemsAreUnique(result);
        }

        [Test]
        public void Take_Returns_First_Three_Items()
        {
            var model1 = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };
            var model2 = new TestModel { Id = null, SomeIntAttribute = 12, SomeStringAttribute = "BlaBla" };
            var model3 = new TestModel { Id = null, SomeIntAttribute = 14, SomeStringAttribute = "BlaBlaBla" };


            _sut.Persist(model1);
            _sut.Persist(model2);
            _sut.Persist(model3);

            var result= _sut.Take(3);

            Assert.That(result.Count(), Is.EqualTo(3));

        }

        [Test]
        public void Take_Returns_First_Two_Items_But_Is_Three()
        {
            var model1 = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };
            var model2 = new TestModel { Id = null, SomeIntAttribute = 12, SomeStringAttribute = "BlaBla" };
            var model3 = new TestModel { Id = null, SomeIntAttribute = 14, SomeStringAttribute = "BlaBlaBla" };


            _sut.Persist(model1);
            _sut.Persist(model2);
            _sut.Persist(model3);

            var result = _sut.Take(2);

            Assert.That(result.Count(), Is.EqualTo(2));

        }

        [Test]
        public void Take_Returns_Throw_ArgumentException_Because_Is_Empty()
        {
           
            var e = Assert.Throws<ArgumentException>(() =>_sut.Take(2));

            Assert.That(e.Message, Is.EqualTo("Empty repository"));

        }

        [Test]
        public void Take_Returns_Throw_ArgumentException_Because_Not_Enough_Items()
        {
            var model1 = new TestModel { Id = null, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };

            _sut.Persist(model1);


            var e = Assert.Throws<ArgumentException>(() => _sut.Take(2));

            Assert.That(e.Message, Is.EqualTo("Empty repository"));

        }

        [Test]
        public void Take_Returns_Throw_ArgumentException_Because_Expects_Items()
        {
            var e = Assert.Throws<ArgumentException>(() => _sut.Take(0));

            Assert.That(e.Message, Is.EqualTo("Empty repository"));
        }

        [Test]
        public void GetByIds_Return_Item_By_Id()
        {

            var model1 = new TestModel { Id = 1, SomeIntAttribute = 10, SomeStringAttribute = "Bla" };
            var model2 = new TestModel { Id = 3, SomeIntAttribute = 12, SomeStringAttribute = "BlaBla" };
            var model3 = new TestModel { Id = 4, SomeIntAttribute = 14, SomeStringAttribute = "BlaBlaBla" };


            _sut.Persist(model1);
            _sut.Persist(model2);
            _sut.Persist(model3);

            List<int> lista = new List<int>() {1, 3, 4};


            var result = _sut.GetByIds(lista);


            Assert.That(result.Count(), Is.EqualTo(3));
        }






    }
}