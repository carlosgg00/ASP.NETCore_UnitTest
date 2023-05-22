using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria
{
    [TestFixture]
    internal class CuentaBancariaNUnitTests
    {
        private CuentaBancaria cuenta;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Deposito_InputMonto100LoggerFake_ReturnTrue()
        {
            CuentaBancaria cuentaBancaria = new CuentaBancaria(new LoggerFake());
            var resultado = cuentaBancaria.Deposito(100);

            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));
        }

        [Test]
        public void Deposito_InputMonto100Mocking_ReturnTrue()
        {
            var mocking = new Mock<IloggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(mocking.Object);
            var resultado = cuentaBancaria.Deposito(100);

            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));
        }

        [Test]
        [TestCase(200, 100)]
        [TestCase(200, 150)]
        public void Retiro_Retiro100ConBalance200_ReturnTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<IloggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<String>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesDeRetiro(It.Is<int>(x => x > 0))).Returns(true);



            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);
            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.IsTrue(resultado);
        }

        [Test]
        [TestCase(200, 300)]
        public void Retiro_Retiro300ConBalance200_ReturnFalse(int balance, int retiro)
        {
            var loggerMock = new Mock<IloggerGeneral>();
            //loggerMock.Setup(u => u.LogBalanceDespuesDeRetiro(It.Is<int>(x => x < 0))).Returns(false);
            loggerMock.Setup(u => u.LogBalanceDespuesDeRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive ))).Returns(false);



            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);
            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();
            string textoPrueba = "hola mundo";

            loggerGeneralMock.Setup(u => u.MessageConReturnString(It.IsAny<string>())).Returns<string>( str => str.ToLower() );

            var resultado = loggerGeneralMock.Object.MessageConReturnString("hoLA MUndo");

            Assert.That( resultado, Is.EqualTo(textoPrueba));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();
            string textoprueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParamteroReturnBooleano(It.IsAny<string>(), out textoprueba)).Returns(true);

            string parametroOut = "";
            var resultado = loggerGeneralMock.Object.MessageConOutParamteroReturnBooleano("Carlos", out parametroOut);
            Assert.IsTrue(resultado);
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingObjetoRef_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();
            Cliente cliente = new();
            Cliente clienteNoUsado = new();

            loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBooleano(ref cliente)).Returns(true);

            Assert.IsTrue(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBooleano(ref cliente));

            Assert.IsFalse(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBooleano(ref clienteNoUsado));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();

            loggerGeneralMock.Setup(u => u.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(u => u.PrioridadLogger).Returns(10);

            Assert.That(loggerGeneralMock.Object.TipoLogger, Is.EqualTo("warning"));
            Assert.That(loggerGeneralMock.Object.PrioridadLogger, Is.EqualTo(10));

            // CALLBACKS

            string textoTemporal = "Carlos";
            loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback( (string parametro) => textoTemporal += parametro );

            loggerGeneralMock.Object.LogDatabase("Garcia");

            Assert.That(textoTemporal, Is.EqualTo("CarlosGarcia"));
        }

        [Test]
        public void CuentaBancariaLogger_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();

            CuentaBancaria cuentaBancaria = new (loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);

            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));

            // Verifica cuantas veces el mock esta llamando al metodo .message

            loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3));

            loggerGeneralMock.Verify(u => u.Message("Visita vaxidrez.com"), Times.AtLeastOnce);

            loggerGeneralMock.VerifySet(u => u.PrioridadLogger=100, Times.Once);

            loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.AtLeastOnce);
        }
    }
}
