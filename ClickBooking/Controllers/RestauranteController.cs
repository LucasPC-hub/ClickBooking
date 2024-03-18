using ClickBooking.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClickBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace ClickBooking.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class RestauranteController : Controller
{
    private readonly ApplicationDbContext _context;

    public RestauranteController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Restaurante restaurante)
    {
        _context.Restaurantes.Add(restaurante);
        await _context.SaveChangesAsync();

        return Ok(restaurante);
    }
    [HttpPost("{id}/addReserva")]
    public async Task<IActionResult> AddReserva(int id, [FromBody] Reserva reserva)
    {
        var restaurante = await _context.Restaurantes.FindAsync(id);
        if (restaurante == null)
        {
            return NotFound("Restaurante não encontrado");
        }

        restaurante.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        return Ok(restaurante);
    }
    [HttpGet]
    public async Task<IActionResult> GetRestaurantes()
    {
        var restaurantes = await _context.Restaurantes.ToListAsync();
        return Ok(restaurantes);
    }
}