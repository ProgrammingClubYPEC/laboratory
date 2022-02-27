using laboratory.database;
using laboratory.interfaces;
using laboratory.widgets.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laboratory.widgets
{
    /// <summary>
    /// Логика взаимодействия для IntakeBiomaterialsWidget.xaml
    /// </summary>
    public partial class IntakeBiomaterialsWidget : Page, IWidget, IErrorMessage, IFieldble
    {
        public IPage ParentPage { get; set; }

        private IConfigWidget _configWidget;
        public IConfigWidget CurrentConfigWidget 
        { 
            get { return _configWidget; }
            set 
            {
                if (_configWidget == value)
                    return;

                _configWidget = value;
                configWidgetCard.Content = _configWidget;
            }
        }

        private string _errorMessageString;
        public string MessageErrorString 
        { 
            get { return _errorMessageString; }
            set
            {
                _errorMessageString = value;
                ErrorMessageText.Text = value;
            }
        }

        public List<IConfigWidget> ConfigWidgets { get; set; }

        private BiomaterialsOrderConfigWidget biomaterialsOrderConfigWidget;
        private AddingNewUserConfigWidget addingNewUserConfigWidget;

        public IntakeBiomaterialsWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;

            biomaterialsOrderConfigWidget = new BiomaterialsOrderConfigWidget(owner, this);
            addingNewUserConfigWidget = new AddingNewUserConfigWidget(owner, this);
            ConfigWidgets = new List<IConfigWidget> { biomaterialsOrderConfigWidget, addingNewUserConfigWidget };

            exportBarcodeBtn.IsEnabled = false;
            scanBarcodeIcon.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/icons/barcode.png")));
            exportBarcodeIcon.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/icons/printer.png")));
        }

        public void UpdateData()
        {
            Instance.Reload();
        }

        public void ChangeConfigWidget<T>()
        {
            foreach (IConfigWidget widget in ConfigWidgets)
                if (typeof(T).Equals(widget.GetType()))
                    CurrentConfigWidget = widget;
        }

        private void exportBarcodeBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageErrorString("Данный функционал не реализован");
        }

        private void scanBarcodeBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageErrorString("Необнаружено устройство для сканирования штрих-кода!");
        }

        private void biomaterialCodeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                CurrentConfigWidget = biomaterialsOrderConfigWidget;
                barcodeImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/barcode.png")));
                exportBarcodeBtn.IsEnabled = true;
            }    
        }

        public async void ShowMessageErrorString(string errorString)
        {
            MessageErrorString = errorString;
            await Task.Delay(5000);
            MessageErrorString = string.Empty;
        }

        public void ClearFields()
        {
            biomaterialCodeText.Text = String.Empty;
            CurrentConfigWidget = null;
            barcodeImage.Source = null;
            (biomaterialsOrderConfigWidget as IFieldble).ClearFields();
            (addingNewUserConfigWidget as IFieldble).ClearFields();
        }
    }
}
