﻿using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MosoblgasSensorManager.Models;

namespace MosoblgasSensorManager.Pages;

public partial class TableUser : UserControl
{
    public TableUser()
    {
        InitializeComponent();
        Data();
    }
    public ObservableCollection<User> People { get; set; }

    public void Data()
    {
        var data = App.DB.Users.ToList();
        People = new ObservableCollection<User>(data);
        dg.ItemsSource = People;
    }



 
}