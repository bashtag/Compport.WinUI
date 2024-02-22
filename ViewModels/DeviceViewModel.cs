using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business;
using App3.Business.API;
using App3.Business.Models;

namespace App3.ViewModels;

public class DeviceViewModel
{
    public ObservableCollection<DeviceDisplay> Devices
    {
        get; set;
    }

    private readonly DeviceManagementService service = new();

    public DeviceViewModel()
    {
        Devices = new ObservableCollection<DeviceDisplay>();
        LoadDataAsync();
    }

    private async void LoadDataAsync()
    {
        var data = await service.GetUserDevices(UserSessionManagement.Instance.User.Id);
        foreach (var item in data)
        {
            Devices.Add(new DeviceDisplay
            {
                Id = item.Id,
                BatteryDescription = item.ComputerModel.BatteryDescription,
                BrandName = item.ComputerModel.BrandName,
                Description = item.ComputerModel.Description,
                ModelName = item.ComputerModel.ModelName,
                DisplayDescription = item.ComputerModel.DisplayDescription,
                GraphicsCardDescription = item.ComputerModel.GraphicsCardDescription,
                ProcessorDescription = item.ComputerModel.ProcessorDescription,
                ModelNumber = item.ComputerModel.ModelNumber,
                SerialNumber = item.SerialNumber,
                OperatingSystemDescription = item.ComputerModel.OperatingSystemDescription,
                RamDescription = item.ComputerModel.RamDescription,
                StorageDescription = item.ComputerModel.StorageDescription,
                Weight = item.ComputerModel.Weight,
                ReleaseDate = item.ComputerModel.ReleaseDate,
                WifiCardDescription = item.ComputerModel.WifiCardDescription,
            });
        }
    }
}
