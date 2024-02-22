using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business.Models;

namespace App3.Business;

public class UserSessionManagement
{
    private static UserSessionManagement _instance;
    public static UserSessionManagement Instance => _instance ??= new UserSessionManagement();

    public User User
    {
        get; set;
    }

    public bool IsLoggedIn
    {
        get; set;
    }

    public void LogIn(User user)
    {
        User = user;
        IsLoggedIn = true;
    }

    public void LogOut()
    {
        User = null;
        IsLoggedIn = false;
    }

}
