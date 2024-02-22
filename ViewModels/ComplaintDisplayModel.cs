using System;
using System.Collections.Generic;
using System.ComponentModel;
using App3.Business.Models;

namespace App3.ViewModels;

public class ComplaintDisplayModel : INotifyPropertyChanged
{
    // Other properties and methods...
    public string UserOrPhone
    {
        get;
    }
    public string Description
    {
        get;
    }
    public string DeviceModel
    {
        get;
    }
    public string SerialNumber
    {
        get;
    }
    public DateTime? ComplaintDate
    {
        get;
    }

    public ComplaintDisplayModel(Complaint complaint,
        Dictionary<ComplaintStatus, string> complaintStatusTranslations,
        Dictionary<CargoStatus, string> cargoStatusTranslations)
    {
        UserOrPhone = complaint.User?.Name ?? complaint.PhoneNumber ?? "Unknown";
        Description = complaint.Description;
        DeviceModel = complaint.Device.ComputerModel.ModelNumber;
        SerialNumber = complaint.Device.SerialNumber;
        ComplaintDate = complaint.Timestamp;
        ComplaintStatus = complaintStatusTranslations[complaint.Status];
        CargoStatus = cargoStatusTranslations[complaint.CargoStatus];
    }

    private string cargoStatus;

    public string CargoStatus
    {
        get => cargoStatus;
        set
        {
            if (cargoStatus != value)
            {
                cargoStatus = value;
                OnPropertyChanged(nameof(CargoStatus));
            }
        }
    }

    private string complaintStatus;

    public string ComplaintStatus
    {
        get => complaintStatus;
        set
        {
            if (complaintStatus != value)
            {
                complaintStatus = value;
                OnPropertyChanged(nameof(ComplaintStatus));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
