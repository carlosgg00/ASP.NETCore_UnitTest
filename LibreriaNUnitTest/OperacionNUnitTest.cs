using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria
{
    [TestFixture]
    public class OperacionNUnitTest
    {
        [Test]
        public void SumarNumeros_InputDosNumeros_GetValorCorrecto()
        {
            //1. Arrange: inicializacion de las valores que se van a probar
            Operacion op = new();
            int numero1Test = 50;
            int numero2Test = 69;

            //2. Act: Representa la ejecucion de la operacion
            int resultado = op.SumarNumeros(numero1Test, numero2Test);

            //3. Assert: Comparador de resultados
            Assert.AreEqual(119, resultado);

            //Para hacer el test, se usa la pestaña llamada prueba, seguido de explorador de pruebas

            //Hay dos formas de ejecutar el test: si solo quiero ejecutar uno o todos

            // Para ejecutar solo una prueba se ejecuta justo de bajo donde dice [TestMethod] aparecerá un icono de admiración
            // indicando que aún no se ejecuta, hay que pulsarlo para ejecutarlo

            //Si aparece en verde, quiere decir que funcionó correctamente
            // Si está en rojo es que falló
        }

        [Test]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(7, ExpectedResult = false)]
        public bool IsValorPar_InputNumeroImpar_ReturnFalse(int numeroImpar)
        {
            //1. Arrange
            Operacion op = new();

            //2. Act
            return op.IsValorPar(numeroImpar);

            //3. Assert
            //Assert.IsFalse(isPar);
            //Assert.That(isPar, Is.EqualTo(false));
        }

        [Test]

        //Numeros de prueba con [TestCase]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            Operacion op = new();
            
            bool isPar = op.IsValorPar(numeroPar);

            Assert.IsTrue(isPar);
            Assert.That(isPar, Is.EqualTo(true));
        }

        [Test]
        [TestCase(2.2, 1.2)] // resultado debe ser 3.4
        [TestCase(2.23, 1.24)] // resultado debe ser 3.47
        public void SumarDecimal_InputDosDecimales_GetValorCorrecto(double decimal1Test, double decimal2Test)
        {
            //1. Arrange
            Operacion op = new();

            //2. Act
            double resultado = op.SumarDecimal(decimal1Test, decimal2Test);

            // La suma es 3.47
            // El resultado que yo espero es 3.4

            // 3.3 - 3.5, todo lo que esté en este intervalo, pasará el test

            //3. Assert
            //El resultado que yo espero es 3.4 - La suma es 3.47 - intervalo de 0.1
            Assert.AreEqual(3.4, resultado, 0.1);

        }

        [Test]
        public void GetListaNumeroImpares_InputMinimoMaximoIntervalos_ReturnListaNumerosImpares()
        {
            //1. Arrange
            Operacion op = new();
            List<int> numerosImparesEsperados = new() { 5, 7, 9 };

            //2. Act
            List<int> resultados = op.GetListaNumeroImpares(5, 10);

            //3. Assert
            Assert.That(resultados, Is.EquivalentTo(numerosImparesEsperados));
            Assert.AreEqual(numerosImparesEsperados, resultados);
            Assert.That(resultados, Does.Contain(5));
            Assert.Contains(5, resultados);
            Assert.That(resultados, Is.Not.Empty);
            Assert.That(resultados.Count, Is.EqualTo(3));
            Assert.That(resultados, Has.No.Member(100));
            Assert.That(resultados, Is.Ordered);
            Assert.That(resultados, Is.Unique);

        }
    }
}
