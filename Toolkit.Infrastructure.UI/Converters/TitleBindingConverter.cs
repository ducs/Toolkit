﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Toolkit.Infrastructure.UI.Converters
{
    public class TitleBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] is System.Windows.Controls.Frame frame)
            {
                return frame.Content;
            }
            return values[0];
            //return values[1] == null ? values[0] :
            //    (string)values[0] + " - " + (string)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}