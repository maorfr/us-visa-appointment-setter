using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;

namespace Frames.Pages
{
    public class LoginPage
    {
        private string Url = "https://ais.usvisa-info.com/he-il/niv/users/sign_in";

        private IWebElement UserEmail { get { return DriverManager.Driver.FindElement(By.Id("user_email")); } }
        private IWebElement Password { get { return DriverManager.Driver.FindElement(By.Id("user_password")); } }

        private IWebElement LoginButton { get { return DriverManager.Driver.FindElement(By.Name("commit")); } }

        public void Navigate()
        {
            DriverManager.Driver.Navigate().GoToUrl(Url);
        }

        public void Login(string email, string password)
        {
            DriverManager.Start();
            Navigate();
            UserEmail.SendKeys(email);
            Password.SendKeys(password);
        }

        public void ContinueLogin()
        {
            LoginButton.Click();
        }
    }
}
