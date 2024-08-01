using System;
using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new(){
            new User {
                    Id =1,
                    Name="Test case 1",
                    Address = new Address {
                        Street = "123 Main St",
                        City = "Madison",
                        ZipCode= "5374"

                    },

                    Email= "001@example.com"
             },
            new User {
                    Id =2,
                    Name="Test case 1",
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "Madison",
                        ZipCode= "00002"

                    },

                    Email= "002@example.com"
             },
            new User {
                    Id =2,
                    Name="Test case 2",
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "Madison",
                        ZipCode= "00003"

                    },

                    Email= "003@example.com"
             }

        };
    }
}
