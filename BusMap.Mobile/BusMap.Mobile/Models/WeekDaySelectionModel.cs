using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class WeekDaySelectionModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsChecked { get; set; }

        public WeekDaySelectionModel(DayOfWeek dayOfWeek, bool isChecked = false)
        {
            DayOfWeek = dayOfWeek;
            IsChecked = isChecked;
        }

        public override string ToString()
            => DayOfWeek.ToString();
    }
}
