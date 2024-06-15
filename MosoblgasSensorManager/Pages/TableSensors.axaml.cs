using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MosoblgasSensorManager.Pages;

public partial class TableSensors : UserControl
{
    public TableSensors()
    {
        InitializeComponent();
        dg.ItemsSource = App.DB.Sensors.ToList();
    }
}