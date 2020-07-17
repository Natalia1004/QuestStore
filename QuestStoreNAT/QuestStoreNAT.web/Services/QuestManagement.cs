using System.Collections.Generic;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.DatabaseLayer.ConcreteDAO;

namespace QuestStoreNAT.web.Services
{
    public class QuestManagement
    {
        private OwnedQuestStudentDAO _ownedStudentDAO { get; set; }
        private OwnedQuestGroupDAO _ownedGroupDAO { get; set; }
        private QuestDAO _questDAO { get; set; }

        public QuestManagement()
        {
            _ownedStudentDAO = new OwnedQuestStudentDAO();
            _questDAO = new QuestDAO();
            _ownedGroupDAO = new OwnedQuestGroupDAO();
        }

        private List<Quest> returnAllIndividualQuest(int studentID)
        {
            List<OwnedQuestStudent> allOwnedQuestStudent = _ownedStudentDAO.FetchAllRecords(studentID);
            List<Quest> allStudentQuest = new List<Quest>();
            foreach(OwnedQuestStudent ownedQuestStudent in allOwnedQuestStudent)
            {
                var model = _questDAO.FindOneRecordBy(ownedQuestStudent.QuestId);
                model.QuestStatus = ownedQuestStudent.CompletionStatus;
                allStudentQuest.Add(model);
            }
            return allStudentQuest;
        }

        private List<Quest> returnAllGroupQuest(int groupID)
        {
            List<OwnedQuestGroup> allOwnedQuestGroup = _ownedGroupDAO.FetchAllRecords(groupID);
            List<Quest> allGroupQuest = new List<Quest> { };
            foreach (OwnedQuestGroup ownedQuestGroup in allOwnedQuestGroup)
            {
                var model = _questDAO.FindOneRecordBy(ownedQuestGroup.QuestId);
                model.QuestStatus = ownedQuestGroup.CompletionStatus;
                allGroupQuest.Add(model);
            }
            return allGroupQuest;
        }

        public List<Quest> returnListOfAllQuest(int studentID, int groupID)
        {
            var ListIndividualQuest = returnAllIndividualQuest(studentID);
            var ListGroupQuest = returnAllGroupQuest(groupID);
            ListGroupQuest.ForEach(item => ListIndividualQuest.Add(item));
            List<Quest> ListOfAllQuest = new List<Quest> { };
            ListIndividualQuest.ForEach(item => ListOfAllQuest.Add(item));
            ListGroupQuest.ForEach(item => ListOfAllQuest.Add(item));
            return ListOfAllQuest;
        }

        public void ClaimIndividualQuest(OwnedQuestStudent claimedOwnedQuest)
        {
            _ownedStudentDAO.AddRecord(claimedOwnedQuest);
        }

        public void ClaimGroupQuest(OwnedQuestGroup claimedOwnedQuest)
        {
            _ownedGroupDAO.AddRecord(claimedOwnedQuest);
        }

        public void DeclaimIndividualQuest(int id)
        {
            _ownedStudentDAO.DeleteRecord(id);
        }

        public void DeclaimGroupQuest(int id)
        {
            _ownedGroupDAO.DeleteRecord(id);
        }
    }
}
