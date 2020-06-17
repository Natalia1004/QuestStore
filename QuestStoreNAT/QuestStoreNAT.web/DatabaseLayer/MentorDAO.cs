using System;
using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class MentorDAO : DBAbstractRecord<Mentor>
    {
        public override string DBTableName { get; set; } = "Mentors";

        private int CredentialID {get; set;}

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

        public override string ProvideQueryStringToAdd(Mentor mentorToAdd)
        {
            throw new System.NotImplementedException();
        }

        public override string ProvideQueryStringToUpdate(Mentor mentorToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" SET (\"FirstName\" = {mentorToUpdate.FirstName}, " +
                        $"\"Surname\" = {mentorToUpdate.LastName}, \"Bio\" = {mentorToUpdate.Bio}, " +
                       $"WHERE (\"ID\" = {mentorToUpdate.Id}";
            return query;
        }


#region outOfAbstraction
        public string ProvideQueryStringReturningID( Mentor mentorToAdd )
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"FirstName\", \"Surname\", \"Bio\", \"CredentialID\")" +
                       $"VALUES({mentorToAdd.FirstName}, " +
                              $"'{mentorToAdd.LastName}', " +
                              $"'{mentorToAdd.Bio}', " +
                              $"{mentorToAdd.CredentialID}) RETURNING ID ";
            return query;
        }

        public int AddMentorByCredentialsReturningID(int credentialID ) // credentialID from CredentialsDAO.AddRecordReturningID(Credentials newCredential)
        {
            Mentor newMentor = new Mentor
            {
                FirstName = "" ,
                LastName = "" ,
                Bio = "" ,
                CredentialID = credentialID
            };
            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringToAdd(newMentor);
            return ExecuteScalar(connection , query); // MentorID used to instanly update Mentor
        }

        private int ExecuteScalar( NpgsqlConnection connection , string query )
        {
            using var command = new NpgsqlCommand(query , connection);
            command.Prepare();
            return Convert.ToInt32(command.ExecuteScalar());
        }
        #endregion
    }
}
