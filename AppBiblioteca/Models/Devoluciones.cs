using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Devoluciones
    {
        [Key]
        public int DevolucionID{ get; set; }

        [Display(Name ="Fecha de Devolución")]
        [DataType(DataType.Date)]
        public DateTime DevolucionFecha{ get; set; }

        [Display(Name ="Socio")]
        public int SocioID{ get; set; }
        public virtual Socios Socios{ get; set; }

        public virtual ICollection<DevolucionesDetalles> DevolucionesDetalles{ get; set; }
    }
}