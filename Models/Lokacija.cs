using System.ComponentModel.DataAnnotations;

namespace rezerviraj.si.Models
{
    public class Lokacija
    {
        public int LokacijaID { get; set; }

        [Required]
        public string Ulica { get; set; }

        [Required]
        [Display(Name = "Hišna številka")]
        public int HisnaSt { get; set; }

        [Required]
        public string Kraj { get; set; }

        [Display(Name = "Poštna številka")]
        public int PostnaSt { get; set; }

        [Required]
        [Display(Name = "Država")]
        public string Drzava { get; set; }

        public override string ToString()
        {
            return $"{Ulica} {HisnaSt}, {PostnaSt} {Kraj}, {Drzava}";
        }
    }
}