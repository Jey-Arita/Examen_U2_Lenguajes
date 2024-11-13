using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    [Table("detalle_partidas", Schema = "dbo")]
    public class DetallePartidaEntity : BaseEntity
    {
        [Required]
        public Guid IdPartida { get; set; }

        [ForeignKey(nameof(IdPartida))]
        public virtual PartidaEntity Partida { get; set; }

        [Required]
        public Guid IdCatalogoCuenta { get; set; }

        [ForeignKey(nameof(IdCatalogoCuenta))]
        public CatalogoCuentaEntity CatalogoCuenta { get; set; }

        [Required]
        public string TipoOperacion { get; set; } // "Débito" o "Crédito"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
    }
}
