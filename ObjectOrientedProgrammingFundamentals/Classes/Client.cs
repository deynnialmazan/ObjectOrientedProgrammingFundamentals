using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOrientedProgrammingFundamentals_Lab4
{
    public class Client
    {
        // FIELDS
        private string _name;
        private long _creditCard;
        private List<Reservation> _reservations;

        // PROPERTIES
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name cannot be empty. Please," +
                        "enter a name.");
                }
                _name = value;
            }
        }

        public long CreditCard
        {
            get { return _creditCard; }
            set
            {
                _creditCard = value;
            }
        }

        public List<Reservation> Reservations
        {
            get { return _reservations; }
            set { _reservations = value; }
        }

        public Client(string name, long creditCard)
        {
            Name = name;
            CreditCard = creditCard;
            Reservations = new List<Reservation>();

            // Add client to list or clients in Hotel
            Hotel.Clients.Add(this);
        }

        public void HistoryReservations()
        {
            Console.WriteLine($"\n--- Reservations of {this.Name} --- ");
            foreach (Reservation reservation in Reservations)
            {
                Console.WriteLine($"Room: {reservation.Room.Number} - Date: {reservation.Date.ToShortDateString()}");
            }
        }
    }

    public class VIPClient : Client
    {
        private int _vipNumber;
        private int _vipPoints;

        public int VIPNumber
        {
            get { return _vipNumber; }
            set { _vipNumber = value; }
        }

        public int VIPPoints
        {
            get { return _vipPoints; }
            set { _vipPoints = value; }
        }

        public VIPClient(string name, int creditCard, int vipNumber) : base(name, creditCard)
        {
            VIPNumber = vipNumber;
            VIPPoints = 0;
        }
    }
}
