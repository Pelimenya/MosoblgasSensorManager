using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace MosoblgasSensorManager.Pages;

public partial class Menu : UserControl
{
    public Menu()
    {
        InitializeComponent();
    }


    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        SV.IsPaneOpen = !SV.IsPaneOpen;
    }

    private void NavigateToTableUser(object sender, RoutedEventArgs e)
    {
        TableUser.Content = new TableUser();
    }

    private void NavigateToTableSensors(object sender, RoutedEventArgs e)
    {
        TableUser.Content = new TableSensors();
    }
}