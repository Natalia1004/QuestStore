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

        public ArtifactManagement()
        {
            _student = new StudentDAO();
            _artifact = new ArtifactDAO();
            _ownedArtifactGroup = new OwnedArtifactGroupDAO();
            _ownedArtifactStudent = new OwnedArtifactStudentDAO();
        }

        public void UseArtifact(int studentID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(studentID);
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

        public void DeleteUsedArtifactFromView(int studentID, int artifactID)
        {
            var currentStudent = _student.FindOneRecordBy(studentID);
            var artifactToDelete = _artifact.FindOneRecordBy(artifactID);
            int completiotStatus = 1;
            switch (artifactToDelete.Type)
            {
                case TypeClassification.Individual:
                    var model = _ownedArtifactStudent.FindOneRecordBy(artifactID, currentStudent.Id, completiotStatus);
                    _ownedArtifactStudent.UpdateRecord(model);
                    break;
                case TypeClassification.Group:
                    var modelGroup = _ownedArtifactGroup.FindOneRecordBy(artifactID, currentStudent.Id, completiotStatus);
                    _ownedArtifactGroup.UpdateRecord(modelGroup);
                    break;
            }
        }

    }
}
