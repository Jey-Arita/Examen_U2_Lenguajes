using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    public class SaldoEntity : BaseEntity
    {
        [Required]
        public int Año { get; set; }

        [Required]
        public int Mes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoSaldo { get; set; }

        [Required]
        public Guid IdCatalogoCuenta { get; set; }

        [ForeignKey(nameof(IdCatalogoCuenta))]
        public CatalogoCuentaEntity CatalogoCuenta { get; set; }
    }
}
