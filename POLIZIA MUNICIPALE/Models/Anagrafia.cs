namespace POLIZIA_MUNICIPALE.Models
{
    public class Anagrafia
    {
        public int IDAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string Cap { get; set; }
        public string Cod_Fisc { get; set; }
    }
    public class VerbaliTrascritti
    {
        public int IDAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int TotaleVerbaliTrascritti { get; set; }
    }
}

public class VerbaleDettaglio
{
    public int IDAnagrafica { get; set; }
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public decimal Importo { get; set; }
    public DateTime DataViolazione { get; set; }
    public int DecurtamentoPunti { get; set; }
}

