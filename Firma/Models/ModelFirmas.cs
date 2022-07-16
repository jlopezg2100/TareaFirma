using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace Firma.Models
{
    public class ModelFirmas
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        [MaxLength(70)]
        public string nombre { get; set; }

        [MaxLength(70)]
        public string descripcion { get; set; }

        public byte[] ifirma { get; set; }

    }
}
