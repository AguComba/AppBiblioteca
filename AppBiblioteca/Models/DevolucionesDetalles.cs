using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class DevolucionesDetalles
    {
        [Key]
        public int DevolucionesDetallesID { get; set; }

        public int DevolucionID { get; set; }
        public virtual Devoluciones Devoluciones { get; set; }
        
        [Display(Name = "Libro")]
        public int LibroID{ get; set; }
        public virtual Libros Libros{ get; set; }
        
    }
}