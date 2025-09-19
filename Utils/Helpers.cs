using System.Collections;

namespace LUDOTECA.Utils
{
    public static class Helpers
    {
        public static void AnimacaoDePontos(int tempo)
        {
            for (int segundo = 0; segundo < tempo; segundo += 1)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.WriteLine("");
        }

        public static string LerEntradaDeDados()
        {
            string? entrada_obrigatoria = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada_obrigatoria))
                throw new ArgumentException("ERRO: Valor nulo ou em branco, informe um valor válido");

            return entrada_obrigatoria;
        }

        public static void MensagemDeExcessao(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public static bool VerificarSeUsuarioDesejaContinuar()
        {
            Console.Write("\n\nDeseja tentar novamente?\n" +
                              "1- Sim\nQualquer outra tecla- Não\n\n-> ");
            string? opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    return true;
                default:
                    return false;
            }
        }
    }
}
