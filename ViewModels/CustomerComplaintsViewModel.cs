using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business;

namespace App3.ViewModels;

public class CustomerComplaintsViewModel : ComplaintViewModel
{
    protected async override void LoadComplaints()
    {
        var data = await service.GetComplaintsbyUserIdAsync(UserSessionManagement.Instance.User.Id);
        if (data != null)
        {
            Complaints.Clear();
            foreach (var item in data)
            {
                AddToComplaints(item);
            }
        }
    }
}

