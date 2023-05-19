namespace Libreria
{
    public class Operacion
    {
        public List<int> NumerosImpares = new();
        public int SumarNumeros(int numero1, int numero2)
        {
            return numero1 + numero2;
        }

        public bool IsValorPar(int numero)
        {
            return numero % 2 == 0;
        }

        public double SumarDecimal(double decimal1, double decimal2)
        {
            return decimal1 + decimal2;
        }

        // Test en colecciones
        public List<int> GetListaNumeroImpares(int intervaloMinimo, int intervaloMaximo)
        {
            NumerosImpares.Clear();
            for (int i = intervaloMinimo; i <= intervaloMaximo; i++)
            {
                if ( i % 2 != 0 )
                {
                    NumerosImpares.Add(i);
                }
            }

            return NumerosImpares;
        }
    }
}