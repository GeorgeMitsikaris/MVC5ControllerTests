using MVCForTests.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCForTests.Tests.Controllers
{
    [TestFixture]
    public class EmployeesControllerTests
    {
        private MVCForTests.Controllers.EmployeesController _employeesController;

        [SetUp]
        public void Setup()
        {
            _employeesController = new MVCForTests.Controllers.EmployeesController();
        }

        [Test]
        public void Index_ViewBagEmployee_ReturnsCorrectMessage()
        {
            _employeesController.Index();
            Assert.That(_employeesController.ViewBag.Employees, Is.EqualTo("List of Employees"));
        }
    }
}
