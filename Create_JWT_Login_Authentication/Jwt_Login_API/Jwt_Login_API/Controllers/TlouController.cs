using Jwt_Login_API.DTOs;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Login_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TlouController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TlouController(AppDbContext context)
        {
            _context = context;
        }

        // RelationShip 1-1 - Backpack
        [HttpPost("CreataCharacter1")]
        public async Task<ActionResult<List<Character>>> CreataCharacter(CharacterCreateDto request)
        {
            var newCharacter = new Character()
            {
                Name = request.Name,
            };
            var backpack = new Backpack() { Description = request.Backpack.Description, Character = newCharacter };

            newCharacter.Backpack = backpack;

            _context.Characters.Add(newCharacter);
            _context.SaveChanges();

            return Ok(await _context.Characters.Include(x => x.Backpack).ToListAsync());
        }

        // RelationShip 1-N - Weapon
        [HttpPost("CreataCharacter2")]
        public async Task<ActionResult<List<Character>>> CreataCharacter2(CharacterCreateDto request)
        {
            var newCharacter = new Character()
            {
                Name = request.Name,
            };
            var backpack = new Backpack() { Description = request.Backpack.Description, Character = newCharacter };
            var weapons = request.WeaponCreateDtos.Select(x => new Weapon() { Name = x.Name, Character = newCharacter }).ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;

            _context.Characters.Add(newCharacter);
            _context.SaveChanges();

            return Ok(await _context.Characters.Include(x => x.Backpack).ToListAsync());
        }

        // RelationShip N-N - Faction
        [HttpPost("CreataCharacter3")]
        public async Task<ActionResult<List<Character>>> CreataCharacter3(CharacterCreateDto request)
        {
            var newCharacter = new Character()
            {
                Name = request.Name,
            };
            var backpack = new Backpack() { Description = request.Backpack.Description, Character = newCharacter };
            var weapons = request.WeaponCreateDtos.Select(x => new Weapon() { Name = x.Name, Character = newCharacter }).ToList();

            var factions = request.FactionCreateDtos.Select(f => new Faction() { Name = f.Name,
                                                                                Characters = new List<Character>() { newCharacter } 
                                                                               }).ToList();


            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions = factions;

            _context.Characters.Add(newCharacter);
            _context.SaveChanges();

            return Ok(await _context.Characters.Include(x => x.Backpack).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacters(int id)
        {
            var character = await _context.Characters
                            .Include(x => x.Backpack)
                            .Include(x => x.Weapons)
                            .Include(x => x.Factions)
                            .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(character);
        }
    }
}
