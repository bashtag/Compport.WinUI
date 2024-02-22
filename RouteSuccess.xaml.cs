using App3.Pages.Account;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App3;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RouteSuccess : Page
{
    public RouteSuccess()
    {
        this.InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Business.UserSessionManagement.Instance.LogOut();
        Frame.Navigate(typeof(LoginPage));
        Frame.BackStack.Clear();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        var prevButton = (Button)e.Parameter;

        UserNameTextBlock.Text = Business.UserSessionManagement.Instance.User.Email;
        PasswordTextBlock.Text = Business.UserSessionManagement.Instance.User.PasswordHash;

        var textBlock = new TextBlock
        {
            Text = prevButton.Content.ToString(),
            Width = 400,
            Height = 100,
            Margin = new Thickness(0, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 15,
            TextAlignment = TextAlignment.Center
        };

        // Create a new button to navigate to the MainWindow
        var button = new Button
        {
            Content = "Go to main window",
            Width = 200,
            Height = 100,
            Margin = new Thickness(0, 100, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 20,
            Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 255)),
            Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255)),
            BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0)),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(10),
            FontFamily = new FontFamily("Arial"),
            FontStretch = Windows.UI.Text.FontStretch.UltraCondensed,
            FontStyle = Windows.UI.Text.FontStyle.Italic
        };
        button.Click += Button_Click;

        // Assuming the name of your Grid is "LayoutRoot"
        // You need to add this Grid to your XAML with x:Name="LayoutRoot"
        LayoutRoot.Children.Add(button);
        LayoutRoot.Children.Add(textBlock);


        // Optionally, if you want to set the button's position dynamically, you can use Grid.SetRow and Grid.SetColumn here
        // For example, if your Grid has defined rows/columns, you might want to place the button in a specific cell
    }

}
