using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class AddCardRequest
    {
        public int AccountId { get; set; }
        public string EmbossedName { get; set; } = string.Empty;

    }
}
