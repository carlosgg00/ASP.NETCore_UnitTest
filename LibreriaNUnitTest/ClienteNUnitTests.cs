using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria
{
    [TestFixture]
    public class ClienteNUnitTests
    {
        // inicializando objeto cliente para reutilizarlo
        private Cliente cliente;
        [SetUp]
        public void Setup()
        {
            cliente = new Cliente();
        }

        //Test para concatenacion de texto
        [Test]
        public void CrearNombreCompletoTest_InputNombreApellido_ReturnNombreCompleto()
        {
            // 1. Arrange
            //Cliente cliente = new();

            // 2.Act
            cliente.CrearNombreCompleto("Carlos", "Garcia");

            // 3. Assert
            //Assert.Multiple ayuda a describir todos los errores o logs de manera multiple
            Assert.Multiple(() =>
            {
                Assert.That(cliente.ClienteNombre, Is.EqualTo("Carlos Garcia"));
                // Assert normal
                Assert.AreEqual(cliente.ClienteNombre, "Carlos Garcia");

                // que este algo especifico en el texto
                Assert.That(cliente.ClienteNombre, Does.Contain("Garcia"));

                // ignorar mayusculas y minusculas
                Assert.That(cliente.ClienteNombre, Does.Contain("garcia").IgnoreCase);

                // que empiece con un valor determinado
                Assert.That(cliente.ClienteNombre, Does.StartWith("Carlos"));

                // que termine con un valor determinado
                Assert.That(cliente.ClienteNombre, Does.EndWith("Garcia"));
            });
                        
        }

        [Test]
        public void ClienteNombreTest_NoValues_ReturnNull()
        {
            // 1. Arrange
            //Cliente cliente = new();

            // 2.Act
            //No ejecuta ningun metodo para forzar un nulo
            //cliente.CrearNombreCompleto("Carlos", "Garcia");

            // 3. Assert
            Assert.IsNull(cliente.ClienteNombre);
        }

        [Test]
        public void DescuentoEvaluacion_DefaultCliente_ReturnDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
            Assert.That(descuento, Is.InRange(5, 24));
        }

        [Test]
        public void CrearNombreCompleto_InputNombre_ReturnNotNull()
        {
            cliente.CrearNombreCompleto("Carlos", "");

            Assert.IsNotNull(cliente.ClienteNombre);
            Assert.IsFalse(string.IsNullOrEmpty(cliente.ClienteNombre));
        }

        [Test]
        public void ClienteNombre_InputNombreEnBlanco_ReturnThrowException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>( () => cliente.CrearNombreCompleto("", "Garcia") );
            Assert.AreEqual("El nombre está en blanco", exceptionDetalle.Message);

            Assert.That(() =>
                cliente.CrearNombreCompleto("", "Garcia"), Throws.ArgumentException.With.Message.EqualTo("El nombre está en blanco")
            );

            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Garcia"));
            Assert.That(() =>
                cliente.CrearNombreCompleto("", "Garcia"), Throws.ArgumentException
            );
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMenosDe500OrdenTotal_ReturnClienteBasico()
        {
            cliente.OrderTotal = 150;
            var resultado = cliente.GetClienteDetalle();
            Assert.That( resultado, Is.TypeOf<ClienteBasico>() );
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMasDe500OrdenTotal_ReturnClientePremium()
        {
            cliente.OrderTotal = 650;
            var resultado = cliente.GetClienteDetalle();
            Assert.That(resultado, Is.TypeOf<ClientePremium>());
        }
    }
}
