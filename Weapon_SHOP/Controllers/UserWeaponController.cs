using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weapon_SHOP.Data;
using Weapon_SHOP.Models;

namespace Weapon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWeaponController : ControllerBase
    {
        private readonly DataContext _context;
        public UserWeaponController(DataContext context)
        {
            _context = context;

        }


        [HttpPut("Buy_Weapon_by_id"), Authorize(Roles = "Ryadovoy")]
        public async Task<ActionResult<string>> BuyPlayer(int _WeaponID, string mood)
        {
            UserWeaponRelationships Relationship = new UserWeaponRelationships();
            int UserBalance = GetUserBalance(User.Identity.Name);
            int WeaponPrice = GetWeaponPrice(_WeaponID);

            int _UserID = GetUserID(User.Identity.Name);
            if (UserBalance >= WeaponPrice)
            {
                Relationship.UserID = _UserID;
                Relationship.WeaponID = _WeaponID;
                Relationship.Date = DateTime.Now;
                Relationship.Mood = mood;
                int NewBalance = UserBalance - WeaponPrice;
                SetNewUserBalance(_UserID, NewBalance);
                _context.UserWeaponRelationships.Add(Relationship);
                await _context.SaveChangesAsync();
                return Ok(Relationship);
            }
            else
                return BadRequest("You haven't enough money to buy this player");
        }

        [HttpDelete("{id}"), Authorize(Roles = "General")]
        public async Task<IActionResult> DeleteUserCardRelationship(int id)
        {
            var userCardRelationship = await _context.UserWeaponRelationships.FindAsync(id);
            if (userCardRelationship == null)
            {
                return NotFound();
            }

            _context.UserWeaponRelationships.Remove(userCardRelationship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Show_My_Weapons"), Authorize(Roles = "Ryadovoy")]
        public async Task<ActionResult<string>> GetMyPlayers()
        {
            UserWeaponRelationships Relationship = new UserWeaponRelationships();
            int _UserID = GetUserID(User.Identity.Name);
            var query = from Weapons in _context.Weapons
                        join UserWeapon in _context.UserWeaponRelationships
                        on Weapons.Id equals UserWeapon.WeaponID
                        where UserWeapon.UserID == _UserID
                        select new
                        {
                            Weapons.Name,
                            Weapons.Calibre,
                            Weapons.Price
                        };
            return Ok(query);
        }

        private void SetNewUserBalance(int _UserID, int _NewBalance)
        {
            var query = from Users in _context.Users where Users.Id == _UserID select Users;
            foreach (User user in query)
            {
                user.Balance = _NewBalance;
            }
        }
        private int GetUserBalance(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Balance;
            int _Balance = query.FirstOrDefault();
            return _Balance;
        }

        private int GetUserID(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Id;
            int id = query.FirstOrDefault();
            return id;
        }
        private int GetWeaponPrice(int CardID)
        {
            IQueryable<int> query = from Weapon in _context.Weapons where Weapon.Id == CardID select Weapon.Price;
            int _Price = query.FirstOrDefault();
            return _Price;
        }
    }
}