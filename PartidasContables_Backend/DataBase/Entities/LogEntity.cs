﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    [Table("logs", Schema = "dbo")]
    public class LogEntity : BaseEntity
    {
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("id_usuario")]
        [Required]
        [MaxLength(50)] // Ajusta el tamaño según la longitud esperada para IdUsuario
        public string IdUsuario { get; set; }

        [Column("accion")]
        [Required]
        [MaxLength(100)]
        public string Accion { get; set; } // Ej: "Crear Partida" o "Eliminar Partida"

        [Column("id_partida")]
        public string IdPartida { get; set; } // Permite nulos si es opcional
        [Column("id_cuenta")]
        public string IdCuenta { get; set; } // Permite nulos si es opcional
        [Column("email")]
        public string Email { get; set; }

    }
}
