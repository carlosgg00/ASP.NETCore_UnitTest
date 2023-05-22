using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria
{
    public interface IloggerGeneral
    {
        public int PrioridadLogger {  get; set; }
        public string TipoLogger { get; set; }

        void Message(string message);
        bool LogDatabase(string message);
        bool LogBalanceDespuesDeRetiro(int balanceDespuesDeRetiro);
        string MessageConReturnString(string message);
        bool MessageConOutParamteroReturnBooleano(string str, out string outputStr);

        bool MessageConObjetoReferenciaReturnBooleano(ref Cliente cliente);


    }
    public class LoggerGeneral : IloggerGeneral
    {
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        public bool LogBalanceDespuesDeRetiro(int balanceDespuesDeRetiro)
        {
            if (balanceDespuesDeRetiro >= 0 )
            {
                Console.WriteLine("Exito de la operacion");
                return true;
            }

            Console.WriteLine("Error");
            return false;
        }

        public bool LogDatabase(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public bool MessageConObjetoReferenciaReturnBooleano(ref Cliente cliente)
        {
            return true;
        }

        public bool MessageConOutParamteroReturnBooleano(string str, out string outputStr)
        {
            outputStr = "Hola" + str;
            return true;
        }

        public string MessageConReturnString(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }
    }

    public class LoggerFake : IloggerGeneral
    {
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        public bool LogBalanceDespuesDeRetiro(int balanceDespuesDeRetiro)
        {
            return false;
        }

        public bool LogDatabase(string message)
        {
            return false;
        }

        public void Message(string message)
        {
            
        }

        public bool MessageConObjetoReferenciaReturnBooleano(ref Cliente cliente)
        {
            return true;
        }

        public bool MessageConOutParamteroReturnBooleano(string str, out string outputStr)
        {
            outputStr = "";
            return true;
        }

        public string MessageConReturnString(string message)
        {
            return message;
        }
    }
}
