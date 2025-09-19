namespace LUDOTECA.Utils
{
    /// <summary>
    /// Classe utilitária que contém métodos auxiliares
    /// para entrada de dados, mensagens e interações com o usuário.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Exibe uma animação de pontos no console para simular carregamento.
        /// </summary>
        /// <param name="tempo">Duração da animação em segundos.</param>
        public static void AnimacaoDePontos(int tempo)
        {
            for (int segundo = 0; segundo < tempo; segundo++)
            {
                Console.Write(".");
                Thread.Sleep(1000); // Tempo de espera
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// Lê a entrada de dados do usuário pelo console e valida se não está nula ou em branco.
        /// </summary>
        /// <returns>A string digitada pelo usuário.</returns>
        /// <exception cref="ArgumentException">Lançada quando a entrada for nula ou em branco.</exception>
        public static string LerEntradaDeDados()
        {
            string? entrada_obrigatoria = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada_obrigatoria))
                throw new ArgumentException("ERRO: Valor nulo ou em branco, informe um valor válido");

            return entrada_obrigatoria;
        }

        /// <summary>
        /// Exibe uma mensagem de exceção no console.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro a ser exibida.</param>
        public static void MensagemDeExcessao(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        /// <summary>
        /// Pergunta ao usuário se ele deseja tentar novamente determinada ação.
        /// </summary>
        /// <returns><c>true</c> se o usuário optar por continuar, caso contrário <c>false</c>.</returns>
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
