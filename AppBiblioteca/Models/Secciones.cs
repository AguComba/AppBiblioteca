using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Secciones
    {
        [Key]
        public int SeccionesID { get; set; }

        [Display(Name = "Nombre de la Seccion")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El nombre de la seccion no puede tener mas de 50 caracteres")]
        public string SeccionesNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }

    }
}