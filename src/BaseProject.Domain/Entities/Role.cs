namespace BaseProject.Domain.Entities
{
    public class Role
    {
        public byte Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
    }
}