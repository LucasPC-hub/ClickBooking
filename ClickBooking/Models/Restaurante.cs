using System.Collections.Generic;
using ClickBooking.Models;

public class Restaurante
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public List<Reserva> Reservas { get; set; }

    public Restaurante()
    {
        Reservas = new List<Reserva>();
    }
}