using System;
using System.Collections.Generic;
using System.Text;
using BusMap.Mobile.Annotations;
using Prism.Mvvm;

namespace BusMap.Mobile.Models
{
    public class RouteDelay : BindableBase
    {
        private string _title;
        [CanBeNull] private string _description;
        private DateTime _dateTime;

        public int Id { get; set; }

        public int RouteId { get; set; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        [CanBeNull]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => SetProperty(ref _dateTime, value);
        }
    }
}
