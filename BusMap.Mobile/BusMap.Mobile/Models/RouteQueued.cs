using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace BusMap.Mobile.Models
{
    public class RouteQueued : BindableBase
    {
        private int _positiveVotes;
        private int _negativeVotes;

        public int Id { get; set; }
        public string Name { get; set; }
        public string DayOfTheWeek { get; set; }
        public CarrierQueued CarrierQueued { get; set; }
        public int CarrierQueuedId { get; set; }
        public List<BusStopQueued> BusStopsQueued { get; set; }

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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var stop in BusStopsQueued)
            {
                stringBuilder.Append(string.IsNullOrEmpty(stop.Label)
                    ? $"{stop.Address}\n"
                    : $"{stop.Address}, {stop.Label}\n");
            }

            return stringBuilder.ToString();
        }

    }
}
