using Microsoft.EntityFrameworkCore;
using Weapon_SHOP.Models;

namespace Weapon_SHOP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<UserWeaponRelationships> UserWeaponRelationships { get; set; }
    }
}
