using System;
using System.Collections.Generic;

namespace rezerviraj.si.Models
{
    public class Rezervacija
    {
        public int RezervacijaID { get; set; }
        public Restavracija Restavracija { get; set; }
        public Gost Gost { get; set; }
        public DateTime RezerviranoZa { get; set; }
        public DateTime DatumRezervacije { get; set; }
        #nullable enable
        public ICollection<Miza>? Mize { get; set; }
        public int StOseb { get; set; }
    }
}