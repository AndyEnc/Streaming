
using System.ComponentModel.DataAnnotations;


namespace Application.ViewModels
{
    public class SaveGenderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El genero es requerido")]
        public string Name { get; set; }
    }
}
