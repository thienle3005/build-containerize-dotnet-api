using System;
using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUserController
{
    [Fact]
    public async Task  Get_OnSuccess_ReturnsStatusCode200()
    {
        var mocUsersService = new Mock<IUsersSevice>();
        mocUsersService
       .Setup(serivce => serivce.GetAllUsers())
       .ReturnsAsync(new List<User>() {
                new()
                {
                    Id =1,
                    Name="Jane",
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "Madison",
                        ZipCode= "5374"

                    },

                    Email= "Jane@example.com"
                }

       });
        //Arrange
        var sut = new UsersController(mocUsersService.Object);
        //Act
        var result = (OkObjectResult)await sut.Get();
        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserService() {
        //Arrange
        var mocUsersService = new Mock<IUsersSevice>();
        mocUsersService
            .Setup(serivce => serivce.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mocUsersService.Object);
        //Act
        var result = (OkObjectResult)await sut.Get();
        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        var mocUsersService = new Mock<IUsersSevice>();
        mocUsersService
            .Setup(serivce => serivce.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mocUsersService.Object);
        var result = await sut.Get();

        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
        // Act
    }

    [Fact]
    public async Task Get_OnNoUserFound_Return404()
    {
        // Arrange
        var mocUsersService = new Mock<IUsersSevice>();
        mocUsersService
            .Setup(serivce => serivce.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mocUsersService.Object);
        var result = await sut.Get();

        result.Should().BeOfType<NotFoundResult>();

        var objectResult = (NotFoundResult)result;
        //Assert
        objectResult.StatusCode.Should().Be(404);

        // Act
    }

}