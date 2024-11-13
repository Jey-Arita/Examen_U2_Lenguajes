using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartidasContables.DataBase.Entities
{
    [Table("saldos", Schema = "dbo")]
    public class SaldoEntity
    {
        [Column("año")]
        [Required]
        public int Año { get; set; }

        [Column("mes")]
        [Required]
        public int Mes { get; set; }

        [Column("monto_saldo")]
        [Required] // Solo si el monto es obligatorio
        public decimal MontoSaldo { get; set; }

        [Column("id_catalogo_cuenta")]
        [Required]
        public Guid IdCatalogoCuenta { get; set; }

        [ForeignKey(nameof(IdCatalogoCuenta))]
        public CatalogoCuentaEntity CatalogoCuenta { get; set; }
    }
}
