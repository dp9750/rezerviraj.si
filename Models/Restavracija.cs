using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace rezerviraj.si.Models
{
    public class Restavracija : IdentityUser
    {
        [Display(Name = "Elektronski naslov")]
        public override string Email { get; set; }
        public string Geslo { get; set; }
        public string Naziv { get; set; }
        [Display(Name = "Datum registracije")]
        public DateTime DatumRegistracije { get; set; }
        [Display(Name = "Telefonska številka")]
        public string TelefonskaSt { get; set; }
        public Lokacija Lokacija { get; set; }
        public ICollection<Miza> Mize { get; set; }
    }
}