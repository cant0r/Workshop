﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_Technician.CustomControls
{
    /// <summary>
    /// Interaction logic for RepairLogEntry.xaml
    /// </summary>
    public partial class RepairLogEntry : UserControl
    {
        public bool Modified { get; set; }
        public long LogId { get; set; }
        public RepairLogEntry()
        {
            InitializeComponent();
            Modified = false;
            logTblock.TextChanged += (object sender, TextChangedEventArgs args) =>
            {
                Modified = true;
            };
        }
    }
}
