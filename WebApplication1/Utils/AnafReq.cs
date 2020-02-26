using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using RestSharp;


namespace WebApplication1.Utils
{
    public class AnafReq
    {

        public static string SendReq(string cif)
        {
            var client = new RestClient("https://webservicesp.anaf.ro");
            var request = new RestRequest("PlatitorTvaRest/api/v4/ws/tva", Method.POST);
            request.RequestFormat = DataFormat.Json;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string req = "";
            req += string.Format(@"{{'cui': {1}, 'data':'{0}'}}", date, cif);
            req = req.Replace("'", "\"");
            req = "[" + req + "]";
            request.AddParameter("application/json", req, ParameterType.RequestBody);
            var data = client.Execute(request);
            return data.Content;



        }


    }
}
