using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedProgrammingFundamentals_Lab4
{
    public class Hotel
    {
        // FIELDS
        public static List<Client> Clients = new List<Client>();
        public static List<Room> Rooms = new List<Room>();
        public static List<Reservation> Reservations = new List<Reservation>();
        private static string _name;
        private static string _address;

        // PROPERTIES
        public static string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty. Please " +
                        "enter a name. ");
                _name = value;
            }
        }

        public static string Address
        {
            get { return _address; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Address cannot be empty. Please" +
                        "enter an address. ");
                _address = value;
            }
        }


        // CONSTRUCTOR
        public Hotel(string name, string address)
        {
            Name = name;
            Address = address;

        }

        // METHODS
        public void printReservations()
        {
            try
            {
                Console.WriteLine($"--- List of Reservations [{Hotel.Name}] --- ");
                foreach (Reservation reservation in Reservations)
                {
                    Console.WriteLine($"Client: {reservation.Client.Name} - Room: {reservation.Room.Number} - Date: {reservation.Date.ToShortDateString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in printing reservations. ", ex);
            }



        }

    }
}
