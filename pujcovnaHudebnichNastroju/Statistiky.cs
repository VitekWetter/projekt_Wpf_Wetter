using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pujcovnaHudebnichNastroju
{
    class Statistiky
    {
        public Statistiky(string typ, int pocet)
        {
            this.typ = typ;
            this.pocet = pocet;
        }

        public string typ { get; set; }
        public int pocet { get; set; }
    }
}
