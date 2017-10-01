using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crud1.Models
{
    public class Evento
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }
        public Categoria Categoria { get; set; }
    }
}