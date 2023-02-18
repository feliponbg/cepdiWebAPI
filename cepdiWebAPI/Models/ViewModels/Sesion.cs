using System.ComponentModel.DataAnnotations;

namespace cepdiWebAPI.Models.ViewModels
{
    public class Sesion
    {
        [StringLength(25)]
        public string Usuario { get; set; }

        //[Required, StringLength(25)]
        //No se utiliza
        [StringLength(25)]
        public string Contraseña { get; set; }

        [Required]
        public bool MantenerSesionIniciada { get; set; }

    }
}
