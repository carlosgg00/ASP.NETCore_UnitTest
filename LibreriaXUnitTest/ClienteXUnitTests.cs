
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Libreria
{
    public class ClienteXUnitTests
    {
        // inicializando objeto cliente para reutilizarlo
        private Cliente cliente;
        

        public ClienteXUnitTests()
        {
            cliente = new Cliente();
        }

        //Test para concatenacion de texto
        [Fact]
        public void CrearNombreCompletoTest_InputNombreApellido_ReturnNombreCompleto()
        {
            // 1. Arrange
            //Cliente cliente = new();

            // 2.Act
            cliente.CrearNombreCompleto("Carlos", "Garcia");

            // 3. Assert
            //Assert.Multiple ayuda a describir todos los errores o logs de manera multiple

                // Assert normal
                Assert.Equal("Carlos Garcia", cliente.ClienteNombre);

                // que este algo especifico en el texto
                Assert.Contains("Garcia", cliente.ClienteNombre);

                // que empiece con un valor determinado
                Assert.StartsWith("Carlos", cliente.ClienteNombre);

                // que termine con un valor determinado
                Assert.EndsWith("Garcia", cliente.ClienteNombre);

        }

        [Fact]
        public void ClienteNombreTest_NoValues_ReturnNull()
        {
            // 1. Arrange
            //Cliente cliente = new();

            // 2.Act
            //No ejecuta ningun metodo para forzar un nulo
            //cliente.CrearNombreCompleto("Carlos", "Garcia");

            // 3. Assert
            Assert.Null(cliente.ClienteNombre);
        }

        [Fact]
        public void DescuentoEvaluacion_DefaultCliente_ReturnDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
            Assert.InRange(descuento, 5, 24);
        }

        [Fact]
        public void CrearNombreCompleto_InputNombre_ReturnNotNull()
        {
            cliente.CrearNombreCompleto("Carlos", "");

            Assert.NotNull(cliente.ClienteNombre);
            Assert.False(string.IsNullOrEmpty(cliente.ClienteNombre));
        }

        [Fact]
        public void ClienteNombre_InputNombreEnBlanco_ReturnThrowException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Garcia"));
            Assert.Equal("El nombre está en blanco", exceptionDetalle.Message);

            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Garcia"));

        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMenosDe500OrdenTotal_ReturnClienteBasico()
        {
            cliente.OrderTotal = 150;
            var resultado = cliente.GetClienteDetalle();
            //Assert.That(resultado, Is.TypeOf<ClienteBasico>());

            Assert.IsType<ClienteBasico>(resultado);
        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMasDe500OrdenTotal_ReturnClientePremium()
        {
            cliente.OrderTotal = 650;
            var resultado = cliente.GetClienteDetalle();
            Assert.IsType<ClientePremium>(resultado);

        }
    }
}
