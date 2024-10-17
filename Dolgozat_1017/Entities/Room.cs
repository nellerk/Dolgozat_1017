using Microsoft.AspNetCore.Identity;

namespace Dolgozat_1017.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }

        // Idegen Kulcs
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        // Navigációs tulajdonság a kapcsolódó Computers-hez.
        public ICollection<Computer> Computers {  get; set; }
    }
}
