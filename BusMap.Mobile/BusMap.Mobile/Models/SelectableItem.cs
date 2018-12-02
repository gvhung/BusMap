using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class SelectableItem<T>
    {
        public T TObject { get; set; }
        public bool IsChecked { get; set; }

        public SelectableItem(T tObject, bool isChecked = false)
        {
            TObject = tObject;
            IsChecked = isChecked;
        }

        public override string ToString()
            => TObject.ToString();
    }
}
