using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SaveSeriesViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La portada es requerida")]
        public string ImageP { get; set; } = null!;

        [Required(ErrorMessage = "El enlace del video es requerido")]
        public string URL { get; set; } = null!;

        [Required(ErrorMessage = "La productora es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La productora es requerida")]
        public int ProducerId { get; set; }

        [MinLength(1, ErrorMessage = "El genero primario es requerido")]
        public List<int> Genders { get; set; } = new List<int>();

        public List<int> GendersSecondary { get; set; } = new List<int>();
    }
}
