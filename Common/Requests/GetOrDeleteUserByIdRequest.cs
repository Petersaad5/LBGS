using System.ComponentModel.DataAnnotations;

namespace Bank.Requests
{
    public class GetOrDeleteUserByIdRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
