using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.ConcertTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;

namespace Application.ConcertTickets.Tests
{
    [TestClass()]
    public class Tests_AccountTicketAppService
    {
        [TestMethod()]
        public async Task Test_GetCurrentUser()
        {
            // Arrange
            Mock<IUserService> targetService = new Mock<IUserService>();
            targetService
                .Setup(x => x.IdentityUser)
                .Returns("gelis");

            AccountTicketAppService target = new AccountTicketAppService(targetService.Object);
            string actual;
            string expected = "gelis";

            // Act
            actual = await target.GetCurrentUser();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}