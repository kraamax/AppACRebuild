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

        const string skip = "Class X disabled";

        [Fact(Skip=skip)]
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
            Thread.Sleep(1000);
            IWebElement toast = driver.FindElement(By.Id("toast-container"));
            toast.Click();
            Console.WriteLine("hogksadkadkad");
            Thread.Sleep(3000);
           
        }
        [Fact(Skip=skip)]
        public void LoginValidoJefeDptoTests()
        {
            string user="prueba1@prueba";
            string password = "777";
            driver.Navigate().GoToUrl("http://localhost:4200/login");
            IWebElement txtUser = driver.FindElement(By.Id("userName"));
            txtUser.SendKeys(user);
            IWebElement txtPassword = driver.FindElement(By.Id("password"));
            txtPassword.SendKeys(password);
            IWebElement login = driver.FindElement(By.Id("btn-login"));
            login.Click();
            Thread.Sleep(1000);

            
           
        }

        [Fact(Skip=skip)]
        public void AgregarDocenteTest()
        {
            LoginValidoJefeDptoTests();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("btn-docente")).Click();
            driver.FindElement(By.Id("btn-addDocente")).Click();
            /*IWebElement txtUser = driver.FindElement(By.Id("userName"));
            txtUser.SendKeys( "");
            IWebElement txtPassword = driver.FindElement(By.Id("password"));
            txtPassword.SendKeys("password");
            IWebElement login = driver.FindElement(By.Id("btn-login"));
            login.Click();*/
            //id="btn-docente"

           
        }
    }
}