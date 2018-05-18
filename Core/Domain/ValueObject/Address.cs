using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ValueObject
{
    public class Address
    {
        public Guid CityId { get; private set; } //A reference to a City entity.

        public string Street { get; private set; }

        public int Number { get; private set; }

        public Address(Guid cityId, string street, int number)
        {
            CityId = cityId;
            Street = street;
            Number = number;
        }
    }
}
