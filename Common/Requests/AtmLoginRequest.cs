using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class AtmLoginRequest
    {
        public string CardNumber { get; set; } = string.Empty;
        public int CSV { get; set; }
    }
}
