using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SeriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }= null!;
        public string ImageP { get; set; }= null!;
        public string URL { get; set; }= null!;
        public string ProducerName { get; set; } = null!;
        public List<string> Genders { get; set; } = null!;
        public List<string> GendersSecondary {  get; set; } = null!;

    }
}
