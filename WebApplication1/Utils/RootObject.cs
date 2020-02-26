using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class RootObject
    {
        public int cod { get; set; }
        public string message { get; set; }
        public List<Found> found { get; set; }
        public List<object> notfound { get; set; }
    }
}
