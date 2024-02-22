using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using App3.Business.Models;
using App3.Business.API;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Json;
using System;
using Newtonsoft.Json;
using App3.Business.Models.Errors;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App3.Pages.Account;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RegisterPage : Page
{
    public RegisterPage()
    {
        this.InitializeComponent();
    }

    private async void RegisterButton_Click(object sender, RoutedEventArgs e)
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
                    CloseButtonText = "Giriþ Sayfasýna Git",
                    XamlRoot = Content.XamlRoot
                };
                dialog.CloseButtonClick += (s, e) => Frame.Navigate(typeof(LoginPage));
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

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }
}
