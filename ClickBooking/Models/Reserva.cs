﻿using System;
using System.Text.Json.Serialization;

namespace ClickBooking.Models
{
public class Reserva
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int NumeroPessoas { get; set; }
    public string UsuarioId { get; set; }
}
}