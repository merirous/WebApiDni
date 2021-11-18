using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDni.DataContext;
using WebApiDni.Models;

namespace WebApiDni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _db;
        public PersonController(PersonContext db)
        {
            _db = db;
        }

    [HttpGet]
    public async Task<ActionResult> GetPeoples()
        {
            var lista = await _db.Person.OrderBy(c => c.Nombre).ToListAsync();
            return Ok(lista);
        }

    [HttpGet("{dni:int}")]
    public async Task<ActionResult> GetPersonas(int dni)
        {
            var obj = await _db.Person.FirstOrDefaultAsync(c => c.Dni == dni);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

    [HttpPost]
    public async Task<ActionResult> PostPersonas(Person person)
        {
            _db.Person.Add(person);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetPersonas", new { dni = person.Dni }, 
                person);
        }
    }
}
