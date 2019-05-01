using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FuzzyTreeLib.Models.Counters;

namespace WPFApplication.Helpers
{
    public static class ResourcePicker
    {
        public static MainController GetMainController(bool recreate = false)
        {
            if (recreate)
            {
                Application.Current.Resources["mainController"] = new MainController(
                    (int)Application.Current.Resources["constNumberOfDecimals"]);
            }

            return (MainController)Application.Current.Resources["mainController"];
        }
    }
}
