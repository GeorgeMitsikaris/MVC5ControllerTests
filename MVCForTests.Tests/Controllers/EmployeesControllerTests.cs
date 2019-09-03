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
            _employeeRepo.Setup(e => e.GetEmployee(It.IsAny<int>())).Returns(new Employee());
            var result = _employeesController.Details(It.IsAny<int>()) as ViewResult;
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Details_IdIsNull_ReturnsHttpStatusCodeResult()
        {
            var result = _employeesController.Details(null) as HttpStatusCodeResult;
            Assert.That(result, Is.TypeOf<HttpStatusCodeResult>());
        }

        [Test]
        public void Details_IdIsNull_StatusCodeIs400()
        {
            var result = _employeesController.Details(null) as HttpStatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public void Details_EmployeeIsNull_ReturnsHttpNotFound()
        {
            var employee = _employeeRepo.Setup(e => e.GetEmployee(It.IsAny<int>())).Returns(() => null);
            var result = _employeesController.Details(It.IsAny<int>());
            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
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
            var result = _employeesController.Create(It.IsAny<Employee>());
            var rootResult = (RedirectToRouteResult)result;
            Assert.That(rootResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Create_WhenModelStateIsValid_ReturnsRedirectToRouteResult()
        {
            var result = _employeesController.Create(It.IsAny<Employee>());
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Create_ModelStateIsInValid_ReturnsViewResult()
        {
            _employeesController.ModelState.AddModelError("Test", "My Custom Error");
            var result = _employeesController.Create(It.IsAny<Employee>());
            Assert.That(result, Is.TypeOf<ViewResult>());
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
            _employeeRepo.Setup(e => e.GetEmployee(It.IsAny<int>())).Returns(employee);
            var result = _employeesController.Edit(It.IsAny<int>()) as ViewResult;
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Edit_IdIsNull_ReturnsHttpStatusCodeResult()
        {
            var result = _employeesController.Edit((int?)null) as HttpStatusCodeResult;
            Assert.That(result, Is.TypeOf<HttpStatusCodeResult>());
        }

        [Test]
        public void Edit_EmployeeIsNull_ReturnsHttpNotFound()
        {
            var employee = _employeeRepo.Setup(e => e.GetEmployee(It.IsAny<int>())).Returns(() => null);
            var result = _employeesController.Edit(It.IsAny<int>()) as HttpStatusCodeResult;
            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Edit_IdIsNull_StatusCodeIs400()
        {
            var result = _employeesController.Edit((int?)null) as HttpStatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public void Edit_WhenModelStateIsValid_ReturnsRedirectToRouteResult()
        {
            var result = _employeesController.Edit(It.IsAny<Employee>());
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Edit_WhenModelStateIsValid_RedirectsToIndex()
        {
            var result = _employeesController.Edit(It.IsAny<Employee>());
            var rootResult = result as RedirectToRouteResult;
            Assert.That(rootResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Edit_WhenModelStateIsInValid_ViewNameIsEmptyString()
        {
            _employeesController.ModelState.AddModelError("EditError", "Mock error message");
            var result = _employeesController.Edit(It.IsAny<Employee>()) as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Edit_WhenModelStateIsInValid_ViewResultIsNotNull()
        {
            _employeesController.ModelState.AddModelError("EditError", "Mock error message");
            var result = _employeesController.Edit(It.IsAny<Employee>()) as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Edit_WhenModelStateIsInValid_ModelStateReturnsFalse()
        {
            _employeesController.ModelState.AddModelError("EditError", "Mock error message");
            var result = _employeesController.Edit(It.IsAny<Employee>()) as ViewResult;
            Assert.That(result.ViewData.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Delete_IdIsNull_ReturnsHttpStatusCodeResult()
        {
            var result = _employeesController.Delete((int?)null) as HttpStatusCodeResult;
            Assert.That(result, Is.TypeOf<HttpStatusCodeResult>());
        }

        [Test]
        public void Delete_IdIsNull_StatusCodeIs400()
        {
            var result = _employeesController.Delete((int?)null) as HttpStatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public void Delete_EmployeeIsNull_ReturnsHttpNotFound()
        {
            var result = _employeesController.Delete(It.IsAny<int>()) as HttpStatusCodeResult;
            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Delete_WhenCalled_ReturnsViewResult()
        {
            _employeeRepo.Setup(e => e.GetEmployee(It.IsAny<int>())).Returns(new Employee());
            var result = _employeesController.Delete(It.IsAny<int>()) as ViewResult;
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void DeleteConfirmed_WhenCalled_RedirectsToIndex()
        {
            var rootResult = _employeesController.DeleteConfirmed(It.IsAny<int>()) as RedirectToRouteResult;
            Assert.That(rootResult.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void DeleteConfirmed_WhenCalled_ReturnsRedirectsToRootResult()
        {
            var rootResult = _employeesController.DeleteConfirmed(It.IsAny<int>()) as RedirectToRouteResult;
            Assert.That(rootResult, Is.TypeOf<RedirectToRouteResult>());
        }
    }
}
