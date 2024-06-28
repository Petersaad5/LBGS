namespace Bank.Models
{
    public class Dependant
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RelationshipCode { get; set; } = string.Empty;
        public DateTime DateOfBirth {  get; set; } 
        

    }
}
