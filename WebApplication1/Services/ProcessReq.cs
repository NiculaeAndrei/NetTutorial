using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Services
{


    public class ProcessReq : IProcessReq
    {
        private readonly CifContext _context;


        public ProcessReq(CifContext context)
        {
            _context = context;
        }

        public async Task<Cif> Process(Cif cif)
        {
            if (CifValidator.Validate(cif.Name) == true)
            {


                cif.IsCached = false;
                cif.DataApel = DateTime.Now.ToString("yyyy-MM-dd");
                cif.IsValid = true;
                string _name = cif.Name;

                var query = await _context.Cifs.Where(s => s.DataApel == cif.DataApel && s.Name == cif.Name).FirstOrDefaultAsync();
                if (query == null)
                {


                    if (cif.Name.StartsWith("RO"))
                    { _name = cif.Name.Substring(2); }
                    if (!CheckAnaf())
                    {
                        return null;
                    }

                    string rez = AnafReq.SendReq(_name);


                    var t = new JObject();
                    t = JObject.Parse(rez);


                    if (!t.SelectToken("$.cod").ToString().Equals("200"))
                    { return null; }

                    cif.Denumire = t.SelectToken("found[0].denumire").ToString();
                    cif.Adresa = t.SelectToken("found[0].adresa").ToString();
                    cif.PlatitorTVA = bool.Parse(t.SelectToken("found[0].scpTVA").ToString());
                    cif.StatusTVAIncasare = bool.Parse(t.SelectToken("found[0].statusTvaIncasare").ToString());
                    cif.StatusActiv = bool.Parse(t.SelectToken("found[0].statusInactivi").ToString());
                    cif.IsCached = true;
                    _context.Cifs.Add(cif);
                    await _context.SaveChangesAsync();
                    cif.IsCached = false;
                    return cif;
                }

                return query;

            }


            return null;

        }





        public async Task<ActionResult<Cif>> GetCif(long id)
        {

            var cif = await _context.Cifs.FindAsync(id);



            return cif;

        }



        public bool CheckDatabase()
        {
            try
            {
                _context.Database.CanConnect();
            }

            catch (SqlException)
            {
                return false;
            }

            return true;
        }

        public bool CheckAnaf()
        {
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                string result = client.DownloadString("https://webservicesp.anaf.ro/PlatitorTvaRest/api/v4/");

            }
            catch (System.Net.WebException)
            {

                return false;

            }
            return true;

        }

    }



    public interface IProcessReq

    {
        Task<Cif> Process(Cif cif);
        Task<ActionResult<Cif>> GetCif(long id);
        bool CheckDatabase();
        bool CheckAnaf();

    }
}
