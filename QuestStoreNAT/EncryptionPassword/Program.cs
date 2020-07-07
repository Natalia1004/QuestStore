using LoginForm.Services;
using System;
using System.Collections.Generic;

namespace EncryptionPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 60; i++)
            {

            }
            var newSALT = EncryptPassword.CreateSALT();
        }
    }
}
