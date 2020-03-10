using System.Text.RegularExpressions;

namespace WebApplication1.Utils
{
    public class CifValidator
    {

        public static bool Validate(string cif)
        {
            if (!Regex.Match(cif, @"^(RO)?([0-9]{2,10}$)").Success)
            {
                return false;
            }

            if (!int.TryParse(cif, out _))
            {
                if (cif.Substring(0, 2).Equals("RO"))
                {
                    cif = cif.Substring(2);
                }
            }

            if (cif.Length > 10 || cif.Length < 2)
            {
                return false;
            }

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
