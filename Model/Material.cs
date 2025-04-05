namespace Model
{
    public abstract class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public string? CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
