using LUDOTECA.Models;
using LUDOTECA.Service;
using LUDOTECA.Utils;

namespace LUDOTECA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Programa iniciado");
            Biblioteca biblioteca = Biblioteca.CarregarBiblioteca();
            RemocaoService remocaoService = new RemocaoService();

            while (true)
            {
                Console.Clear();
                Console.Write(@"
===== LUDOTECA.NET =====

1 - Cadastrar Jogo
2 - Cadastrar Membro
3 - Listar Jogos
4 - Listar Membros
5 - Emprestar Jogo
6 - Devolver Jogo
7 - Gerar Relatório
8 - Desinscrever Membro
9 - Remover Jogo da prateleira

0 - Sair

-> ");

                string opcao = Console.ReadLine()?.Trim() ?? "";

                switch (opcao)
                {
                    case "1": JogoService.CadastrarJogo(biblioteca); break;
                    case "2": MembroService.CadastrarMembro(biblioteca); break;
                    case "3": ListagemService.ListarJogos(biblioteca); break;
                    case "4": ListagemService.ListarMembros(biblioteca); break;
                    case "5": EmprestimoService.EmprestarJogo(biblioteca); break;
                    case "6": DevolucaoService.DevolverJogo(biblioteca); break;
                    case "7": RelatorioService.MostrarRelatorio(); break;
                    case "8": remocaoService.ExcluirMembro(biblioteca); break;
                    case "9": remocaoService.ExcluirJogo(biblioteca); break;
                    case "0":
                        Logger.Log("Programa finalizado");
                        Console.WriteLine("\nAté mais!");
                        return;
                    default:
                        Console.WriteLine("ERRO: Opção inválida! Informe um número de 0 a 9.");
                        if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                        break;
                }
            }
        }
    }
}
