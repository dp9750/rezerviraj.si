using System.ComponentModel.DataAnnotations;

namespace rezerviraj.si.Models
{
    public class Miza
    {
        [Display(Name = "ID restavracije")]
        public string RestavracijaID { get;set; }
        [Display(Name = "ID")]
        public int MizaID { get; set; }
        [Display(Name = "Številka mize")]
        public int StMize { get; set; }
        [Display(Name = "Število oseb")]
        public int StOseb { get; set; }
    }
}