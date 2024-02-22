//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//internal class ComputerModelViewModel
//{
//}
using System;
using System.Collections.ObjectModel;
using App3.Business.API;
using App3.Business.Models;
using DevExpress.WinUI.Grid;

namespace App3.ViewModels;

public class ComputerModelViewModel
{
    public ObservableCollection<ComputerModel> ComputerModels
    {
        get; set;
    }

    private readonly DeviceManagementService service = new();

    public ComputerModelViewModel()
    {
        ComputerModels = new ObservableCollection<ComputerModel>();
        LoadDataAsync();
    }

    private async void LoadDataAsync()
    {
        try
        {
            var data = await service.GetComputerModelsAsync();
            foreach (var item in data)
            {
                ComputerModels.Add(item);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}