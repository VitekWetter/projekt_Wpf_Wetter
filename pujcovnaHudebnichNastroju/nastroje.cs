using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pujcovnaHudebnichNastroju
{
    class nastroje
    {
        public nastroje(int id, string nazev, string typ)
        {
            this.id = id;
            this.nazev = nazev;
            this.typ = typ;
        }

        public int id { get; set; }

        public string nazev { get; set; }

        public string typ { get; set; }
    }   
}
