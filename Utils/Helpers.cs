namespace LUDOTECA.Utils
{
    public static class Helpers
    {
        public static string LerEntradaDeDados()
        {
            string? entrada = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entrada))
                throw new ArgumentException("Valor inválido. Informe um valor não vazio.");
            return entrada;
        }

        public static bool VerificarSeUsuarioDesejaContinuar()
        {
            Console.Write("\nDeseja tentar novamente?\n1 - Sim\nQualquer outra tecla - Não\n\n-> ");
            string? opcao = Console.ReadLine();
            return opcao == "1";
        }

        public static void AnimacaoDePontos(int segundos)
        {
            for (int i = 0; i < segundos; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }
}
