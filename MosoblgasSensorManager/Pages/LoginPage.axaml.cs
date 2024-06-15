using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;


namespace MosoblgasSensorManager.Pages;

public partial class LoginPage : UserControl
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        string enteredLogin = login.Text;
        string enteredPassword = password.Text;
        using SHA256 sha256 = SHA256.Create();
        if (string.IsNullOrEmpty(enteredLogin) || string.IsNullOrEmpty(enteredPassword))
        {
             MessageBoxManager.GetMessageBoxStandard( "Подтверждение","Введите логин или пароль" ).ShowAsync();
            return;
        }
        byte[] hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
        string hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
        var data = App.DB.Users.ToList();
        var user = data.FirstOrDefault(x => x.Username == enteredLogin);
        if (user != null && user.Password == hashedPassword)
        {
            var mainWindow = (MainWindow)this.VisualRoot;
            var menu = new Menu();
            mainWindow.ContentArea.Content = menu;
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Неверный логин или пароль.", "Подтверждение").ShowAsync();
        }
    }
    
       /* private async void MessageBox(string message)
        {
            var dialog = new Window
            {
                Content = new TextBlock { Text = message, Margin = new Thickness(20) },
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            await dialog.ShowDialog((Window)this.VisualRoot);
        }
        */
}