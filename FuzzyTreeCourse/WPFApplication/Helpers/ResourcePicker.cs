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
        /// <summary>
        /// Used to refresh all objects in controller containing reference data
        /// and save controller.Data context
        /// </summary>
        /// <param name="recreate"></param>
        /// <returns></returns>
        public static MainController GetMainController(bool recreate = false)
        {
            if (recreate)
            {
                Application.Current.Resources["mainController"] = new MainController(
                    (int)Application.Current.Resources["constNumberOfDecimals"], false);
            }

            return (MainController)Application.Current.Resources["mainController"];
        }
    }
}
