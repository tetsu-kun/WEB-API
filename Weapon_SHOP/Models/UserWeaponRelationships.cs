namespace Weapon_SHOP.Models
{
    public class UserWeaponRelationships
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int WeaponID { get; set; }
        public DateTime Date { get; set; }

        public string Mood { get; set; }
        
        public User Users { get; set; }
        public Weapon Weapons { get; set; }
    }
}
