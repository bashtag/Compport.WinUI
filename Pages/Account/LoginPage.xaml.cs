using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using App3.Business.API;
using App3.Business.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App3.Pages.Account;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LoginPage : Page
{
    private readonly AccountService accountService;

    public LoginPage()
    {
        this.InitializeComponent();
        accountService = new AccountService();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var (responseText, user) = await accountService.GetLoginService(EmailTextBox.Text, PasswordTextBox.Password);

        if (user != null)
        {
            // Set the user session
            Business.UserSessionManagement.Instance.LogIn(user);
            // Navigate to MainPage
            //Frame.Navigate(typeof(RouteSuccess), sender);
            Frame.Navigate(typeof(NavigationPage));
        }
        else if (responseText != null)
        {
            ErrorTextBlock.Text = responseText;
            ErrorTextBlock.Visibility = Visibility.Visible;
            //var error = new TextBlock
            //{
            //    Text = responseText,
            //    Padding = new Thickness(10),
            //    FontFamily = new FontFamily("Arial"),
            //    FontStretch = Windows.UI.Text.FontStretch.UltraCondensed,
            //    FontStyle = Windows.UI.Text.FontStyle.Italic,
            //    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0))
            //};

            //RootStackPanel.Children.Insert(3, error);
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(RegisterPage));
    }
}
