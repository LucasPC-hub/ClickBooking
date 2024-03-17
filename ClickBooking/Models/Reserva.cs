using System;
using System.Text.Json.Serialization;

namespace ClickBooking.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int NumeroPessoas { get; set; }

        [JsonIgnore]
        public User Usuario { get; set; }
    }
}