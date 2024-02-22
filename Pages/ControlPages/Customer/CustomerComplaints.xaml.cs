using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using App3.Business;
using App3.Business.API;
using App3.Business.Models;
using App3.ViewModels;
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

namespace App3.Pages.ControlPages.Customer;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CustomerComplaints : Page
{
    private readonly ComputerModelViewModel _computerModelViewModel = new();
    private readonly ComplaintManagementService _complaintManagementService = new();
    private readonly DeviceManagementService _deviceManagementService = new();
    private ICollection<Device> _devices;

    public CustomerComplaints()
    {
        this.InitializeComponent();
        LoadDevicesAsync();
    }

    private async void LoadDevicesAsync()
    {
        // Example: Fetch devices from your backend or database
        _devices = await _deviceManagementService.GetUserDevices(UserSessionManagement.Instance.User.Id);
        DevicesComboBox.ItemsSource = _devices;
    }

    private void ModelAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && _computerModelViewModel.ComputerModels != null)
        {
            // filter and lower case
            var suggestions = _computerModelViewModel
                .ComputerModels
                .Where(p => p.ModelNumber.ToLowerInvariant().Contains(sender.Text.ToLowerInvariant()))
                .Select(p => p.ModelNumber);

            sender.ItemsSource = suggestions;
        }
    }

    private void ModelAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (args.SelectedItem is string selectedCard && _computerModelViewModel.ComputerModels != null)
        {
            sender.Text = selectedCard;
        }
    }

    private bool IsInputValid()
    {
        var hasError = false;

        if (DevicesComboBox.SelectedItem == null)
        {
            DevicesErrorTextBlock.Text = "Lütfen cihaz seçin";
            DevicesErrorTextBlock.Visibility = Visibility.Visible;
            hasError = true;
        }
        else
        {
            DevicesErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
        {
            DescriptionErrorTextBlock.Text = "Lütfen þikayet girin";
            DescriptionErrorTextBlock.Visibility = Visibility.Visible;
            hasError = true;
        }
        else
        {
            DescriptionErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        return hasError;
    }

    private async void SubmitComplaint_Click(object sender, RoutedEventArgs e)
    {
        var hasError = IsInputValid();

        if (!hasError)
        {
            var complaint = new Complaint
            {
                UserId = UserSessionManagement.Instance.User.Id,
                Description = DescriptionTextBox.Text,
                DeviceId = ((Device)DevicesComboBox.SelectedItem).Id
            };
            var (text, result) = await _complaintManagementService.AddComplaintAsync(complaint);

            ContentDialog dialog = null;
            if (result)
            {
                dialog = new ContentDialog
                {
                    Title = "Baþarýlý",
                    Content = "Þikayet baþarýyla eklendi",
                    CloseButtonText = "Kapat",
                    XamlRoot = Content.XamlRoot
                };
            }
            else
            {
                dialog = new ContentDialog
                {
                    Title = "Hata",
                    Content = text,
                    CloseButtonText = "Kapat",
                    XamlRoot = Content.XamlRoot
                };
            }

            await dialog.ShowAsync();
        }
    }
}
