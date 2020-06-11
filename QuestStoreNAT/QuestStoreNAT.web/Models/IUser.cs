namespace QuestStoreNAT.web.Models
{
    public interface IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Credentials Credentials { get; set; }
    }
}
