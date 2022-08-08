using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class PrestamosDetallesT
    {
        [Key]
        public int PrestamoDetalleTID { get; set; }

        public int LibroID { get; set; }

        public string LibroTitulo { get; set; }
    }
}