using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses
{
    internal class AddUserResponse
    {
        [Required]
        public int Id { get; set; }
    }
}
