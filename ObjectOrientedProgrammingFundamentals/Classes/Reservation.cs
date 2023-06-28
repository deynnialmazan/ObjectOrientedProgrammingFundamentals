using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedProgrammingFundamentals_Lab4
{
    public class Reservation
    {
        // FIELDS
        private DateTime _date;
        private int _occupants;
        private bool _isCurrent;
        public Client Client;
        public Room Room;

        // PROPERTIES
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int Occupants
        {
            get { return _occupants; }
            set { _occupants = value; }
        }

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set { _isCurrent = value; }
        }

        // CONSTRUCTOR
        public Reservation(DateTime date, int occupants, bool isCurrent, Client client, Room room)
        {

            if (!room.Ocuppied)
            {
                if (occupants > room.Capacity)
                {
                    Console.WriteLine($"\nReservation for {client.Name} on {date.ToShortDateString()} " +
                    $"cannot be booked.The capacity of the room is {room.Capacity}.");
                }
                else
                {
                    Date = date;
                    Occupants = occupants;
                    IsCurrent = isCurrent;
                    Client = client;
                    Room = room;
                    room.Ocuppied = true;

                    // Add reservation to the client
                    client.Reservations.Add(this);

                    // Add reservation to the list of reservations in Hotel
                    Hotel.Reservations.Add(this);
                }

            }
            else
            {
                // If room is ocuppied, do not do the reservation and show a message
                Console.WriteLine($"\nReservation for {client.Name} on {date.ToShortDateString()} " +
                    $"cannot be booked. Room {room.Number} is already occupied.");
            }
        }
    }
}
