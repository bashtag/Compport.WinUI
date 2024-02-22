using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using App3.Business.Models;
using App3.Business.Models.DTOs;


namespace App3.Business.API;

public class ComplaintManagementService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = ApiConnectionConfig.ConnectionString + "Complaints/";

    public ComplaintManagementService()
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

    public async Task<(string, bool)> DeleteComplaintAsync(Complaint complaint)
    {
        return await RequestAsync(complaint, "DeleteComplaint", _httpClient.PostAsync);
    }

    public async Task<(string, bool)> AddComplaintAsync(Complaint complaint)
    {
        return await RequestAsync(complaint, "AddComplaint", _httpClient.PostAsync);
    }

    public async Task<(string, bool)> UpdateComplaintAsync(Complaint complaint)
    {
        return await RequestAsync(complaint, "UpdateComplaint", _httpClient.PutAsync);
    }

    public async Task<ICollection<Complaint>> GetComplaintsAsync()
    {
        return await GetRequestAsync<ICollection<Complaint>>("GetExistingComplaints");
    }

    public async Task<(string, bool)> AddMessageToComplaintAsync(AddMessageToComplaintDTO dto)
    {
        return await RequestAsync(dto, "AddMessageToComplaint", _httpClient.PostAsync);
    }

    public async Task<ICollection<Complaint>> GetComplaintsbyUserIdAsync(int userId)
    {
        return await GetRequestAsync<ICollection<Complaint>>("GetComplaintsByUserId?userId=" + userId);
    }
}
