using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;

namespace BusMap.WebApiTests
{
    class PinRepositoryForTests : IPinRepository
    {
        private readonly List<Pin> _pins;

        public PinRepositoryForTests()
        {
            _pins = new List<Pin>()
            {
                new Pin
                {
                    Id = 1,
                    Latitude = 5.0,
                    Longitude = 10.0,
                    Address = "TestAddress1",
                    Label = "TestLabel1"
                },
                new Pin
                {
                    Id = 2,
                    Latitude = 15.0,
                    Longitude = 20.0,
                    Address = "TestAddress2",
                    Label = "TestLabel2"
                },
                new Pin
                {
                    Id = 3,
                    Latitude = 25.987,
                    Longitude = 30.987,
                    Address = "TestAddress3",
                    Label = "TestLabel3"
                }
            };
        }

        public Pin Get(int id)
            => _pins.First(x => x.Id == id);

        public IEnumerable<Pin> GetAll()
            => _pins;

        public void Add(Pin pin)
            => _pins.Add(pin);

        public void AddRange(IEnumerable<Pin> pins)
            => _pins.AddRange(pins);

        public void Remove(Pin pin)
            => _pins.Remove(pin);
    }
}
