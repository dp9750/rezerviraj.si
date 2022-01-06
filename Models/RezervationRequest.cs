namespace rezerviraj.si.Models 
{
    public class RezervationRequest : Rezervacija {
        public string Email { get; set; }
        public string Geslo { get; set; }
        public string RestavracijaID { get; set; }
        public string GostID { get; set; }
    }
}