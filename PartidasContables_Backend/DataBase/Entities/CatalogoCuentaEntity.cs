using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PartidasContables.DataBase.Entities
{
    [Table("catalogo_cuentas", Schema = "dbo")]
    public class CatalogoCuentaEntity : BaseEntity
    {
        [Required]
        [Column("numero_cuenta")]
        public string NumeroCuenta { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required]
        [Column("tipo_cuenta")]
        public string TipoCuenta { get; set; } // Ej: "Activo", "Pasivo", Capital (Debe/Haber)

        [Column("saldo", TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }

        [Column("id_cuenta_padre")] // Corregido
        public Guid? IdCuentaPadre { get; set; } // Nullable para evitar problemas de eliminación en cascada

        [ForeignKey(nameof(IdCuentaPadre))]
        public virtual CatalogoCuentaEntity CuentaPadre { get; set; }
        [JsonIgnore]
        public ICollection<CatalogoCuentaEntity> CuentasHijas { get; set; } = new List<CatalogoCuentaEntity>();

        [Column("permite_movimiento")]
        public bool PermiteMovimiento { get; set; }
    }

}
