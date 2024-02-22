using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using App3.Business.Models;
using Newtonsoft.Json;

namespace App3.Business.API;
public class DeviceManagementService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = ApiConnectionConfig.ConnectionString + "AdminSystem/";

    public DeviceManagementService()
    {
        _httpClient = new HttpClient();
    }

    private delegate Task<HttpResponseMessage> HttpClientRequestAsync(string? url, HttpContent? data);

    private async Task<(string, bool)> RequestAsync(object o, string postUrl, HttpClientRequestAsync requestAsync)
    {
        string text = null;
        var success = false;

        var url = _apiUrl + postUrl;
        var postJsonObject = JsonConvert.SerializeObject(o);
        var data = new StringContent(postJsonObject, Encoding.UTF8, "application/json");
        var response = await requestAsync(url, data);

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                success = true;
                break;

            case System.Net.HttpStatusCode.BadRequest:
                text = await response.Content.ReadAsStringAsync();
                break;

            default:
                text = "Beklenmeyen bir hata oluştu";
                break;
        }

        return (text, success);
    }

    private async Task<T> GetRequestAsync<T>(string getUrl) where T : class
    {
        var url = _apiUrl + getUrl;
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<ICollection<ComputerModel>> GetComputerModelsAsync()
    {
        var url = _apiUrl + "GetExistingComputerModels";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ICollection<ComputerModel>>(json);
    }

    public async Task<(string, bool)> AddComputerModelAsync(ComputerModel computerModel)
    {
        string text = null;
        var success = false;

        var url = _apiUrl + "AddComputerModel";
        var postJsonObject = JsonConvert.SerializeObject(computerModel);
        var data = new StringContent(postJsonObject, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, data);

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                success = true;
                break;

            case System.Net.HttpStatusCode.BadRequest:
                text = await response.Content.ReadAsStringAsync();
                break;

            default:
                text = "Beklenmeyen bir hata oluştu";
                break;
        }

        return (text, success);
    }

    public async Task<(string, bool)> AddDeviceAsync(Device device)
    {
        return await RequestAsync(device, "AddDevice", _httpClient.PostAsync);
    }

    public async Task<ICollection<Device>> GetUserDevices(int userId)
    {
        return await GetRequestAsync<ICollection<Device>>("GetDevicesByUserId?userId=" + userId);
    }
}
