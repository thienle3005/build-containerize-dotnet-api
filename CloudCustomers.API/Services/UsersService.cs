﻿using System;
using CloudCustomers.API.configs;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services
{
    public interface IUsersSevice
    {
        public Task<List<User>> GetAllUsers();
    }
    public class UsersService : IUsersSevice
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }
        public async Task<List<User>> GetAllUsers() {
            var userResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if(userResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }

            var responseContent = userResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
           
        }
    }
}

