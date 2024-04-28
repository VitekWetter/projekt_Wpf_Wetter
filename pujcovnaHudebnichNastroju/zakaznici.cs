using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pujcovnaHudebnichNastroju
{
    class zakaznici
    {
        public zakaznici(int id, string jmeno, string prijmeni, int telefon) 
        {
            this.id = id;
            this.jmeno = jmeno;
            this.prijmeni = prijmeni;
            this.telefon = telefon;
        }

        public int id { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public int telefon { get; set; }
    }
}
