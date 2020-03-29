using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using Contoso.Domain.Service;
using Contoso.Entity;
using Contoso.Entity.Repository;
using Contoso.Entity.UnitofWork;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Contoso.Domain.Test
{
    class ContactTest
    {
    }


 
    public class ContactServiceTest
    {


        [Test]
        public void ContactService_Given_Contact_Id_Should_Get_Contact_Name()
        {
            //Arrange
            var ContactId = 1;
            var expected = "AAA";
            var Contact = new Contact() { FirstName = expected, Id = ContactId };

            var ContactRepositoryMock = new Mock<IRepository<Contact>>();
            ContactRepositoryMock.Setup(m => m.Get(ContactId)).Returns(Contact).Verifiable();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.GetRepository<Contact>()).Returns(ContactRepositoryMock.Object);

            IService< sut = new ContactService(unitOfWorkMock.Object);
            //Act
            var actual = sut.GetContactName(ContactId);

            //Assert
            ContactRepositoryMock.Verify();//verify that GetByID was called based on setup.
            Assert.IsNotNull(actual);//assert that a result was returned
            Assert.AreEqual(expected, actual);//assert that actual result was as expected
        }


        private Mock<IRepository<Contact>> _mockRepository;
        private ContactService<ContactViewModel, Contact> _service;
        Mock<IUnitOfWork> _mockUnitWork;
        List<Contact> listContact;

        [SetUp]
        public void Initialize()
        {
            
            services.AddAutoMapper(typeof(MappingProfile));

            _mockRepository = new Mock<IRepository<Contact>>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new ContactService<ContactViewModel, Contact>(_mockUnitWork.Object, _mockRepository.Object);
            listContact = new List<Contact>() {
           new Contact() { Id = 1, Name = "US" },
           new Contact() { Id = 2, Name = "India" },
           new Contact() { Id = 3, Name = "Russia" }
          };
        }

        [Test]
        public void Contact_Get_All()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(listContact);

            //Act
            List<Contact> results = _service.GetAll() as List<Contact>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }


        [Test]
        public void Can_Add_Contact()
        {
            //Arrange
            int Id = 1;
            Contact emp = new Contact() { FirstName = "UK" };
            _mockRepository.Setup(m => m.Insert(emp)).Returns((Contact e) =>
            {
                e.Id = Id;
                return e;
            });


            //Act
            _service.Add(emp);

            //Assert
            Assert.AreEqual(Id, emp.Id);
            _mockUnitWork.Verify(m => m.Commit(), Times.Once);
        }


    }
}
