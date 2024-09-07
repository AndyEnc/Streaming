using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageP {  get; set; }= null!;
        public string URL { get; set; } = null!;
        public int ProducerId { get; set; }
        public Producer Producer {  get; set; }=null!;

        

        public ICollection<SeriesGender> SeriesGenderList { get; set; } = null!;



    }
}
