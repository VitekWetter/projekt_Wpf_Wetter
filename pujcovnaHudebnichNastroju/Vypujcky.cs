using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pujcovnaHudebnichNastroju
{
    internal class Vypujcky
    {
        public Vypujcky(int id, string nazev, string typ, string prijmeni, DateTime datumVypujceni, DateTime datumVraceni)
        {
            this.id = id;
            this.nazev = nazev;
            this.typ = typ;
            this.prijmeni = prijmeni;
            this.datumVypujceni = datumVypujceni;
            this.datumVraceni = datumVraceni;
        }

        public int id { get; set; }
        public string nazev { get; set; }
        public string typ { get; set; }
        public string prijmeni { get; set; }
        public DateTime datumVypujceni { get; set; }
        public DateTime datumVraceni { get; set; }
    }
}
