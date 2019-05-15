﻿using Moq;
using MVCForTests.Controllers;
using MVCForTests.Models;
using MVCForTests.RepositoryContracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCForTests.Tests.Controllers
{
    [TestFixture]
    public class EmployeesControllerTests
    {
        private MVCForTests.Controllers.EmployeesController _employeesController;
        private Mock<IEmployeeRepo> _employeeRepo=new Mock<IEmployeeRepo>();

        [SetUp]
        public void Setup()
        {
            _employeesController = new EmployeesController(_employeeRepo.Object);
        }

        [Test]
        public void Index_ViewBagEmployee_ReturnsCorrectMessage()
        {
            _employeesController.Index();
            Assert.That(_employeesController.ViewBag.Employees, Is.EqualTo("List of Employees"));
        }

        [Test]
        public void Index_WhenCalled_ReturnsViewResult()
        {
            var result = _employeesController.Index();
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Details_WhenCalled_ReturnsViewResult()
        {
            _employeeRepo.Setup(e => e.GetEmployee(1)).Returns(new Employee());
            var result = _employeesController.Details(1) as ViewResult;
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Details_IdIsNull_ReturnsHttpStatusCodeResult()
        {
            var result = _employeesController.Details(null) as HttpStatusCodeResult;
            Assert.That(result, Is.TypeOf<HttpStatusCodeResult>());
        }

        [Test]
        public void Details_EmployeeIsNull_ReturnsHttpNotFound()
        {
            var result = _employeesController.Details(-1);
            var employee = _employeeRepo.Setup(e => e.GetEmployee(-1)).Returns(() => null);
            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Details_IdIsNull_StatusCodeIs400()
        {
            var result = _employeesController.Details(null) as HttpStatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public void Create_WhenCalled_ReturnsViewResult()
        {
            var result = _employeesController.Create() as ViewResult;
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
