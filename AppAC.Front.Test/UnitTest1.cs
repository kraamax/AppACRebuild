using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace AppAC.Front.Test
{
    [Ignore("")]
    public class Tests
    {
        public IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Dispose()
        {
            driver.Close();
            driver.Quit();
        }
        [TestCase("sad", "asdas","Credenciales incorrectas",TestName = "LoginInvalido")]
        [TestCase("prueba1@prueba", "777","Login exitoso",TestName = "LoginValido")]
        [Test]
        public void LoginTests(string user, string password, string result)
        {
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
            toast.Text.Should().Contain(result);
        }
        [Test]
        public void AgregarDocenteTest()
        {
            LoginTests("prueba1@prueba","777","Login exitoso");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("btn-docente")).Click();
            driver.FindElement(By.Id("btn-addDocente")).Click();
            IWebElement txtNombres = driver.FindElement(By.Id("nombres"));
            txtNombres.SendKeys("Julio");
            driver.FindElement(By.Id("apellidos")).SendKeys("Perez");
            driver.FindElement(By.Id("id")).SendKeys("9583zz");
            driver.FindElement(By.Id("email")).SendKeys("test@tes");
            // select the drop down list
            driver.FindElement(By.Id("sexo")).Click();
            driver.FindElement(By.Id("mat-option-1")).Click();
            driver.FindElement(By.Id("btn-guardar")).Click();
            Thread.Sleep(3000);
        }
        [Test]
        public void AsignarActividad()
        {
            LoginTests("prueba1@prueba","777","Login exitoso");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("btn-docente")).Click();
            driver.FindElement(By.Id("btn-addDocente")).Click();
            IWebElement txtNombres = driver.FindElement(By.Id("nombres"));
            txtNombres.SendKeys("Julio");
            driver.FindElement(By.Id("apellidos")).SendKeys("Perez");
            driver.FindElement(By.Id("id")).SendKeys("9583zz");
            driver.FindElement(By.Id("email")).SendKeys("test@tes");
            // select the drop down list
            driver.FindElement(By.Id("sexo")).Click();
            driver.FindElement(By.Id("mat-option-1")).Click();
            driver.FindElement(By.Id("btn-guardar")).Click();
            Thread.Sleep(3000);
        }
    }
}