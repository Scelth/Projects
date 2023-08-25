using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WhiteBoard.Model;

namespace WhiteBoard.Library
{
    public class ColorList
    {
        public static ObservableCollection<ColorModel> AvailableColors { get; set; } = new()
        {
            new()
            {
                ColorName = "Orange",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Orange")),
            },
            new()
            {
                ColorName = "Red",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red")),
            },
            new()
            {
                ColorName = "Blue",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("LightBlue")),
            },
            new()
            {
                ColorName = "Pink",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Pink")),
            },
            new()
            {
                ColorName = "White",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White")),
            },
            new()
            {
                ColorName = "Black",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black")),
            },
            new()
            {
                ColorName = "Green",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Green")),
            },
            new()
            {
                ColorName = "Purple",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Purple")),
            },
            new()
            {
                ColorName = "Yellow",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Yellow")),
            },
            new()
            {
                ColorName = "Brown",
                ColorValue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Brown")),
            }
        };
    }
}