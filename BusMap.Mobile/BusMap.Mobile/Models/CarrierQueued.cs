using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace BusMap.Mobile.Models
{
    public class CarrierQueued : BindableBase
    {
        private int _positiveVotes;
        private int _negativeVotes;
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteQueued> RoutesQueued { get; set; }

        public DateTime CreatedDatetime { get; set; }
        public DateTime? VotingStartedDatetime { get; set; }
        public DateTime? VotingEndedDateTime { get; set; }

        public int PositiveVotes
        {
            get => _positiveVotes;
            set => SetProperty(ref _positiveVotes, value);
        }

        public int NegativeVotes
        {
            get => _negativeVotes;
            set => SetProperty(ref _negativeVotes, value);
        }
    }
}
