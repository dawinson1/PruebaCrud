using System.ComponentModel.DataAnnotations;

namespace PruebaCrud.Models.DTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "El Campo {0} Requerido")]
        [StringLength(maximumLength: 18)]
        [Display(Name ="Documento")]
        public string Docu_cli { get; set; }

        [Required(ErrorMessage = "El Campo {0} Requerido")]
        [Display(Name = "Tipo de Identificación")]
        public int Tip_ide { get; set; }

        [Required(ErrorMessage = "El Campo {0} Requerido")]
        [StringLength(maximumLength: 30)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El Campo {0} Requerido")]
        [StringLength(maximumLength: 30)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El Campo {0} Requerido")]
        [Display(Name = "Estado Cliente")]
        public int Est_cli { get; set; }

        public List<Estados> estados_Cli { get; set; }
        public List<TiposIde> tipos_Ide { get; set; }
    }
}
