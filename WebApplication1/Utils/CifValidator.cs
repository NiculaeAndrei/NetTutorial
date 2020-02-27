using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class CifValidator
    {

        public static bool Validate(string cif) 
        {
            if (!int.TryParse(cif, out _))
            {
                if (cif.Substring(0, 2).Equals("RO"))
                { cif = cif.Substring(2); }

                else return false;

            }

            if (cif.Length > 10 || cif.Length<2) return false;

            int _cif;
            int.TryParse(cif, out _cif);

            int cifcnt = _cif % 10;
            int cntnr = 753217532;
            _cif = _cif / 10;
            int sum = 0;
            while (_cif != 0)
            {
                sum += (_cif % 10) * (cntnr % 10);
                _cif = _cif / 10;
                cntnr = cntnr / 10;
            }
            int rez = (sum * 10) % 11;
            if (rez == 10)
            { rez = 0; }

            return rez == cifcnt;



        }

    }
}
