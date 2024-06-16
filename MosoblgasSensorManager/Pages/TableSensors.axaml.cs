using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using MosoblgasSensorManager.Models;
using MosoblgasSensorManager.Context;

namespace MosoblgasSensorManager.Pages
{
    public partial class TableSensors : UserControl
    {
        private readonly ContextDB _context;

        public TableSensors()
        {
            InitializeComponent();
            _context = App.ServiceProvider.GetService<ContextDB>();
            Data();
        }

        // ObservableCollection для хранения данных сенсоров
        public ObservableCollection<Sensor> Sensors { get; set; }

        // Метод для загрузки данных из базы данных и привязки к DataGrid
        public void Data()
        {
            var data = _context.Sensors.ToList(); // Загрузка данных из базы данных
            Sensors = new ObservableCollection<Sensor>(data); // Инициализация ObservableCollection
            dg.ItemsSource = Sensors; // Установка ItemsSource для DataGrid
        }

        // Обработчик события для добавления сенсора
        private void AddSensor(object sender, RoutedEventArgs e)
        {
            var newSensor = new Sensor 
            { 
                SerialNumber = "Новый серийный номер", 
                InstallationDate = DateOnly.FromDateTime(DateTime.Now), 
                Status = "Новый статус", 
                Type = "Новый тип" 
            };
            Sensors.Add(newSensor); // Добавление нового сенсора в ObservableCollection
            _context.Sensors.Add(newSensor); // Добавление нового сенсора в базу данных
        }

        // Обработчик события для удаления сенсора
        private void DeleteSensor(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem is Sensor selectedSensor)
            {
                Sensors.Remove(selectedSensor); // Удаление сенсора из ObservableCollection
                _context.Sensors.Remove(selectedSensor); // Удаление сенсора из базы данных
            }
        }

        // Обработчик события для сохранения изменений
        private async void SaveChanges(object sender, RoutedEventArgs e)
        {
            await _context.SaveChangesAsync(); // Сохранение изменений в базе данных асинхронно
            Data(); // Обновление данных в ObservableCollection
        }
    }
}
