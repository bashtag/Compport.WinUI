using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.Business.API;
using App3.Business.Models;

namespace App3.ViewModels;

public abstract class UsersViewModel
{
	public ObservableCollection<User> Users
	{
		get; set;
	}

	protected readonly AccountService service = new();

	protected UsersViewModel()
	{
		Users = new ObservableCollection<User>();
		LoadDataAsync();
	}

	protected abstract void LoadDataAsync();
}

