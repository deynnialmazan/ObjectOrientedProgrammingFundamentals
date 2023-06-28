using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedProgrammingFundamentals_Lab4
{
    public class Room
    {
        // FIELDS
        private string _number;
        private int _capacity;
        private bool _ocuppied;
        private List<Reservation> _reservations = new List<Reservation>();

        // PROPERTIES
        public string Number
        {
            get { return _number; }
            set
            {

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Number cannot be empty. Please " +
                        "enter a number.");
                }
                _number = value;
            }
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {

                if (!(int.TryParse(value.ToString(), out value)) && !(value > 0))
                {
                    throw new ArgumentException("Invalid capacity. It must be" +
                        "a number greater than 0. ");
                }
                _capacity = value;
            }
        }

        public bool Ocuppied
        {
            get { return _ocuppied; }
            set { _ocuppied = value; }
        }

        public List<Reservation> Reservations
        {
            get { return _reservations; }
            set { _reservations = value; }
        }

        // CONSTRUCTOR
        public Room(string number, int capacity)
        {
            Number = number;
            Capacity = capacity;
            Ocuppied = false;

            // Add room to list or rooms in Hotel
            Hotel.Rooms.Add(this);
        }

    }

    public class PremiumRoom : Room
    {
        private string _additionalAmenities;
        private int _vipValue;

        public string AdditionalAmenities
        {
            get { return _additionalAmenities; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Please enter additional amenities. ");
                }
                _additionalAmenities = value;
            }
        }

        public int VIPValue
        {
            get { return _vipValue; }
            set { _vipValue = value; }
        }

        public PremiumRoom(string number, int capacity, string additionalAmenities,
            int vipValue) : base(number, capacity)
        {
            Number = number;
            Capacity = capacity;
            AdditionalAmenities = additionalAmenities;
            VIPValue = vipValue;
        }
    }

}
