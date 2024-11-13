using PartidasContables.DataBase.Entities;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.CatalogoCuenta
{
    public class CatalogoCuentaCreateDto
    {
        [Required]
        public string NumeroCuenta { get; set; }

        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public string TipoCuenta { get; set; } // Ej: "Activo", "Pasivo", Capital (Debe/Haber)

        [Required]
        public decimal Saldo { get; set; }

        public bool PermiteMovimiento { get; set; }
    }
}
