using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class UpdateCardRequest
    {
        public int CardId { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string EmbossedName { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int CSV {  get; set; }
    }
}
