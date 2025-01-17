﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EducationalPracticeSessionOne
{
    /// <summary>
    /// Логика взаимодействия для CaptchaFieldWindow.xaml
    /// </summary>
    public partial class CaptchaFieldWindow : Window
    {

        private static string _captchaWord = "";

        public CaptchaFieldWindow()
        {
            InitializeComponent();

            const string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghigklmnopqrstuvwxyz!@#$%^&*()";

            Random random = new Random();
            int countSymbols = random.Next(8,9);

            for (int i = 0; i < countSymbols; i++)
            {
                Line line = new Line()
                {
                    X1 = random.Next(Convert.ToInt32(can.Width)),
                    Y1 = random.Next(Convert.ToInt32(can.Height)),
                    X2 = random.Next(Convert.ToInt32(can.Width)),
                    Y2 = random.Next(Convert.ToInt32(can.Height)),
                    Stroke = new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255)))
                };
                can.Children.Add(line);
            }

            int x1 = 0, x2 = Convert.ToInt32(can.Width / countSymbols) - 20;

            int textStyle = 0;

            for (int i = 0; i < countSymbols; i++)
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = Convert.ToString(symbols[random.Next(symbols.Length)]),
                    FontSize = 40,
                    Padding = new Thickness(random.Next(x1, x2), random.Next(Convert.ToInt32(can.Height)) / 2, 0, 0),
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 255)))
                };

                textStyle = random.Next(4);
                if (textStyle == 1)
                {
                    textBlock.FontWeight = FontWeights.Bold;
                }
                if (textStyle == 2)
                {
                    textBlock.FontStyle = FontStyles.Italic;
                }
                if (textStyle == 3)
                {
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.FontStyle = FontStyles.Italic;
                }
                _captchaWord += textBlock.Text;
                x1 = x2 + 5;
                x2 += Convert.ToInt32(can.Width / countSymbols);
                can.Children.Add(textBlock);
            }
        }
        public static string getCode()
        {
            return _captchaWord;
        }
    }
}
