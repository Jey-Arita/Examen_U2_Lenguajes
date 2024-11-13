using PartidasContables.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.CatalogoCuenta
{
    public class CatalogoCuentaDto
    {
        public Guid Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string Descripcion { get; set; }

        public string TipoCuenta { get; set; } // Ej: "Activo", "Pasivo", Capital (Debe/Haber)

        public decimal Saldo { get; set; }

        public Guid? IdCuentaPadre { get; set; } // Nullable para evitar problemas de eliminación en cascada

        public ICollection<CatalogoCuentaEntity> CuentasHijas { get; set; } = new List<CatalogoCuentaEntity>();

        public bool PermiteMovimiento { get; set; }
    }
}
