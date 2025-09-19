using LUDOTECA.Models;
using LUDOTECA.Utils;

namespace LUDOTECA.Services
{
    public static class JogoService
    {
        // Dicionário que armazena todos os jogos cadastrados, indexados pelo ID único de cada jogo
        public static Dictionary<int, Jogo> Lista_de_jogos = new Dictionary<int, Jogo>();

        // Caminho do arquivo JSON
        private static string CaminhoJson = "Data/Biblioteca.json";

        // Salva a lista atual de jogos no JSON
        public static void SalvarJogos()
        {
            JsonHelper.SalvarLista(Lista_de_jogos.Values.ToList(), CaminhoJson);
        }

        /// <summary>
        /// Realiza o cadastro de um novo jogo na biblioteca:
        /// - Solicita os dados do usuário (nome, categoria, ano de lançamento).
        /// - Cria um novo objeto Jogo com ID único.
        /// - Adiciona o jogo à lista em memória.
        /// - Persiste a alteração no arquivo JSON.
        /// - Mostra os detalhes do jogo cadastrado.
        /// </summary>
        public static void CadastrarJogo()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n\n\n\n==== CADASTRANDO NOVO JOGO ====\n");

                    Console.Write("Nome do jogo: ");
                    string nome_do_jogo = Helpers.LerEntradaDeDados();

                    Console.Write("Categoria do jogo: ");
                    string categoria_do_jogo = Helpers.LerEntradaDeDados();

                    Console.Write("Ano de Lançamento: ");
                    int ano_de_lancamento = Convert.ToInt32(Helpers.LerEntradaDeDados());

                    // Instanciando o novo objeto Jogo e adicionando na lista de objetos
                    Jogo novo_jogo = new Jogo(nome_do_jogo, categoria_do_jogo, ano_de_lancamento, Lista_de_jogos.Values.ToList());
                    Lista_de_jogos.Add(novo_jogo._Id, novo_jogo);

                    SalvarJogos();
                    novo_jogo.MostrarNovoJogoCadastrado();
                    break;
                }
                catch (Exception ex)
                {
                    Helpers.MensagemDeExcessao(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar())
                        break;
                }
            }
        }
    }
}
