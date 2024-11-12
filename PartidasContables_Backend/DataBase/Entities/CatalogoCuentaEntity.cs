using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    public class CatalogoCuentaEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public string TipoCuenta { get; set; } // Ej: "Activo", "Pasivo"

        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoInicial { get; set; }

        public Guid IdCuentaPadre { get; set; }

        [ForeignKey(nameof(IdCuentaPadre))]
        public virtual CatalogoCuentaEntity CuentaPadre { get; set; }

        public ICollection<CatalogoCuentaEntity> CuentasHijas { get; set; } = new List<CatalogoCuentaEntity>();

        public bool PermiteMovimiento { get; set; }
    }
}
