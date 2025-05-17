namespace API.Entities
{
    public class DatingAppUser
    {
        public int MyProperty { get; set; }
        public required string UserName { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
            
    }
}
