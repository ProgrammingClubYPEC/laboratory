﻿using laboratory.database;
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
    /// Логика взаимодействия для InvoiceGenerationWidget.xaml
    /// </summary>
    public partial class InvoiceGenerationWidget : Page
    {
        private Page parentPage;

        public InvoiceGenerationWidget(user owner, Page parent)
        {
            InitializeComponent();
            DataContext = owner;
            parentPage = parent;
        }
    }
}
