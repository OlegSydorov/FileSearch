using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BlueSkin
{
    public class PlugIn
    {
        public void ChangeSkin(System.Windows.Window window)
        {

            if (window.Content is Grid)
            {
                Grid mainContainer = (Grid)window.Content;
                foreach (UIElement currentElement1 in mainContainer.Children)
                {


                    if (currentElement1 is Border)
                    {
                        Border b1 = new Border();
                        b1 = currentElement1 as Border;
                        b1.BorderBrush = Brushes.Blue;
                        if (b1.Name == "border1")
                        {
                            Border b2 = new Border();

                            b2 = ((Border)currentElement1).Child as Border;

                            if (b2.Name == "border2")
                            {
                                b2.Background = Brushes.Cyan;

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
                                            ((Button)currentElement2).Foreground = Brushes.Blue;
                                        }

                                        else if (x.Name == "startButton" || x.Name == "pauseButton" || x.Name == "resumeButton" || x.Name == "stopButton")
                                        {
                                            ((Button)currentElement2).Background = Brushes.Blue;
                                            ((Button)currentElement2).Foreground = Brushes.White;
                                        }

                                        else if (x.Name == "skinButton" || x.Name == "exitButton")
                                        {
                                            ((Button)currentElement2).Background = Brushes.CornflowerBlue;
                                            ((Button)currentElement2).Foreground = Brushes.Blue;
                                        }
                                    }
                                    if (currentElement2 is TextBox)
                                    {
                                        ((TextBox)currentElement2).Background = Brushes.White;
                                        ((TextBox)currentElement2).Foreground = Brushes.Blue;
                                    }

                                    if (currentElement2 is ListBox)
                                    {
                                        ((ListBox)currentElement2).Background = Brushes.CornflowerBlue;
                                        ((ListBox)currentElement2).Foreground = Brushes.Blue;
                                    }

                                    if (currentElement2 is DataGrid)
                                    {
                                        ((DataGrid)currentElement2).Background = Brushes.White;
                                        ((DataGrid)currentElement2).Foreground = Brushes.Navy;
                                    }

                                    if (currentElement2 is ProgressBar)
                                    {
                                        ((ProgressBar)currentElement2).Background = Brushes.CornflowerBlue;
                                        ((ProgressBar)currentElement2).Foreground = Brushes.Blue;
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