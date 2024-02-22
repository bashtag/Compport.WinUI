using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business.API;
using App3.Business.Models;

namespace App3.ViewModels;

public class CustomersViewModel : UsersViewModel
{
    public CustomersViewModel(): base() { }

    protected override async void LoadDataAsync()
    {
        var data = await service.GetCustomersAsync();
        if (data != null)
        {
            foreach (var item in data)
            {
                Users.Add(item);
            }
        }
    }
}
