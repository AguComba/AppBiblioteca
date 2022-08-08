using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppBiblioteca.Models
{
    public class Socios
    {
        [Key]
        public int SocioID { get; set; }

        [Display(Name = "Nombre del Socio")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El nombre del Socio no puede tener mas de 50 caracteres")]
        public string SocioNombre { get; set; }

        [Display(Name = "Apellido del Socio")]
        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Apellido del Socio no puede tener mas de 50 caracteres")]
        public string SocioApellido { get; set; }

        [Display(Name ="Domicilio del Socio")]
        public string SocioDireccion { get; set; }

        [Display(Name = "Telefono del Socio")]
        public string SocioTelefono { get; set; }

        [Display(Name = "Fecha de Nacimiento" )]
        [DataType(DataType.Date)]
        public DateTime SocioFechaNacimiento { get; set; }

        [Display(Name ="Nombre del Socio")]
        [NotMapped]
        public string SocioNombreCompleto {
            get
            {
                return string.Format("{0} {1}", SocioApellido, SocioNombre);
            }
        }

        [Display(Name ="Edad del Socio")]
        [NotMapped]
        public int SocioEdad {
            get
            {
                return DateTime.Now.Year - SocioFechaNacimiento.Year;
            }
        }

        public virtual ICollection<Prestamos> Prestamos{ get; set; }

        public virtual ICollection<Devoluciones> Devoluciones { get; set; }
    }
}