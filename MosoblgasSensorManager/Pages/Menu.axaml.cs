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
        ContentFrame.Content = new AboutPage();
    }


    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        SV.IsPaneOpen = !SV.IsPaneOpen;
    }

    private void NavigateToTableUser(object sender, RoutedEventArgs e)
    {
        ContentFrame.Content = new TableUser();
    }

    private void NavigateToTableSensors(object sender, RoutedEventArgs e)
    {
        ContentFrame.Content = new TableSensors();
    }

    private void NavigateToSettings(object? sender, RoutedEventArgs e)
    {
        ContentFrame.Content = new SettingsPage();
    }

    private void NavigateToAbout(object? sender, RoutedEventArgs e)
    {
        ContentFrame.Content = new AboutPage();
    }

    private void NavigateToLoginPage(object? sender, RoutedEventArgs e)
    {
        var mainWindow = (MainWindow)this.VisualRoot;
        mainWindow.ContentArea.Content = new LoginPage();
    }
}