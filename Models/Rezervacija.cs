using System;
using System.ComponentModel.DataAnnotations;

namespace rezerviraj.si.Models
{
    public class Rezervacija
    {
        [Display(Name = "ID")]
        public int RezervacijaID { get; set; }
        public Restavracija Restavracija { get; set; }
        public Gost Gost { get; set; }
        [Display(Name = "Rezerviran datum")]
        public DateTime RezerviranoZa { get; set; }
        [Display(Name = "Datum rezervacije")]
        public DateTime DatumRezervacije { get; set; }
        [Display(Name = "Število oseb")]
        public int StOseb { get; set; }
    }
}