namespace MinimalWebhook.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }       
        public string? Address { get; set; }
        public string? City { get; set; }
        public bool videoCallScheduled { get; set; }

    }
}
