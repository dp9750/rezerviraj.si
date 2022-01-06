using System.ComponentModel.DataAnnotations;

namespace rezerviraj.si.Models
{
    public class Gost
    {
        [Display(Name = "ID")]
        public int GostID { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        [Display(Name = "Elektronski naslov")]
        public string Email { get; set; }
        public string Geslo { get; set; }
        [Display(Name = "Telefonska številka")]
        public string TelefonskaSt { get; set; }
    }
}