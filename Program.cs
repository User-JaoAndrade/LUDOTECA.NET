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

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": JogoService.CadastrarJogo(biblioteca); break; // [AV1-4-CadastrarJogo]
                    case "2": MembroService.CadastrarMembro(biblioteca); break; // [AV1-4-CadastrarMembro]
                    case "3": ListagemService.ListarJogos(biblioteca); break; // [AV1-4-ListarJogos]
                    case "4": ListagemService.ListarMembros(biblioteca); break; // [AV1-4-ListarMembros]
                    case "5": EmprestimoService.EmprestarJogo(biblioteca); break; // [AV1-4-EmprestarJogo]
                    case "6": DevolucaoService.DevolverJogo(biblioteca); break; // [AV1-4-DevolverJogo]
                    case "7": RelatorioService.MostrarRelatorio(); break; // [AV1-4-Ralatorio]
                    case "8": remocaoService.ExcluirMembro(biblioteca); break; // [AV1-4-RemoverMembro]
                    case "9": remocaoService.ExcluirJogo(biblioteca); break; // [AV1-4-RemoverJogo]
                    case "0":
                        Logger.Log("Programa finalizado");
                        Console.WriteLine("\nAté mais!");
                        return;
                    default:
                        Console.WriteLine("ERRO: Opção inválida! Informe um número de 0 a 9.");
                        Console.Write("Aperte ENTER para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
