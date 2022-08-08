using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoID { get; set; }
        
        [Display(Name = "Fecha del prestamo")]
        [DataType(DataType.Date)]
        public DateTime PrestamoFecha { get; set; }

        [Display(Name ="Fecha de devolución")]
        [DataType(DataType.Date)]
        public DateTime PrestamoFechaDevolucion { get; set; }

        public int SocioID { get; set; }
        public virtual Socios Socios { get; set; }

        public virtual ICollection<PrestamosDetalle> PrestamosDetalles { get; set; }
    }
}