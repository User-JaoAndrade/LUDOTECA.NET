using LUDOTECA.Models;
using LUDOTECA.Utils;
using System.Collections.Generic;
using System.Linq;

namespace LUDOTECA.Services
{
    public static class JogoService
    {
        public static Dictionary<int, Jogo> Lista_de_jogos = new Dictionary<int, Jogo>();
        private static string CaminhoJson = "Data/Biblioteca.json";

        public static void SalvarJogos()
        {
            JsonHelper.SalvarLista(Lista_de_jogos.Values.ToList(), CaminhoJson);
        }

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
