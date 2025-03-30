namespace Model
{
    public abstract class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public string? MatCreatorId { get; set; }
        public User MatCreator { get; set; }

        public abstract void DisplayDetails();
    }
}
