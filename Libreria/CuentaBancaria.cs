using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria
{
    public class CuentaBancaria
    {
        public int balance { get; set; }
        private readonly IloggerGeneral _loggerGeneral;
        public CuentaBancaria(IloggerGeneral loggerGeneral)
        {
            balance = 0;
            _loggerGeneral = loggerGeneral;
        }

        public bool Deposito(int monto)
        {
            _loggerGeneral.Message("Está depositando la cantidad de " + monto);
            _loggerGeneral.Message("Es otro texto");
            _loggerGeneral.Message("Visita vaxidrez.com");
            _loggerGeneral.PrioridadLogger = 100;
            var prioridad = _loggerGeneral.PrioridadLogger;
            balance += monto;
            return true;
        }

        public bool Retiro(int monto)
        {
            if (balance <= balance)
            {
                _loggerGeneral.LogDatabase("Monto de retiro " + monto.ToString());
                balance -= monto;
                return _loggerGeneral.LogBalanceDespuesDeRetiro(balance);
            }

            return _loggerGeneral.LogBalanceDespuesDeRetiro(balance-monto);
        }

        public int GetBalance()
        {
            return balance;
        }
    }
}
