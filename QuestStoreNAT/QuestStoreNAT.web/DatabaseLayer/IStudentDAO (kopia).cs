using System;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;

namespace QuestStoreNAT.web.Services
{
    public interface IStudentDAO
    {
        List<Student> GetAllRows();    
    }
}
