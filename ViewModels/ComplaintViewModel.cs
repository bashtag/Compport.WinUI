using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business.Models;
using App3.Business.API;
using System.ComponentModel;

namespace App3.ViewModels;

public class ComplaintViewModel : INotifyPropertyChanged
{
    private readonly Dictionary<ComplaintStatus, string> complaintStatusTranslations = new()
    {
        { ComplaintStatus.Open, "Açık" },
        { ComplaintStatus.Support, "Destekte" },
        { ComplaintStatus.Technician, "Teknisyende" },
        { ComplaintStatus.CargoToTecnician, "Teknisyene Kargoda" },
        { ComplaintStatus.CargoToCustomer, "Müşteriye Kargoda" },
        { ComplaintStatus.Closed, "Kapalı" }
    };

    private readonly Dictionary<CargoStatus, string> cargoStatusTranslations = new()
    {
        { CargoStatus.Base, "Temel" },
        { CargoStatus.NotReceived, "Teslim Alınmadı" },
        { CargoStatus.Received, "Teslim Alındı" },
        { CargoStatus.Processing, "Yolda" },
        { CargoStatus.Shipped, "Gönderildi" },
        { CargoStatus.Delivered, "Müşteriye Teslim Edildi" }
    };

    public ObservableCollection<ComplaintDisplayModel> Complaints
    {
        get; set;
    }

    public ComplaintViewModel()
    {
        Complaints = new ObservableCollection<ComplaintDisplayModel>();
        LoadComplaints();
    }

    protected readonly ComplaintManagementService service = new();

    protected async virtual void LoadComplaints()
    {
        var complaints = await service.GetComplaintsAsync();
        Complaints.Clear();
        foreach (var complaint in complaints)
        {
            AddToComplaints(complaint);
        }
    }

    protected void AddToComplaints(Complaint complaint)
    {
        Complaints.Add(new ComplaintDisplayModel(complaint, complaintStatusTranslations, cargoStatusTranslations));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
