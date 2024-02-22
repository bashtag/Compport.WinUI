using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using App3.Business.Models;
using App3.Business.Models.DTOs;
using Newtonsoft.Json;

namespace App3.Business.API;

public class AccountService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = ApiConnectionConfig.ConnectionString + "Account/";

    public AccountService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<(string, User)> GetLoginService(string username, string password)
    {
        var url = _apiUrl + "Login";
        var login = new LoginDTO
        {
            Email = username,
            Password = password
        };
        var postJson = JsonConvert.SerializeObject(login);
        var data = new StringContent(postJson, Encoding.UTF8, "application/json");

        User user = null;
        string text = null;
        HttpResponseMessage response = null;

        try
        {
            response = await _httpClient.PostAsync(url, data);
        }
        catch (Exception)
        {
            return ("API Bağlantısı Yok", null);
        }

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                var json = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(json);
                break;

            case System.Net.HttpStatusCode.Unauthorized:
                text = await response.Content.ReadAsStringAsync();
                break;

            case System.Net.HttpStatusCode.NotFound:
                text = await response.Content.ReadAsStringAsync();
                break;

            default:
                text = response.StatusCode.ToString();
                break;
        }

        return (text, user);
    }

    public async Task<HttpResponseMessage> GetRegisterService(User user)
    {
        var url = _apiUrl + "RegisterCustomer";
        var json = JsonConvert.SerializeObject(user);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            return await _httpClient.PostAsync(url, data);
        }
        catch (Exception)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }

    public async Task<ICollection<User>> UserAsync(string postUrl)
    {
        var url = _apiUrl + postUrl;
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ICollection<User>>(json);
    }

    public async Task<ICollection<User>> GetCustomersAsync()
    {
        return await UserAsync("GetCustomers");
    }

    public async Task<ICollection<User>> GetSupportsAsync()
    {
        return await UserAsync("GetSupports");
    }
}
