using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MosoblgasSensorManager.Models;
using MosoblgasSensorManager.Context;

namespace MosoblgasSensorManager.Pages
{
    public partial class TableUser : UserControl
    {
        private ContextDB _context;
        
        public TableUser()
        {
            InitializeComponent();
            _context = App.ServiceProvider.GetService<ContextDB>();
            Data();
        }

        public ObservableCollection<User> People { get; set; }

        public void Data()
        {
            var data = _context.Users.ToList();
            People = new ObservableCollection<User>(data);
            dg.ItemsSource = People;
        }

        // Обработчик события для добавления пользователя
        private void AddUser(object sender, RoutedEventArgs e)
        {
            var newUser = new User { Username = "Новый пользователь", Password = "Пароль", Role = "Роль" };
            People.Add(newUser);
            _context.Users.Add(newUser);
        }

        // Обработчик события для удаления пользователя
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem is User selectedUser)
            {
                People.Remove(selectedUser);
                _context.Users.Remove(selectedUser);
            }
        }

        // Обработчик события для сохранения изменений
        private async void SaveChanges(object sender, RoutedEventArgs e)
        {
            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();
            
            // Обновление данных в ObservableCollection
            Data();
        }

        private void TbSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            var searhItem = tbSearch.Text.ToLower();
            dg.ItemsSource = People.Where(x => x.Username.ToLower().Contains(searhItem));
        }

        private void Refresh(object? sender, RoutedEventArgs e)
        {
           
            Data();
            dg.ItemsSource = People;
        }
    }
}