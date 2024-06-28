using System.ComponentModel.DataAnnotations;

namespace Bank.Requests
{
    public class GetUserByIdRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
