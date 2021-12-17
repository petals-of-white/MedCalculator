using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
namespace Калькулятор
{
    //екскреція натрію
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<WatermarkTextBox> textBoxes;

        public MainWindow()
        {
            InitializeComponent();

            textBoxes = new();

            textBoxes.Add(CreaSyr);
            textBoxes.Add(NaUrine);
            textBoxes.Add(NaSyr);
            textBoxes.Add(CreaUrine);

        }

        private void WatermarkTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            int keyNumber = (int) e.Key;

            if (((keyNumber >= 34) && (keyNumber <= 43))
                   ||
               ((keyNumber >= 74) && (keyNumber <= 83)))
            {

            }
            else
                e.Handled = true;


        }
        private void CalculateOnTextChange(object sender, TextChangedEventArgs e)
        {

            int parseTemp;

            var valuesInBoxes = (from textbox in textBoxes
                                 select int.TryParse(textbox.Text, out parseTemp) ? parseTemp : -1).ToArray();


            if (!valuesInBoxes.Contains(-1))
            {
                double fractExcretion = SodiumExcretion.CalculateFractExcretion(
                    valuesInBoxes [0], valuesInBoxes [1], valuesInBoxes [2], valuesInBoxes [3]);

                var interpretation = SodiumExcretion.InterpretFractExcretion(fractExcretion);


                Application.Current.MainWindow.Resources ["FractExtraction"] = fractExcretion;


                Application.Current.MainWindow.Resources ["Diagnosis"] = interpretation.Item1;
                Application.Current.MainWindow.Resources ["Explanation"] = interpretation.Item2;

            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var textbox in textBoxes)
                textbox.Text = "";
        }


    }

    public class PercentageToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush(Colors.Lavender);
            if (parameter.ToString() == "title")
            {
                double trueValue = System.Convert.ToDouble(value?.ToString()?.TrimStart('0'));

                if ((trueValue) < 1)
                    brush.Color = Colors.Lavender;
                else if ((trueValue >= 1) && (trueValue <= 4))
                    brush.Color = Colors.LemonChiffon;
                else
                    brush.Color = Colors.WhiteSmoke;

            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
