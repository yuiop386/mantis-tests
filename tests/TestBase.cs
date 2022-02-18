using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = false;
        protected ApplicationManager app;
        public static Random Rnd = new Random();

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static string GenerateRandomString(int maxLength)
        {
            int length = Convert.ToInt32(Rnd.NextDouble() * maxLength);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(Convert.ToChar(32 + Convert.ToInt32(Rnd.NextDouble() * 65)));
            }
            return sb.ToString();
        }
    }
}