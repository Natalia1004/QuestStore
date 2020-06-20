using System;
namespace QuestStoreNAT.web.Services
{
    public class LevelStudent
    {
        public int levelStudent(int overWallet)
        {
            int levelStudent = 0;

            if (overWallet >= 0 && overWallet < 100)
            {
                return levelStudent = 1;

            }
            else if (overWallet >= 100 && overWallet < 200)
            {
                return levelStudent = 2;

            }
            else if (overWallet >= 200 && overWallet < 300)
            {
                return levelStudent = 3;

            }
            else if (overWallet >= 300 && overWallet < 400)
            {
                return levelStudent = 4;

            }
            else if (overWallet >= 400 && overWallet < 500)
            {
                return levelStudent = 4;

            }
            else if (overWallet >= 500)
            {
                return levelStudent = 5;
            }

            return levelStudent;
        }
    }
}


