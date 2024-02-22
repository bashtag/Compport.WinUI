using App3.Business.Models;
using App3.Pages.Account;
using App3.Pages.ControlPages;
using App3.Pages.ControlPages.Admin;
using App3.Pages.ControlPages.Common;
using App3.Pages.ControlPages.Customer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App3.Pages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NavigationPage : Page
{
    public NavigationPage()
    {
        this.InitializeComponent();
    }

    private void NavigationPageNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            // Navigate to settings page
            NavigationPageFrame.Navigate(typeof(AccountSettings));
        }
        else
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;

            switch (selectedItem.Tag.ToString())
            {
                case "MainPage":
                    NavigationPageFrame.Navigate(typeof(MainPage));
                    break;
                case "Devices":
                    NavigationPageFrame.Navigate(typeof(AdminDevices));
                    break;
                case "Complaints":
                    NavigationPageFrame.Navigate(typeof(AdminComplaints));
                    break;
                case "Customers":
                    NavigationPageFrame.Navigate(typeof(Customers));
                    break;
                case "Supports":
                    NavigationPageFrame.Navigate(typeof(Supports));
                    break;
                case "CustomerDevices":
                    NavigationPageFrame.Navigate(typeof(CustomerDevices));
                    break;
                case "CustomerComplaints":
                    NavigationPageFrame.Navigate(typeof(CustomerComplaints));
                    break;
                default:
                    break;
            }
        }
    }

    private void NavigationPageNavigationView_Loaded(object sender, RoutedEventArgs e)
    {
        NavigationPageNavigationView.SelectedItem = NavigationPageNavigationView.MenuItems[0];
        var settingsItem = NavigationPageNavigationView.SettingsItem as NavigationViewItem;
        if (settingsItem != null)
        {
            settingsItem.Content = "Hesap Ayarlarý";
        }

        var userRole = Business.UserSessionManagement.Instance.User.Role;

        switch (userRole)
        {
            case UserRole.Admin:
                AddNavigationItem("Cihazlar", new SymbolIcon(Symbol.AllApps), "Devices");
                AddNavigationItem("Þikayetler", new SymbolIcon(Symbol.ReportHacked), "Complaints");
                AddNavigationItem("Müþteriler", new SymbolIcon(Symbol.People), "Customers");
                AddNavigationItem("Destek Ekibi", new FontIcon
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Glyph = "\xE77B;"
                }, "Supports");
                break;

            case UserRole.Customer:
                AddNavigationItem("Cihazlarým", new SymbolIcon(Symbol.AllApps), "CustomerDevices");
                AddNavigationItem("Þikayetlerim", new SymbolIcon(Symbol.ReportHacked), "CustomerComplaints");
                break;

            case UserRole.CustomerSupport:
            case UserRole.Technician:
                AddNavigationItem("Þikayetler", new SymbolIcon(Symbol.ReportHacked), "Complaints");
                break;

            default:
                break;
        }
    }

    private void AddNavigationItem(string content, IconElement icon, string tag)
    {
        var item = new NavigationViewItem
        {
            Content = content,
            Icon = icon,
            Tag = tag
        };

        //item.Tapped += NavigationViewItem_Tapped;
        NavigationPageNavigationView.MenuItems.Add(item);
    }

    private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
    {
        Business.UserSessionManagement.Instance.LogOut();
        Frame.Navigate(typeof(LoginPage));
        Frame.BackStack.Clear();
    }

    //private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
    //{
    //    var selectedItem = (NavigationViewItem)sender;

    //    switch (selectedItem.Tag)
    //    {

    //        case "Devices":
    //            NavigationPageFrame.Navigate(typeof(AdminDevices));
    //            break;
    //        case "Complaints":
    //            NavigationPageFrame.Navigate(typeof(AdminComplaints));
    //            break;
    //        case "Customers":
    //            NavigationPageFrame.Navigate(typeof(Customers));
    //            break;
    //        case "Supports":
    //            NavigationPageFrame.Navigate(typeof(Supports));
    //            break;
    //        case "CustomerDevices":
    //            NavigationPageFrame.Navigate(typeof(CustomerDevices));
    //            break;
    //        case "CustomerComplaints":
    //            NavigationPageFrame.Navigate(typeof(CustomerComplaints));
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
