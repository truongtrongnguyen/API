namespace Jwt_Login_API.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 1-1
        public Backpack Backpack { get; set; }

        // 1-N
        public List<Weapon> Weapons { get; set; }

        // N-N -> Trên Databse nó tự động generate ra table CharacterFaction
        public List<Faction> Factions { get; set; }
    }
}
