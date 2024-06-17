using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MosoblgasSensorManager.Context;
using MosoblgasSensorManager.Models;

namespace MosoblgasSensorManager.Pages
{
    public partial class TableSensors : UserControl
    {
        private readonly ContextDB _context;

        public TableSensors()
        {
            InitializeComponent();
            _context = App.ServiceProvider.GetService<ContextDB>();
            LoadData();
        }

        public ObservableCollection<Sensor> Sensors { get; set; }

        private void LoadData()
        {
            var data = _context.Sensors.Include(s => s.Location).ToList();
            Sensors = new ObservableCollection<Sensor>(data);
            dg.ItemsSource = Sensors;
        }

        private void AddSensor(object sender, RoutedEventArgs e)
        {
            var newLocationAddress = "Новый адрес";

            var newSensor = new Sensor
            {
                SerialNumber = "Новый серийный номер",
                InstallationDate = DateOnly.FromDateTime(DateTime.Now),
                Status = "Новый статус",
                Type = "Новый тип",
                Location = new MosoblgasSensorManager.Models.Location { Address = newLocationAddress }
            };

            Sensors.Add(newSensor);
            _context.Sensors.Add(newSensor);
        }

        private void DeleteSensor(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem is Sensor selectedSensor)
            {
                Sensors.Remove(selectedSensor);
                _context.Sensors.Remove(selectedSensor);
            }
        }

        private async void SaveChanges(object sender, RoutedEventArgs e)
        {
            await _context.SaveChangesAsync();
            LoadData();
        }

        private void CbFilter_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selectedStatus = (cbFilter.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedStatus == "Все")
            {
                dg.ItemsSource = Sensors;
            }
            else
            {
                var filteredSensors = Sensors.Where(s => s.Status == selectedStatus).ToList();
                dg.ItemsSource = filteredSensors;
            }
        }

        private void TbSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            var searchQuery = tbSearch.Text.ToLower();
            var filteredSensors = Sensors.Where(s => s.SerialNumber.ToLower().Contains(searchQuery)).ToList();
            dg.ItemsSource = filteredSensors;
        }
    }
}
