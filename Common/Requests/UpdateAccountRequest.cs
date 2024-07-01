using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class UpdateAccountRequest
    {
        public int Id { get; set; }
        public int UsertId { get; set; }
        public int AccountNumber {  get; set; } 
        public bool IsActive { get; set; }  
        public decimal Balance { get; set; }
    }
}
