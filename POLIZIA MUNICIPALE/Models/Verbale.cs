using System.ComponentModel.DataAnnotations.Schema;

namespace POLIZIA_MUNICIPALE.Models
{
    public class Verbale
    {
        public int IDVERBALE { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public int IDAnagrafica { get; set; }
        public int IDViolazione { get; set; }
    }

    public class ViolazioneDettaglio
    {
        public int IDViolazione { get; set; }
        public string Descrizione { get; set; }
    }

}
