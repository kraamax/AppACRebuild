using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace AppAC.Domain.Test
{
    class JefeDptoTest
    {
         [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LaFechaDeInicioNoPuedeSerMayorAlaFechaFinal()
        {
            Assert.Pass();
        }
    }
}
