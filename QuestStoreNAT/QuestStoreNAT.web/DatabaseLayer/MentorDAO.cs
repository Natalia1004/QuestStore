using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class MentorDAO : DBAbstractRecord<Mentor>
    {
        public override string DBTableName { get; set; } = "Mentors";
        private enum MentorsEnum
        {
            Id, FirstName, LastName, Bio
        }

        public override Mentor ProvideOneRecord(NpgsqlDataReader reader)
        {
            var mentor = new Mentor();
            mentor.Id = reader.GetInt32((int)MentorsEnum.Id);
            mentor.FirstName = reader.GetString((int)MentorsEnum.FirstName);
            mentor.LastName = reader.GetString((int)MentorsEnum.LastName);
            mentor.Bio = reader.GetString((int)MentorsEnum.Bio);
            //TODO Credential retrieval ?
            return mentor;
        }
    }
}
