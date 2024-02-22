using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using App3.Business.API;
using App3.Business.Models;
using App3.Pages.Account;
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

namespace App3.Pages.ControlPages.Admin;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Customers : Page
{
    public Customers()
    {
        this.InitializeComponent();
    }

    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        ContentDialog dialog = null;

        if (EmailTextBox.Text == "" || PasswordTextBox.Password == "" || NameTextBox.Text == "" || SurnameTextBox.Text == "" || PhoneTextBox.Text == "")
        {
            dialog = new ContentDialog
            {
                Title = "Hata",
                Content = "Lütfen tüm alanlarý doldurun",
                CloseButtonText = "Kapat",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
            return;
        }
        var user = new User
        {
            Email = EmailTextBox.Text,
            PasswordHash = PasswordTextBox.Password,
            Name = NameTextBox.Text,
            Surname = SurnameTextBox.Text,
            Phone = PhoneTextBox.Text,
        };

        var service = new AccountService();
        var response = await service.GetRegisterService(user);

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                EmailErrorTextBlock.Visibility = Visibility.Collapsed;
                PasswordErrorTextBlock.Visibility = Visibility.Collapsed;
                PhoneErrorTextBlock.Visibility = Visibility.Collapsed;
                // create dialog and it navigates to the login page
                dialog = new ContentDialog
                {
                    Title = "Baþarýlý",
                    Content = "Kullanýcý baþarýyla eklendi",
                    CloseButtonText = "Kapat",
                    XamlRoot = Content.XamlRoot
                };
                break;

            case System.Net.HttpStatusCode.BadRequest:

                try
                {
                    var content = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

                    if (content.ContainsKey("Email"))
                    {
                        EmailErrorTextBlock.Text = content["Email"];
                        EmailErrorTextBlock.Visibility = Visibility.Visible;
                    }
                    if (content.ContainsKey("Password"))
                    {
                        PasswordErrorTextBlock.Text = content["Password"];
                        PasswordErrorTextBlock.Visibility = Visibility.Visible;
                    }
                    if (content.ContainsKey("Phone"))
                    {
                        PhoneErrorTextBlock.Text = content["Phone"];
                        PhoneErrorTextBlock.Visibility = Visibility.Visible;
                    }
                    break;
                }
                catch (Exception)
                {
                    dialog = new ContentDialog
                    {

                        Title = "Hata",
                        Content = "Kullanýcý Eklenemedi",
                        CloseButtonText = "Kapat",
                        XamlRoot = Content.XamlRoot
                    };
                    break;
                }

            default:
                EmailErrorTextBlock.Visibility = Visibility.Collapsed;
                PasswordErrorTextBlock.Visibility = Visibility.Collapsed;
                PhoneErrorTextBlock.Visibility = Visibility.Collapsed;
                dialog = new ContentDialog
                {
                    Title = "Hata",
                    Content = "Kullanýcý Eklenemedi",
                    CloseButtonText = "Kapat",
                    XamlRoot = Content.XamlRoot
                };
                break;
        }

        if (dialog != null)
        {
            await dialog.ShowAsync();
        }
    }
}
