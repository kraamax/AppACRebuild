using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace AppAC.FrontEnd.Test
{
    public class UnitTest1:IDisposable
    {
        
        public IWebDriver driver;

        public UnitTest1()
        {
            driver = new ChromeDriver();
        }
        public void Dispose()
        {
            driver.Close();
            driver.Quit();
        }
        [Fact]
        //public void LoginTests(string user, string password, string cargo)
        
        public void LoginTests()
        {
            string user="gola";
            string password = "gola";
            string cargo = "hola";
            driver.Navigate().GoToUrl("http://localhost:4200/login");
            IWebElement txtUser = driver.FindElement(By.Id("userName"));
            txtUser.SendKeys(user);
            IWebElement txtPassword = driver.FindElement(By.Id("password"));
            txtPassword.SendKeys(password);
            IWebElement login = driver.FindElement(By.Id("btn-login"));
            login.Click();
            Thread.Sleep(3000);
           
        }
    }
}