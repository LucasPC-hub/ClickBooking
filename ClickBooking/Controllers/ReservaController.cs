using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClickBooking.Models;
using System.Threading.Tasks;
using ClickBooking.Data;
using Microsoft.AspNetCore.Authorization;

namespace ClickBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public ReservaController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Reserva reserva)
        {
            var user = await _userManager.FindByIdAsync(reserva.UsuarioId);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return Ok(reserva);
        }          
        
        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}