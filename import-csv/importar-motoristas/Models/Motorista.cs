using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace import_csv.Models
{
    public class Motorista
    {
        public Motorista() => Id = Guid.NewGuid();

        [Key, Column(Order = 1)]
        public Guid Id { get; set; }
        public string Nome { get; set; } 
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPFCNPJ { get; set; }
        public string CEP { get; set; }

    }
}