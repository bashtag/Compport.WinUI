using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business.API;
using App3.Business.Models;

namespace App3.ViewModels;

class SupportsViewModel : UsersViewModel
{
    protected override async void LoadDataAsync()
    {
        var data = await service.GetSupportsAsync();
        if (data != null)
        {
            foreach (var item in data)
            {
                Users.Add(item);
            }
        }
    }
}
