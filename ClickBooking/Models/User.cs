using Microsoft.AspNetCore.Identity;

namespace ClickBooking.Models
{
    public class User : IdentityUser
    {
        public int? RestauranteId { get; set; } // Campo para associar o User a um Restaurante
        public Restaurante Restaurante { get; set; } // Propriedade de navegação para o Restaurante
    }
}