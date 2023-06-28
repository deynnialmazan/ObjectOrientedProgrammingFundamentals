//****************** LAB 04 - DEYNNI ALMAZAN *********************************/

using ObjectOrientedProgrammingFundamentals_Lab4;

// Create hotel

Hotel hotel = new Hotel("Canada Inn", "Winnipeg, Canada");

// Create rooms & add to hotel list
Room room01 = new Room("A12", 2);
Room room02 = new Room("A15", 4);
Room room03 = new Room("A20", 6);
PremiumRoom room04Premium = new PremiumRoom("P01", 6, "Jacuzzi, Kitchen", 50);


// Create clients & add to hotel list
Client client01 = new Client("Deynni Almazan", 123647910222);
Client client02 = new Client("Karl Miller", 589769988);
Client client03 = new Client("Valerie Jazlyn", 78945888);
VIPClient client04 = new VIPClient("Karl Smith", 1123664558, 55);

// Make a reservation
Reservation reservation01 = new Reservation(DateTime.Now, 2, true, client01, room01);
Reservation reservation02 = new Reservation(DateTime.Now.AddDays(2), 3, true, client02, room03);
Reservation reservation03 = new Reservation(DateTime.Now.AddDays(4), 5, true, client04, room04Premium);


// Print total reservations
hotel.printReservations();

// Print reservations for client
client01.HistoryReservations();
client02.HistoryReservations();
client04.HistoryReservations();

// If a room is ocuppied, show a message:
Reservation reservation0 = new Reservation(DateTime.Now.AddDays(2), 3, true, client03, room03);

// If capacity of room is < occupants, show a message:
Reservation reservation04 = new Reservation(DateTime.Now.AddDays(3), 5, true, client02, room02);