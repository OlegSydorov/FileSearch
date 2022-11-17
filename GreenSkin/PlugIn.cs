using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace GreenSkin
{
    public class PlugIn
    {
        public void ChangeSkin(System.Windows.Window window)
        {
            //for buttons folder and file 
            Style st1 = new Style(typeof(Button));
            Setter setter11 = new Setter(Button.BackgroundProperty, Brushes.White);
            Setter setter12 = new Setter(Button.ForegroundProperty, Brushes.Green);
            st1.Setters.Add(setter11);
            st1.Setters.Add(setter12);

            //for buttons start pause resume and stop
            Style st2 = new Style(typeof(Button));
            Setter setter21 = new Setter(Button.BackgroundProperty, Brushes.Green);
            Setter setter22 = new Setter(Button.ForegroundProperty, Brushes.White);
            st2.Setters.Add(setter21);
            st2.Setters.Add(setter22);

            //for buttons skins and exit
            Style st3 = new Style(typeof(Button));
            Setter setter31 = new Setter(Button.BackgroundProperty, Brushes.LimeGreen);
            Setter setter32 = new Setter(Button.ForegroundProperty, Brushes.Green);
            st3.Setters.Add(setter31);
            st3.Setters.Add(setter32);

            //for textbox
            Style st4 = new Style(typeof(TextBox));
            Setter setter41 = new Setter(TextBox.BackgroundProperty, Brushes.White);
            Setter setter42 = new Setter(TextBox.ForegroundProperty, Brushes.Green);
            st4.Setters.Add(setter41);
            st4.Setters.Add(setter42);

            //for listbox
            Style st5 = new Style(typeof(ListBox));
            Setter setter51 = new Setter(ListBox.BackgroundProperty, Brushes.LimeGreen);
            Setter setter52 = new Setter(ListBox.ForegroundProperty, Brushes.Green);
            st5.Setters.Add(setter51);
            st5.Setters.Add(setter52);

            //for datagrid
            Style st6 = new Style(typeof(DataGrid));
            Setter setter61 = new Setter(DataGrid.BackgroundProperty, Brushes.White);
            Setter setter62 = new Setter(DataGrid.ForegroundProperty, Brushes.DarkGreen);
            st6.Setters.Add(setter61);
            st6.Setters.Add(setter62);

            //for progressbar
            Style st7 = new Style(typeof(ProgressBar));
            Setter setter71 = new Setter(ProgressBar.BackgroundProperty, Brushes.LimeGreen);
            Setter setter72 = new Setter(ProgressBar.ForegroundProperty, Brushes.Green);
            st7.Setters.Add(setter71);
            st7.Setters.Add(setter72);

            //for border
            Style st8 = new Style(typeof(Border));
            Setter setter81 = new Setter(Border.BackgroundProperty, Brushes.LightGreen);
            st8.Setters.Add(setter81);

            // Применить стиль для кнопок главного окна
            if (window.Content is Grid)
            {
                Grid mainContainer = (Grid)window.Content;
                foreach (UIElement currentElement1 in mainContainer.Children)
                {


                    if (currentElement1 is Border)
                    {
                        Border b1 = new Border();
                        b1 = currentElement1 as Border;
                        b1.BorderBrush = Brushes.Green;
                        if (b1.Name == "border1")
                        {
                            Border b2 = new Border();

                            b2 = ((Border)currentElement1).Child as Border;

                            if (b2.Name == "border2")
                            {
                                b2.Background = Brushes.LightGreen;

                                Grid g = new Grid();
                                g = b2.Child as Grid;

                                foreach (UIElement currentElement2 in g.Children)
                                {
                                    if (currentElement2 is Button)
                                    {
                                        Button x = new Button();
                                        x = currentElement2 as Button;

                                        if (x.Name == "folderButton" || x.Name == "fileButton")
                                        {
                                            ((Button)currentElement2).Background = Brushes.White;
                                            ((Button)currentElement2).Foreground = Brushes.Green;
                                        }

                                        else if (x.Name == "startButton" || x.Name == "pauseButton" || x.Name == "resumeButton" || x.Name == "stopButton")
                                        {
                                            ((Button)currentElement2).Background = Brushes.Green;
                                            ((Button)currentElement2).Foreground = Brushes.White;
                                        }

                                        else if (x.Name == "skinButton" || x.Name == "exitButton")
                                        {
                                            ((Button)currentElement2).Background = Brushes.LimeGreen;
                                            ((Button)currentElement2).Foreground = Brushes.Green;
                                        }
                                    }
                                    if (currentElement2 is TextBox)
                                    {
                                        ((TextBox)currentElement2).Background = Brushes.White;
                                        ((TextBox)currentElement2).Foreground = Brushes.Green;
                                    }

                                    if (currentElement2 is ListBox)
                                    {
                                        ((ListBox)currentElement2).Background = Brushes.LimeGreen;
                                        ((ListBox)currentElement2).Foreground = Brushes.Green;
                                    }

                                    if (currentElement2 is DataGrid)
                                    {
                                        ((DataGrid)currentElement2).Background = Brushes.White;
                                        ((DataGrid)currentElement2).Foreground = Brushes.DarkGreen;
                                    }

                                    if (currentElement2 is ProgressBar)
                                    {
                                        ((ProgressBar)currentElement2).Background = Brushes.LimeGreen;
                                        ((ProgressBar)currentElement2).Foreground = Brushes.Green;
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
