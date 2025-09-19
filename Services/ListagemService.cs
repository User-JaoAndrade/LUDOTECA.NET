using LUDOTECA.Utils;
using LUDOTECA.Models;

namespace LUDOTECA.Service
{
    public static class ListagemService
    {
        /// <summary>
        /// Lista todos os jogos cadastrados na biblioteca.
        /// - Carrega os jogos a partir do arquivo JSON.
        /// - Exibe ID, nome, categoria, ano de lançamento e disponibilidade.
        /// - Caso não haja jogos, informa que a biblioteca está vazia.
        /// </summary>
        public static void ListarJogos()
        {
            Console.WriteLine("\n\n\n\n=== LISTA DE JOGOS ===\n" +
            "=======================================================================\n");

            var lista_de_jogos = JsonHelper.CarregarLista<Jogo>("Data/Biblioteca.json")
                                            ?.ToDictionary(j => j._Id, j => j) 
                                            ?? new Dictionary<int, Jogo>();

            // Verificando se a lista de objetos Jogo está vazia
            if (!lista_de_jogos.Any())
            {
                Console.WriteLine("ESTAMOS POBRES, NENHUM JOGO DISPONÍVEL.");
            }
            else
            {
                // Percorrendo as chaves do dicionário de objeto Jogo
                foreach (var kvp in lista_de_jogos)
                {
                    var jogo = kvp.Value;
                    string disponibilidade = jogo.Disponivel ? "DISPONÍVEL" : "INDISPONÍVEL";

                    Console.WriteLine($"             ID: {jogo._Id}\n" +
                                      $"           Nome: {jogo.Nome}\n" +
                                      $"      Categoria: {jogo.Categoria}\n" +
                                      $"            Ano: {jogo.AnoDeLancamento}\n" +
                                      $"Disponibilidade: {disponibilidade}\n" +
                                      "=======================================================================\n");
                }
            }

            Console.Write("Aperte ENTER para continuar...");
            Console.ReadLine();
        }

        /// <summary>
        /// Lista todos os membros cadastrados no sistema.
        /// - Carrega os membros a partir do arquivo JSON.
        /// - Exibe ID, nome e jogo alugado (se houver).
        /// - Caso não haja membros, informa que nenhum cadastro foi realizado.
        /// </summary>
        // public static void ListarMembros()
        // {
        //     Console.WriteLine("\n\n\n\n=== LISTA DE MEMBROS ===\n" +
        //                       "=======================================================================\n");

        //     var membros = JsonHelper.CarregarLista<Membro>("Data/Membros.json")
        //                             ?.ToDictionary(m => m._Id, m => m) 
        //                             ?? new Dictionary<int, Membro>();

        //     if (!membros.Any())
        //     {
        //         Console.WriteLine("NENHUM MEMBRO CADASTRADO.");
        //     }
        //     else
        //     {
        //         foreach (var kvp in membros)
        //         {
        //             var membro = kvp.Value;
        //             Console.WriteLine($"          ID: {membro._Id}\n" +
        //                               $"        Nome: {membro.Nome}\n" +
        //                               $"Jogo Alugado: {membro.Jogo_alugado}\n" +
        //                               "=======================================================================\n");
        //         }
        //     }

        //     Console.Write("Aperte ENTER para continuar...");
        //     Console.ReadLine();
        // }
    }
}
