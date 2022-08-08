using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Editoriales
    {
        [Key]
        public int EditorialID { get; set; }

        [Display(Name = "Nombre de la editorial")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El nombre de la editorial no puede tener mas de 50 caracteres")]
        public string EditorialNombre{ get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}