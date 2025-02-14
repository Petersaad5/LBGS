﻿namespace Bank.Models
{
    public class Card
    {
        public int Id { get; set; }
        public  int AccountId { get; set; }
        public string CardNumber { get; set; } = string.Empty ;
        public string EmbossedName { get; set; } = string.Empty ;
        public string ExpiryDate {  get; set; } = string.Empty ;
        public bool IsActive { get; set; }
        public int CSV { get; set; } 
    }
}
