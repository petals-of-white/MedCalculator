using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace Захист
{
    //Гідрогенкарбонат
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

            textBoxes.Add(Deficit);
            textBoxes.Add(BodyMass);



        }
        private void WatermarkTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        private void WatermarkTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            int keyNumber = (int) e.Key;



            var currentBox = (WatermarkTextBox) sender;

            if (
               ((keyNumber >= 34) && (keyNumber <= 43))
                   ||
               ((keyNumber >= 74) && (keyNumber <= 83))

               )
            {
                //do nothing

            }

            else
                if (e.Key == Key.OemMinus)
            {
                e.Handled = true;
                //(sender as WatermarkTextBox).Text = "";
            }


        }
        private void CalculateThings()
        {

            bool parseTemp1, parseTemp2;
            double [] valuesInBoxes = new double [2];

            try
            {
                //valuesInBoxes = (from textbox in textBoxes
                //                     select double.TryParse(textbox.Text.Substring(1, textbox.Text.Length - 1), out parseTemp) ? parseTemp : -1).ToArray();
                parseTemp1 = double.TryParse(textBoxes [0].Text.Substring(1, textBoxes [0].Text.Length - 1), out valuesInBoxes [0]);
                parseTemp2 = double.TryParse(textBoxes [1].Text, out valuesInBoxes [1]);


                if (parseTemp1 && parseTemp2)
                {
                    var result = HydrogencarbonateCorrection.CalculateHydrocard(valuesInBoxes [0], valuesInBoxes [1]);
                    double hydroCard = result.Item1;
                    double volume1 = result.Item2;
                    double volume2 = result.Item3;

                    Application.Current.MainWindow.Resources ["HydroCarb"] = hydroCard;
                    Application.Current.MainWindow.Resources ["volume_4"] = volume1;
                    Application.Current.MainWindow.Resources ["volume_8_4"] = volume2;
                }

            }

            catch (ArgumentOutOfRangeException ex)
            {
                Debug.Write(ex.Message);
            }

        }
        private void BE_TextChange(object sender, TextChangedEventArgs e)
        {
            var currentBox = (WatermarkTextBox) sender;

            if ((currentBox.Text.Length == 1) && (currentBox.Text [0] != '-'))
            {
                currentBox.Text = "-" + currentBox.Text;
                e.Handled = true;
            }

            CalculateThings();

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var textbox in textBoxes)
                textbox.Text = "";
        }

        private void BodyMass_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateThings();
        }

    }
}
