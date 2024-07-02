using System.ComponentModel.DataAnnotations;

namespace Bank.Requests
{
    public class DeactiveUserByIdRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
