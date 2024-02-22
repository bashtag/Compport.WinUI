using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using App3.ViewModels;
using App3.Business.API;
using App3.Business.Models;
using App3.Business;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App3.Pages.ControlPages.Customer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerDevices : Page
    {
        private readonly ComputerModelViewModel _computerModelViewModel = new();
        private readonly DeviceManagementService _deviceManagementService = new();

        public CustomerDevices()
        {
            this.InitializeComponent();

        }

        private void ModelAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = _computerModelViewModel
                    .ComputerModels
                    .Where(c => c.ModelNumber.ToLowerInvariant().Contains(sender.Text.ToLowerInvariant()))
                    .Select(c => c.ModelNumber);

                sender.ItemsSource = suggestions;
            }
        }

        private void ModelAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is string selected)
            {
                sender.Text = selected;
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = null;

            // Get the values
            var modelNumber = ModelAutoSuggestBox.Text;
            var serialNumber = SerialNumberTextBox.Text;

            // Validate the input
            if (string.IsNullOrWhiteSpace(modelNumber) || string.IsNullOrWhiteSpace(serialNumber))
            {
                dialog = new ContentDialog
                {
                    Title = "Hata",
                    Content = "Lütfen tüm alanlarý doldurunuz!",
                    CloseButtonText = "Kapat",
                    XamlRoot = this.Content.XamlRoot
                };
            }
            else
            {
                // Add the device
                var device = new Device
                {
                    ComputerModelId = _computerModelViewModel.ComputerModels.FirstOrDefault(c => c.ModelNumber == modelNumber).Id,
                    SerialNumber = serialNumber,
                    UserId = UserSessionManagement.Instance.User.Id
                };
                var (text, success) = await _deviceManagementService.AddDeviceAsync(device);

                if (!success)
                {
                    dialog = new ContentDialog
                    {
                        Title = "Hata",
                        Content = text,
                        CloseButtonText = "Kapat",
                        XamlRoot = this.Content.XamlRoot
                    };
                }
                else
                {
                    dialog = new ContentDialog
                    {
                        Title = "Baþarýlý",
                        Content = "Cihaz baþarýyla eklendi",
                        CloseButtonText = "Kapat",
                        XamlRoot = this.Content.XamlRoot
                    };
                }
            }

            // Show the dialog
            await dialog.ShowAsync();
        }

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            var debug = sender;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var debug = grid;
        }
    }
}
