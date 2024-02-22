using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace App3.Pages.ControlPages.Admin;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AdminComplaints : Page
{
    private readonly ComputerModelViewModel _computerModelViewModel = new();
    private readonly ComplaintManagementService _complaintManagementService = new();

    public AdminComplaints()
    {
        InitializeComponent();
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
        if (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
        {
            PhoneErrorTextBlock.Text = "Lütfen telefon numarasý girin";
            PhoneErrorTextBlock.Visibility = Visibility.Visible;
            hasError = true;
        }

        if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
        {
            DescriptionErrorTextBlock.Text = "Lütfen þikayet girin";
            DescriptionErrorTextBlock.Visibility = Visibility.Visible;
            hasError = true;
        }

        if (string.IsNullOrWhiteSpace(ModelAutoSuggestBox.Text) || string.IsNullOrWhiteSpace(SerialNumberTextBox.Text))
        {
            DeviceInfoErrorTextBlock.Text = "Lütfen cihaz bilgilerini girin";
            DeviceInfoErrorTextBlock.Visibility = Visibility.Visible;
            hasError = true;
        }

        return hasError;
    }

    private async void SubmitComplaint_Click(object sender, RoutedEventArgs e)
    {
        var hasError = IsInputValid();

        if (!hasError)
        {
            var device = new Device
            {
                ComputerModelId = _computerModelViewModel.ComputerModels.FirstOrDefault(p => p.ModelNumber == ModelAutoSuggestBox.Text).Id,
                SerialNumber = SerialNumberTextBox.Text
            };
            var complaint = new Complaint
            {
                PhoneNumber = PhoneNumberTextBox.Text,
                Description = DescriptionTextBox.Text,
                Device = device
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
