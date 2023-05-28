namespace pracapiapp.Models
{
    public class AdminLogin
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int  AdminId { get; set; }
        public string? AdminName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
