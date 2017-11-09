﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStorePoc.Integrador
{
    [Table("Agencias")]
    public class Agencia
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
    }
}
