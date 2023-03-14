using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weapon_SHOP.Models;
using Weapon_SHOP.Data;
using Microsoft.AspNetCore.Authorization;

namespace Weapon_SHOP.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly DataContext _context;

        public WeaponController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Get_Weapon_by_id"), Authorize(Roles = "Ryadovoy, General")]
        public async Task<ActionResult<Weapon>> Get(int id)
        {
            var sub = await _context.Weapons.FindAsync(id);
            if (sub == null)
                return BadRequest("Id not found :(");
            return Ok(sub);
        }

        [HttpGet("Get_Weapons_by_calibre"), Authorize(Roles = "Ryadovoy, General")]
        public async Task<ActionResult<Weapon>> SuperPuperGet(string calibre)
        {
            var sub = from Weapons in _context.Weapons where Weapons.Calibre == calibre select new 
            {
                Weapons.Name 
            };
            return Ok(sub);
        }

        [HttpGet("Get_Weapon_by_Name"), Authorize(Roles = "Ryadovoy, General")]
        public IEnumerable<Object> Get(string name)
        {
            var weap = (from Weapons in _context.Weapons
                      where (Weapons.Name.Contains(name))
                      select new
                      { Weapons.Name }).ToHashSet();
            return weap;
        }

        [HttpGet("Get_all_Weapons_on_Market"), Authorize(Roles = "Ryadovoy, General")]
        public async Task<ActionResult<List<Weapon>>> Get()
        {
            return Ok(await _context.Weapons.ToListAsync());
        }

        [HttpPost("Add_new_Weapon"), Authorize(Roles = "General")]
        public async Task<ActionResult<List<Weapon>>> AddCard(Weapon sub)
        {
            _context.Weapons.Add(sub);
            await _context.SaveChangesAsync();
            return Ok(await _context.Weapons.ToListAsync());
        }

        [HttpDelete("Delete_Weapon_by_id"), Authorize(Roles = "General")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}