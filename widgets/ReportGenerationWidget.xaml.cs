﻿using laboratory.database;
using laboratory.interfaces;
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
    /// Логика взаимодействия для ReportGenerationWidget.xaml
    /// </summary>
    public partial class ReportGenerationWidget : Page, IWidget
    {
        public Page ParentPage { get; set; }

        public ReportGenerationWidget(user owner, Page parent)
        {
            InitializeComponent();
            DataContext = owner;
            parentPage = parent;
        }
    }
}