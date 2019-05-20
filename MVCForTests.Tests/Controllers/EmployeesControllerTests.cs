using Moq;
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
        Employee employee;

        [SetUp]
        public void Setup()
        {
            _employeesController = new EmployeesController(_employeeRepo.Object);
            employee= new Employee { Id=1, FirstName="a",LastName="b",Gender="c",Email="a@b.c"};
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

        [Test]
        public void Create_WhenModelStateIsValid_RedirectsToIndex()
        {
            var result = _employeesController.Create(employee);
            var rootResult = (RedirectToRouteResult)result;
            Assert.That(rootResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Create_WhenModelStateIsValid_ReturnsRedirectToRouteResult()
        {
            var result = _employeesController.Create(employee);
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Create_WhenModelStateIsInValid_ViewNameIsEmptyString()
        {
            _employeesController.ModelState.AddModelError("MyError", "Mock error message");
            var result = _employeesController.Create(It.IsAny<Employee>()) as ViewResult;
            
            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Create_WhenModelStateIsInValid_ViewResultIsNotNull()
        {
            _employeesController.ModelState.AddModelError("MyError", "Mock error message");
            var result = _employeesController.Create(It.IsAny<Employee>()) as ViewResult;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Create_WhenModelStateIsInValid_ModelStateReturnsFalse()
        {
            _employeesController.ModelState.AddModelError("MyError", "Mock error message");
            var result = _employeesController.Create(It.IsAny<Employee>()) as ViewResult;

            Assert.That(result.ViewData.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Edit_WhenCalled_ReturnsViewResult()
        {
            _employeeRepo.Setup(e => e.GetEmployee(1)).Returns(employee);
            var result = _employeesController.Edit(1) as ViewResult;
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Edit_WhenSucceeds_RedirectsToIndex()
        {
            var result = _employeesController.Edit(employee);
            var rootResult = (RedirectToRouteResult)result;
            Assert.That(rootResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Edit_WhenModelStateIsValid_ReturnsRedirectToRouteResult()
        {
            var result = _employeesController.Edit(employee);
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }
    }
}
