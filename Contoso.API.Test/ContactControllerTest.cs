using System;
using System.Collections.Generic;
using System.Text;
using Contoso.Api.Controllers;
using Contoso.Entity;
using NUnit.Framework;
using Moq;
using Contoso.Domain.Service;
using Contoso.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.API.Test
{
    public class ContactControllerTest
    {
    

        private Mock<ContactService<ContactViewModel, Contact>> _ContactServiceMock;
        ContactController objController;
        List<ContactViewModel> listContact;

        [SetUp]
        public void Initialize()
        {

            _ContactServiceMock = new Mock<ContactService<ContactViewModel, Contact>>();
            objController = new ContactController(_ContactServiceMock.Object);
            listContact = new List<ContactViewModel>() {
           new ContactViewModel() { Id = 1, FirstName = "FName1", LastName = "LName1", Email = "1s@s.com" },
           new ContactViewModel() { Id = 2, FirstName = "FName2", LastName = "LName2", Email = "2s@s.com" },
           new ContactViewModel() { Id = 3, FirstName = "FName3", LastName = "LName3", Email = "3s@s.com" }
          };
        }



        [Test]
        public void Contact_Get_All()
        {
            //Arrange
            _ContactServiceMock.Setup(x => x.GetAll()).Returns(listContact);

            //Act
            var result = ((objController.GetAll() as ViewResult).Model) as List<Contact>;

            //Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("Name1", result[0].FirstName);
            Assert.AreEqual("Name2", result[1].FirstName);
            Assert.AreEqual("Name3", result[2].FirstName);

        }
        
        [Test]
        public void Valid_Contact_Create()
        {
            //Arrange
            ContactViewModel c = new ContactViewModel() { FirstName = "test1" , LastName = "LName1", Email = "1s@s.com" };

            //Act
            var result = (RedirectToRouteResult)objController.Create(c);

            //Assert 
            _ContactServiceMock.Verify(m => m.Add(c), Times.Once);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [Test]
        public void Invalid_Contact_Create()
        {
            // Arrange
            ContactViewModel c = new ContactViewModel() { FirstName = "" };
            objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var result = (ViewResult)objController.Create(c);

            //Assert
            _ContactServiceMock.Verify(m => m.Add(c), Times.Never);
            Assert.AreEqual("", result.ViewName);
        }

    }
}
