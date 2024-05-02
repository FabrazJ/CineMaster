namespace CineMaster.DTO
{
    public class DTOBooking
    {
        public int BookingId { get; set; }
        public int SeatId { get; set; }
        public DateTime Date { get; set; }
    }

    public class SeatDto
    {
        public int SeatId { get; set; }
    }
}
