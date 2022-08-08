using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Autores
    {
        [Key]
        public int AutoresID { get; set; }

        [Display(Name = "Nombre del autor")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El nombre del autor no puede tener mas de 50 caracteres")]
        public string AutoresNombre { get; set; }

        [Display(Name = "Apellido del autor")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El apellido del autor no puede tener mas de 50 caracteres")]
        public string AutoresApellido { get; set; }

        [NotMapped]
        public string AutoresNombreCompleto {
            get
            {
                return string.Format("{0} {1}", AutoresApellido, AutoresNombre);
            }
        }

        public virtual ICollection<Libros> Libros { get; set;}

    }
}