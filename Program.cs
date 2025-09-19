using LUDOTECA.Utils;
using LUDOTECA.Services;
using LUDOTECA.Service;

namespace LUDOTECA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("\n\n===== LUDOTECA.NET =====\n\n" +
                              "1 - Cadastrar jogo\n" +
                              "2 - Cadastrar membro\n" +
                              "3 - Listar jogos\n" +
                              "4 - Emprestar jogo\n" +
                              "5 - Devolver jogo\n" +
                              "6 - Gerar Relatório\n" +
                              "0 - Sair\n\n" +
                              "-> ");
                string? opcao = Console.ReadLine();
                try
                {
                    switch (opcao)
                    {
                        case "0":
                            Console.WriteLine("\nATÉ MAIS, LUDOTECA :D\n");
                            Environment.Exit(0);
                            break;

                        case "1":
                            JogoService.CadastrarJogo();
                            break;

                        case "2":
                            MembroService.CadastrarMembro();
                            break;

                        case "3":
                            ListagemService.ListarJogos();
                            break;

                        case "4":
                            EmprestimoService.EmprestarJogo();
                            break;

                        case "5":
                            DevolucaoService.DevolverJogo();
                            break;

                        case "6":
                            Console.WriteLine("INCREMENTAR RELATORIO");
                            Helpers.AnimacaoDePontos(5);
                            break;

                        default:
                            throw new InvalidOperationException("ERRO: Opção inválida, tente um valor de 0 a 6");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Helpers.MensagemDeExcessao(ex.Message);
                }
            }
        }
    }
}