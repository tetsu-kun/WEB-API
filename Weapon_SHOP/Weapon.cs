namespace Weapon_SHOP
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Calibre { get; set; } = string.Empty;
        public string Range { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string RateFire { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
