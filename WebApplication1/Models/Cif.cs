namespace WebApplication1.Models
{
    public class Cif
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsValid { get; set; }

        public string Denumire { get; set; }

        public string Adresa { get; set; }

        public bool PlatitorTVA { get; set; }

        public bool StatusTVAIncasare { get; set; }

        public bool StatusActiv { get; set; }

        public string DataApel { get; set; }

        public bool IsCached { get; set; }
    }
}
