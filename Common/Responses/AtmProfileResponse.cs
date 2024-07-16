using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses
{
    public class AtmProfileResponse
    {
        public int userId {  get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public decimal balance { get; set; }
    }
}
