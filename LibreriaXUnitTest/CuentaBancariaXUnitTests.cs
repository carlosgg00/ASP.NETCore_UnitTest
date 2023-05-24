using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Libreria
{
    public class CuentaBancariaXUnitTests
    {
        private CuentaBancaria cuenta;


        [Fact]
        public void Deposito_InputMonto100LoggerFake_ReturnTrue()
        {
            CuentaBancaria cuentaBancaria = new CuentaBancaria(new LoggerFake());
            var resultado = cuentaBancaria.Deposito(100);

            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());
        }

        [Fact]
        public void Deposito_InputMonto100Mocking_ReturnTrue()
        {
            var mocking = new Mock<IloggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(mocking.Object);
            var resultado = cuentaBancaria.Deposito(100);

            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void Retiro_Retiro100ConBalance200_ReturnTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<IloggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<String>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesDeRetiro(It.Is<int>(x => x > 0))).Returns(true);



            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);
            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.True(resultado);
        }

        [Theory]
        [InlineData(200, 300)]
        public void Retiro_Retiro300ConBalance200_ReturnFalse(int balance, int retiro)
        {
            var loggerMock = new Mock<IloggerGeneral>();
            //loggerMock.Setup(u => u.LogBalanceDespuesDeRetiro(It.Is<int>(x => x < 0))).Returns(false);
            loggerMock.Setup(u => u.LogBalanceDespuesDeRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);



            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);
            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.False(resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();
            string textoPrueba = "hola mundo";

            loggerGeneralMock.Setup(u => u.MessageConReturnString(It.IsAny<string>())).Returns<string>(str => str.ToLower());

            var resultado = loggerGeneralMock.Object.MessageConReturnString("hoLA MUndo");

            Assert.Equal(textoPrueba, resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();
            string textoprueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParamteroReturnBooleano(It.IsAny<string>(), out textoprueba)).Returns(true);

            string parametroOut = "";
            var resultado = loggerGeneralMock.Object.MessageConOutParamteroReturnBooleano("Carlos", out parametroOut);
            Assert.True(resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingObjetoRef_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();
            Cliente cliente = new();
            Cliente clienteNoUsado = new();

            loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBooleano(ref cliente)).Returns(true);

            Assert.True(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBooleano(ref cliente));

            Assert.False(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBooleano(ref clienteNoUsado));
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();

            loggerGeneralMock.Setup(u => u.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(u => u.PrioridadLogger).Returns(10);

            Assert.Equal("warning", loggerGeneralMock.Object.TipoLogger);
            Assert.Equal(10, loggerGeneralMock.Object.PrioridadLogger);

            // CALLBACKS

            string textoTemporal = "Carlos";
            loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback((string parametro) => textoTemporal += parametro);

            loggerGeneralMock.Object.LogDatabase("Garcia");

            Assert.Equal("CarlosGarcia", textoTemporal);
        }

        [Fact]
        public void CuentaBancariaLogger_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<IloggerGeneral>();

            CuentaBancaria cuentaBancaria = new(loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);

            Assert.Equal(100, cuentaBancaria.GetBalance());

            // Verifica cuantas veces el mock esta llamando al metodo .message

            loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3));

            loggerGeneralMock.Verify(u => u.Message("Visita vaxidrez.com"), Times.AtLeastOnce);

            loggerGeneralMock.VerifySet(u => u.PrioridadLogger = 100, Times.Once);

            loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.AtLeastOnce);
        }
    }
}
