﻿using PartidasContables.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.LogDatabase
{
    public class LogDatabaseDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdUsuario { get; set; }
        public string Accion { get; set; }
        public string IdPartida { get; set; }
        public string IdCatalogo { get; set; }
        public Dictionary<string, object> Detalles { get; set; }
    }

}
