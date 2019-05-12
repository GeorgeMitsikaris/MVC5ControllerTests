using Moq;
using MVCForTests.Controllers;
using MVCForTests.Models;
using MVCForTests.RepositoryContracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
