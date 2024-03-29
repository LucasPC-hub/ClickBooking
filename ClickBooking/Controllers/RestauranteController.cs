﻿using ClickBooking.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClickBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;


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
public async Task<IActionResult> Create(IFormFile file, [FromForm] Restaurante restaurante)
{
    using (var memoryStream = new MemoryStream())
    {
        await file.CopyToAsync(memoryStream);
        restaurante.Imagem = memoryStream.ToArray();
    }

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

        // Add the reserva to the restaurant
        restaurante.Reservas.Add(reserva);

        // Also add the reserva to the Reservas table
        _context.Reservas.Add(reserva);

        await _context.SaveChangesAsync();

        return Ok(restaurante);
    }
    [HttpGet]
    public async Task<IActionResult> GetRestaurantes()
    {
        var restaurantes = await _context.Restaurantes
            .Include(r => r.Reservas) // Include the Reservas when fetching the Restaurantes
            .ToListAsync();
        return Ok(restaurantes);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurante(int id)
    { 
        var restaurante = await _context.Restaurantes
            .Include(r => r.Reservas) // Include the Reservas when fetching a single Restaurante
            .FirstOrDefaultAsync(r => r.Id == id);
        if (restaurante == null)
        {
            return NotFound("Restaurante não encontrado");
        } 
        return Ok(restaurante);
    }
}