using LUDOTECA.Utils;
using LUDOTECA.Models;

namespace LUDOTECA.Service
{
    public static class ListagemService
    {
        public static void ListarJogos()
        {
            Console.WriteLine("\n\n\n\n=== LISTA DE JOGOS ===\n" +
            "=======================================================================\n");

            var lista_de_jogos = JsonHelper.CarregarLista<Jogo>("Data/Biblioteca.json")
                                            ?.ToDictionary(j => j._Id, j => j) ?? new Dictionary<int, Jogo>();


            if (!lista_de_jogos.Any())
            {
                Console.WriteLine("ESTAMOS POBRES, NENHUM JOGO DISPONIVEL");
            }
            else
            {
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

        // public static void ListarMembros()
        // {
        //     Console.WriteLine("=== LISTA DE MEMBROS ===\n");

        //     var membros = JsonHelper.CarregarLista<Membro>("Data/Membros.json")
        //                             ?.ToDictionary(m => m._Id, m => m) ?? new Dictionary<int, Membro>();

        //     if (!membros.Any())
        //     {
        //         Console.WriteLine("Nenhum membro cadastrado.");
        //         return;
        //     }

        //     foreach (var kvp in membros)
        //     {
        //         var membro = kvp.Value;
        //         Console.WriteLine($"          ID: {membro._Id}\n" +
        //                           $"        Nome: {membro.Nome}\n" +
        //                           $"Jogo Alugado: {membro.Jogo_alugado}\n" +
        //                           "=======================================================================\n");
        //     }

        //     Console.Write("Aperte ENTER para continuar...");
        //     Console.ReadLine();
        // }
    }
}
