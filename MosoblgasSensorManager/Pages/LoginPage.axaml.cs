using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using MosoblgasSensorManager.Context;
using MsBox.Avalonia;
using Microsoft.EntityFrameworkCore;

namespace MosoblgasSensorManager.Pages
{
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            string enteredLogin = login.Text;
            string enteredPassword = password.Text;

            using SHA256 sha256 = SHA256.Create();
            if (string.IsNullOrEmpty(enteredLogin) || string.IsNullOrEmpty(enteredPassword))
            {
                await MessageBoxManager.GetMessageBoxStandard("Подтверждение", "Введите логин или пароль").ShowAsync();
                return;
            }

            byte[] hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
            string hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

            // Создаем новый контекст для проверки логина и пароля
            using (var scope = App.ServiceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ContextDB>();
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == enteredLogin);

                if (user != null && user.Password == hashedPassword)
                {
                    var mainWindow = (MainWindow)this.VisualRoot;
                    var menu = new Menu();
                    mainWindow.ContentArea.Content = menu;
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Неверный логин или пароль.", "Подтверждение").ShowAsync();
                }
            }
        }
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