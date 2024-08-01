using System;
using CloudCustomers.API.configs;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
	public class TestUsersService
	{
		[Fact]
		public async Task GetAllUsers_WhenCalled_InvokesHttpGetReqeust()
		{
			//Arrange
			var expectedResponsse = UsersFixture.GetTestUsers();

			var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponsse);
			var httlClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httlClient, config);
			//Act

			await sut.GetAllUsers();
			//Assert
			handlerMock
				.Protected()
				.Verify(
				"SendAsync",
				Times.Exactly(1),
				ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
				ItExpr.IsAny<CancellationToken>()
				);
			// Verify HTTP request is made!
		}



		[Fact]
		public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
		{
			//Arrange
			var expectedResponsse = UsersFixture.GetTestUsers();
			var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponsse);
			var httlClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httlClient, config);
			//Act
			var result = await sut.GetAllUsers();
			//Assert
			result.Should().BeOfType<List<User>>();
		}

        [Fact]
        public async Task GetAllUsers_WhenCalled_Returns404()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httlClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httlClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponsse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponsse);
            var httlClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httlClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(expectedResponsse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfigureExternalUrl()
        {
            //Arrange
            var endpoint = "https://example.com/users";
            var expectedResponsse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponsse, endpoint);
            var httlClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            }); 
            var sut = new UsersService(httlClient, config);
            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);
            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }

}

