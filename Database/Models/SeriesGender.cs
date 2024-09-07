namespace Database.Models
{
    public class SeriesGender
    {
        public int SerieId { get; set; }
        public int GenderId {  get; set; }
        public Series Serie { get; set; } = null!;
        public Gender Gender { get; set; } = null!;
        public bool IsPrimary {  get; set; }


    }
}