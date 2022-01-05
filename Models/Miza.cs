namespace rezerviraj.si.Models
{
    public class Miza
    {
        public string RestavracijaID { get;set; }
        public int MizaID { get; set; }
        public int StMize { get; set; }
        public int StOseb { get; set; }

        public override string ToString()
        {
            return $"RestavracijaID: {RestavracijaID}, MizaID: {MizaID}, StMize: {StMize}, StOseb: {StOseb}";
        }
    }
}