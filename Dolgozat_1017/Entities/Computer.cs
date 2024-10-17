namespace Dolgozat_1017.Entities
{
    public class Computer
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturedDate { get; set; }

        // Idegen Kulcs a Room-hoz.
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
