using Libreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaMSTest
{
    [TestClass]
    public class OperacionMSTest
    {
        [TestMethod]
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
    }
}
