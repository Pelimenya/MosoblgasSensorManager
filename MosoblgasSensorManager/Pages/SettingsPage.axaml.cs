using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using System.Linq;
using Avalonia;

namespace MosoblgasSensorManager.Pages
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ThemeRadioButton_Checked(object? sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Tag is string theme)
            {
                // Применяем выбранную тему
                if (theme == "Light")
                {
                    Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
                }
                else if (theme == "Dark")
                {
                    Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
                }
            }
        }

        private void LanguageComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem &&
                selectedItem.Tag is string languageCode)
            {
            }
        }
    }
}