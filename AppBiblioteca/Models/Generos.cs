using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Generos
    {
        [Key]
        public int GeneroID { get; set; }
        
        [Display(Name = "Nombre del genero")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El nombre del genero no puede tener mas de 50 caracteres")]
        public string GeneroNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}