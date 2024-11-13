using PartidasContables.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.Saldo
{
    public class SaldoDto
    {
        public int Año { get; set; }
        public int Mes { get; set; }
        public decimal MontoSaldo { get; set; }
        public Guid IdCatalogoCuenta { get; set; }
    }
}
