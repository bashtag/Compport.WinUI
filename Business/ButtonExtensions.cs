using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace App3.Business;

public static class ButtonExtensions
{
    public static readonly DependencyProperty HoverBackgroundProperty =
        DependencyProperty.RegisterAttached(
            "HoverBackground",
            typeof(Brush),
            typeof(ButtonExtensions),
            new PropertyMetadata(false, OnHoverBackgroundChanged)
        );

    public static Brush GetHoverBackground(DependencyObject obj) => (Brush)obj.GetValue(HoverBackgroundProperty);

    public static void SetHoverBackground(DependencyObject obj, Brush value) => obj.SetValue(HoverBackgroundProperty, value);

    private static void OnHoverBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Button button)
        {
            button.PointerEntered += Button_HoverEntered;
            button.PointerExited += Button_HoverExited;
        }
    }

    private static void Button_HoverEntered(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var hoverBrush = GetHoverBackground(button);
        if (hoverBrush != null)
        {
            button.Tag = button.Background;
            button.Background = hoverBrush;
        }
    }

    private static void Button_HoverExited(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        button.ClearValue(Button.BackgroundProperty);
        //if (sender is Button button && button.Tag is Brush originalBackground)
        //{
        //    button.Background = originalBackground;
        //}
    }

    //private static void Button_GotFocused(object sender, RoutedEventArgs e)
    //{
    //    if (sender is Button button)
    //    {
    //        if (button.Tag is Brush color && color != button.Background)
    //        {
    //            button.Tag = button.Background;
    //        }
    //        button.Background = new SolidColorBrush(Colors.White);
    //    }
    //}


    //private static void Button_LostFocused(object sender, RoutedEventArgs e)
    //{
    //    if (sender is Button button && button.Tag is Brush originalBackground)
    //    {
    //        button.Background = originalBackground;
    //    }
    //}
}
