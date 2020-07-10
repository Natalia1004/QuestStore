using System;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;

namespace QuestStoreNAT.web.Services
{
    public class ArtifactManagement
    {
        private StudentDAO _student { get; set; }
        private ArtifactDAO _artifact { get; set; }
        private OwnedArtifactGroupDAO _ownedArtifactGroup { get; set; }
        private OwnedArtifactStudentDAO _ownedArtifactStudent { get; set; }
        private GroupDAO _groupDAO { get; set; }

        public ArtifactManagement()
        {
            _student = new StudentDAO();
            _artifact = new ArtifactDAO();
            _ownedArtifactGroup = new OwnedArtifactGroupDAO();
            _ownedArtifactStudent = new OwnedArtifactStudentDAO();
            _groupDAO = new GroupDAO();
        }

        public void UseArtifact(int credentialID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(credentialID);
            var artifactToUse = _artifact.FindOneRecordBy(artifactID);
            int completiotStatus = 0;
            switch(artifactToUse.Type)
            {
                case TypeClassification.Individual:
                    var model =_ownedArtifactStudent.FindOneRecordBy(artifactID, currentStudent.Id, completiotStatus);
                    model.CompletionStatus = 1;
                    _ownedArtifactStudent.UpdateRecord(model);
                    break;
                case TypeClassification.Group:
                    var modelGroup = _ownedArtifactGroup.FindOneRecordBy(artifactID, currentStudent.Id, completiotStatus);
                    modelGroup.CompletionStatus = 1;
                    _ownedArtifactGroup.UpdateRecord(modelGroup);
                    break;
            }
        }

        public void DeleteUsedArtifactFromView(int credentialID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(credentialID);
            var artifactToDelete = _artifact.FindOneRecordBy(artifactID);
            int completiotStatus = 1;
            switch (artifactToDelete.Type)
            {
                case TypeClassification.Individual:
                    var model = _ownedArtifactStudent.FindOneRecordBy(artifactID, currentStudent.Id, completiotStatus);
                    _ownedArtifactStudent.DeleteRecord(model.Id);
                    break;
                case TypeClassification.Group:
                    var modelGroup = _ownedArtifactGroup.FindOneRecordBy(artifactID, currentStudent.GroupID, completiotStatus);
                    _ownedArtifactGroup.DeleteRecord(modelGroup.Id);
                    break;
            }
        }

        public void BuyIndiviudalArtifact(int credentialID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(credentialID);
            var artifactToBuy = _artifact.FindOneRecordBy(artifactID);
            var newRecord = new OwnedArtifactStudent()
            {
                StudentId = currentStudent.Id,
                ArtifactId = artifactID,
                CompletionStatus = 0,
            };
            int currentWalletValue = currentStudent.Wallet - artifactToBuy.Cost;
            currentStudent.Wallet = currentWalletValue;
            UpdateGroupWallet(currentStudent.GroupID, artifactToBuy.Cost);
            new StudentDAO().UpdateRecord(currentStudent);
            _ownedArtifactStudent.AddRecord(newRecord);
        }

        public bool CheckigStudentWallet(int credentialID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(credentialID).Wallet;
            var artifactToBuy = _artifact.FindOneRecordBy(artifactID).Cost;
            if (currentStudent < artifactToBuy)
            {
                return false;
            }
            return true;
        }

        public bool CheckigGroupWallet(int groupID, int artifactID, int credentialID)
        {
            int currentValueGroupWallet = _groupDAO.FindOneRecordBy(groupID).GroupWallet;
            var artifactToBuy = _artifact.FindOneRecordBy(artifactID).Cost;

            if (currentValueGroupWallet > artifactToBuy && CheckingStudentToBoughtGroupArtifact(groupID,credentialID,artifactID) == true)
            {
                return true;
            }
            return false;
        }

        private bool CheckingStudentToBoughtGroupArtifact(int groupID, int credentialID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(credentialID);
            var studentGroup = new GroupDAO().FindOneRecordBy(currentStudent.GroupID);
            var artifactToBuy = _artifact.FindOneRecordBy(artifactID);
            studentGroup.GroupStudents = _student.FetchAllStudentInGroup(groupID);
            int amountStudents = studentGroup.GroupStudents.Count;
            if(currentStudent.Wallet - (artifactToBuy.Cost / amountStudents) > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateGroupWallet(int studentGroup, int artifactCost)
        {
            int currentValueGroupWallet = _groupDAO.FindOneRecordBy(studentGroup).GroupWallet;
            int ValueGroupWalletAfterBought = currentValueGroupWallet - artifactCost;
            _groupDAO.UpdateOnlyGroupWallet(studentGroup, ValueGroupWalletAfterBought);
        }

        public bool CheckingIfTransactionForBoughtGroupArtifactExist(int groupID)
        {
            var groupTransactionDAO = new GroupTransactionDAO();
            if (groupTransactionDAO.FindOneRecordBy(groupID) != null)
            {
                return false;
            }
            return true;
        }

        public void CreateNewGroupTransaction(int artifactID, int GroupID)
        {
            var groupTransaction = new GroupTransactionDAO();
            var studentGroup = new GroupDAO().FindOneRecordBy(GroupID);
            studentGroup.GroupStudents = _student.FetchAllStudentInGroup(GroupID);
            int amountOfStudentInGroup = studentGroup.GroupStudents.Count;
            var newRecordGroupTransaction = new GroupTransaction()
            {
                artifactID = artifactID,
                groupID = GroupID,
                numberOfStudents = amountOfStudentInGroup,
                numberOfAcceptance = 1
            };
            groupTransaction.AddRecord(newRecordGroupTransaction);
        }

        public void CreateRecordForAcceptance(int credentialID, int groupID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(credentialID);
            var studentGroup = new GroupDAO().FindOneRecordBy(groupID);
            studentGroup.GroupStudents = _student.FetchAllStudentInGroup(groupID);
            var studentAcceptance = new StudentAcceptanceDAO();

            foreach (Student student in studentGroup.GroupStudents)
            {
                if (student.Id != currentStudent.Id)
                {
                    var newStudentAcceptance = new StudentAcceptance()
                    {
                        studentID = student.Id,
                        artifactID = artifactID,
                        acceptance = 0,
                        groupID = groupID
                    };
                    studentAcceptance.AddRecord(newStudentAcceptance);
                }

            }
        }

    }
}
