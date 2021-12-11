using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Калькулятор
{
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


            //foreach (WatermarkTextBox textBox in textBoxes)
            //    textBox.KeyDown += CalculateOnKeyDown;


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

        private void CalculateOnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var box = sender as WatermarkTextBox;
            int keyNumber = (int) e.Key;

            if (((keyNumber >= 34) && (keyNumber <= 43))
                   ||
               ((keyNumber >= 74) && (keyNumber <= 83)))
            {
                box.KeyDown += CalculateOnKeDownWow;
                //int parseTemp;

                //var valuesInBoxes = (from textbox in textBoxes
                //                     select int.TryParse(textbox.Text, out parseTemp) ? parseTemp : -1).ToArray();


                //if (!valuesInBoxes.Contains(-1))
                //{
                //    double fractExcretion = CalculateFractExcretion(
                //        valuesInBoxes [0], valuesInBoxes [1], valuesInBoxes [2], valuesInBoxes [3]);

                //    var interpreation = InterpretFractExcretion(fractExcretion);
                //    Percentage.Inlines.Clear();
                //    Percentage.Inlines.Add(fractExcretion + "%");

                //    Diagnosis.Inlines.Clear();
                //    Diagnosis.Inlines.Add(interpreation.Item1);

                //    Explanation.Inlines.Clear();
                //    Explanation.Inlines.Add(interpreation.Item2);
                //}


            }

        }

        private void CalculateOnKeDownWow(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int keyNumber = (int) e.Key;


            int parseTemp;

            var valuesInBoxes = (from textbox in textBoxes
                                 select int.TryParse(textbox.Text, out parseTemp) ? parseTemp : -1).ToArray();


            if (!valuesInBoxes.Contains(-1))
            {
                double fractExcretion = CalculateFractExcretion(
                    valuesInBoxes [0], valuesInBoxes [1], valuesInBoxes [2], valuesInBoxes [3]);

                var interpreation = InterpretFractExcretion(fractExcretion);
                Percentage.Inlines.Clear();
                Percentage.Inlines.Add(fractExcretion + "%");

                Diagnosis.Inlines.Clear();
                Diagnosis.Inlines.Add(interpreation.Item1);

                Explanation.Inlines.Clear();
                Explanation.Inlines.Add(interpreation.Item2);



            }
        }

        private void CalculateOnTextChange(object sender, TextChangedEventArgs e)
        {

            int parseTemp;

            var valuesInBoxes = (from textbox in textBoxes
                                 select int.TryParse(textbox.Text, out parseTemp) ? parseTemp : -1).ToArray();


            if (!valuesInBoxes.Contains(-1))
            {
                double fractExcretion = CalculateFractExcretion(
                    valuesInBoxes [0], valuesInBoxes [1], valuesInBoxes [2], valuesInBoxes [3]);

                var interpreation = InterpretFractExcretion(fractExcretion);
                Percentage.Inlines.Clear();
                Percentage.Inlines.Add($"{fractExcretion:f2}" + "%");

                Diagnosis.Inlines.Clear();
                Diagnosis.Inlines.Add(interpreation.Item1);

                Explanation.Inlines.Clear();
                Explanation.Inlines.Add(interpreation.Item2);



            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var textbox in textBoxes)
                textbox.Text = "";
        }

        public static double CalculateFractExcretion(int SCr, int UNa, int SNa, int UCr)
        => 100 * ((double) SCr / 1000 * UNa) / (SNa * (double) UCr / 1000);

        public static (string, string) InterpretFractExcretion(double fractExcretion)
        {
            string oliguriaType, additionalInfo;

            if (fractExcretion < 1)
                (oliguriaType, additionalInfo) = ("Преренальна", "");
            else if ((fractExcretion >= 1) && (fractExcretion <= 4))
                (oliguriaType, additionalInfo) = ("Ренальна", "");
            else
                (oliguriaType, additionalInfo) = ("Постренальна", "");

            return (oliguriaType, additionalInfo);
        }
    }
}
