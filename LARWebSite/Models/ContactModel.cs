using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string NombreContacto { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string EmailContacto { get; set; }

        [Required(ErrorMessage = "El campo comentario es obligatorio")]
        public string ComentarioContacto { get; set; }
    }
}