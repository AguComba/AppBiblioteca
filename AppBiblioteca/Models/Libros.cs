using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Libros
    {
        [Key]
        public int LibroID { get; set; }

        [Display(Name = "ISBN del libro")]
        public string LibroISBN { get; set; }

        [Display(Name = "Nombre del libro")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(150, ErrorMessage = "El nombre del libro no puede tener mas de 150 caracteres")]
        public string LibroTitulo { get; set; }

        [Display(Name = "Reseña del libro")]
        public string LibroResenia { get; set; }

        [Display(Name = "Fecha de publicación")]
        [DataType(DataType.Date)]
        public DateTime LibroFechaPublicacion { get; set; }

        [Display(Name = "Estado del libro")]
        public EstadoLibro EstadoLibro { get; set; }

        [Display(Name = "Autor del libro")]
        public int AutoresID { get; set; }
        public virtual Autores Autores { get; set; }
        
        [Display(Name = "Editorial del libro")]
        public int EditorialID { get; set; }
        public virtual Editoriales Editoriales { get; set; }

        [Display(Name = "Genero del libro")]
        public int GeneroID { get; set; }
        public virtual Generos Generos { get; set; }

        [Display(Name = "Seccion del libro")]
        public int SeccionesID { get; set; }
        public virtual Secciones Secciones { get; set; }

        public virtual ICollection<PrestamosDetalle> PrestamosDetalle { get; set; }

        public virtual ICollection<DevolucionesDetalles> DevolucionesDetalles { get; set; }
    }

    public enum EstadoLibro
    {
        Disponible,
        Prestado
    }
}