using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace App3.Pages.ControlPages.Admin;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AdminDevices : Page
{

    // Define the list of suggestions
    private readonly List<string> graphicsCardSuggestions = new()
    {
        "NVIDIA H100 Tensor Core GPU",
        "NVIDIA GeForce RTX 4090",
        "NVIDIA GeForce RTX 3090",
        "NVIDIA GeForce RTX 3080",
        "NVIDIA GeForce RTX 3070",
        "NVIDIA GeForce RTX 3060",
        "NVIDIA GeForce RTX 3050",
        "AMD Radeon RX 6900 XT",
        "AMD Radeon RX 6800 XT",
        "AMD Radeon RX 6700 XT",
        "AMD Radeon RX 6600 XT"
    };

    private readonly DeviceManagementService _deviceManagementService = new();

    public AdminDevices()
    {
        this.InitializeComponent();
    }

    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        ContentDialog dialog = null;

        // Get the values
        var brandName = BrandNameComboBox.SelectedValue != null ? BrandNameComboBox.SelectedValue.ToString() : null;
        var modelNumber = ModelNumberTextBox.Text;
        var modelName = ModelNameTextBox.Text;
        var processor = Processor.SelectedValue != null ? Processor.SelectedValue.ToString() : null;
        var ram = RAM.SelectedValue != null ? RAM.SelectedValue.ToString() : null;
        var storage = Storage.SelectedValue != null ? Storage.SelectedValue.ToString() : null;
        var operatingSystem = OperatingSystem.SelectedValue != null ? OperatingSystem.SelectedValue.ToString() : null;
        var display = Display.SelectedValue != null ? Display.SelectedValue.ToString() : null;
        var refreshRate = RefreshRate.SelectedValue != null ? RefreshRate.SelectedValue.ToString() : null;
        var panelType = PanelType.SelectedValue != null ? PanelType.SelectedValue.ToString() : null;
        var wifiCard = WiFiCard.SelectedValue != null ? WiFiCard.SelectedValue.ToString() : null;
        var graphicsCard = GraphicsCardAutoSuggestBox.Text;
        var description = DescriptionTextBox.Text;
        var releaseDate = ReleaseDateCalendarDatePicker.Date;

        // the form valid?
        if (string.IsNullOrEmpty(brandName) ||
            string.IsNullOrEmpty(modelNumber) ||
            string.IsNullOrEmpty(modelName) ||
            string.IsNullOrEmpty(processor) ||
            string.IsNullOrEmpty(ram) ||
            string.IsNullOrEmpty(storage) ||
            string.IsNullOrEmpty(operatingSystem) ||
            string.IsNullOrEmpty(display) ||
            string.IsNullOrEmpty(refreshRate) ||
            string.IsNullOrEmpty(panelType) ||
            string.IsNullOrEmpty(wifiCard) ||
            string.IsNullOrEmpty(graphicsCard) ||
            string.IsNullOrEmpty(description) ||
            releaseDate == null)
        {
            dialog = new ContentDialog
            {
                Title = "Hata",
                Content = "Lütfen tüm alanlarý doldurun",
                CloseButtonText = "Kapat",
                XamlRoot = Content.XamlRoot
            };
        }
        else
        {
            // set the model
            var computerModel = new ComputerModel
            {
                BrandName = brandName,
                ModelNumber = modelNumber,
                ModelName = modelName,
                ProcessorDescription = processor,
                RamDescription = ram,
                StorageDescription = storage,
                OperatingSystemDescription = operatingSystem,
                DisplayDescription = display + " " + refreshRate + " " + panelType,
                WifiCardDescription = wifiCard,
                GraphicsCardDescription = graphicsCard,
                Description = description,
                ReleaseDate = releaseDate.HasValue ? releaseDate.Value.DateTime : DateTime.Now,
                BatteryDescription = BatteryValueText.Text,
                Weight = WeightValueText.Text.Length > 0
                    ? double.Parse(WeightValueText.Text.Substring(0, WeightValueText.Text.Length - 2), CultureInfo.InvariantCulture)
                    : 0.0
            };

            // post to api
            var (text, success) = await _deviceManagementService.AddComputerModelAsync(computerModel);
            if (success)
            {
                dialog = new ContentDialog
                {
                    Title = "Baþarýlý",
                    Content = "Cihaz baþarýyla eklendi",
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

        }
        if (dialog != null)
        {
            await dialog.ShowAsync();
        }
    }

    private void WeightSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (WeightValueText != null) // Check if the TextBlock is initialized to avoid null reference exceptions
        {
            WeightValueText.Text = $"{e.NewValue.ToString("F2", CultureInfo.InvariantCulture)}kg";
        }
    }

    private void BatterySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (BatteryValueText != null) // Check if the TextBlock is initialized to avoid null reference exceptions
        {
            BatteryValueText.Text = $"{e.NewValue}Wh";
        }
    }

    private void GraphicsCardAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Only get results when it was a user typing
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            sender.ItemsSource = graphicsCardSuggestions.Where(s => s.ToUpper().Contains(sender.Text.ToUpper())).ToList();
        }
    }

    private void GraphicsCardAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        if (args.SelectedItem is string selectedCard)
        {
            sender.Text = selectedCard;
        }
    }
}
