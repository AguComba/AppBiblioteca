using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class PrestamosDetalle
    {
        [Key]
        public int PrestamoDetalleID { get; set; }

        public int PrestamosID { get; set; }
        public virtual Prestamos Prestamos { get; set; }

        public int LibroID { get; set; }
        public virtual Libros Libros { get; set; }
    }
}