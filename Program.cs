using LUDOTECA.Utils;
using LUDOTECA.Services;
using LUDOTECA.Service;

namespace LUDOTECA
{
    /// <summary>
    /// Classe principal do sistema Ludoteca.NET.
    /// Contém o menu principal e coordena as operações de cadastro,
    /// listagem, empréstimo, devolução e geração de relatórios.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Ponto de entrada da aplicação.
        /// Exibe o menu principal e executa ações de acordo com a escolha do usuário.
        /// </summary>
        /// <param name="args">Argumentos da linha de comando (não utilizados).</param>
        static void Main(string[] args)
        {
            while (true)
            {
                // Menu principal
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
                        case "0": // Sai do programa
                            Console.WriteLine("\nATÉ MAIS, LUDOTECA :D\n");
                            Environment.Exit(0);
                            break;

                        case "1": // Cadastra um novo jogo
                            JogoService.CadastrarJogo();
                            break;

                        case "2": // Cadastra um novo jogo
                            MembroService.CadastrarMembro();
                            break;

                        case "3": // Mostra a lista de jogos
                            ListagemService.ListarJogos();
                            break;

                        case "4": // Aluga um jogo para o usuário
                            EmprestimoService.EmprestarJogo();
                            break;

                        case "5": // Devolve um jogo alugado
                            DevolucaoService.DevolverJogo();
                            break;

                        case "6":
                            // Placeholder para relatório futuro
                            Console.WriteLine("INCREMENTAR RELATORIO");
                            Helpers.AnimacaoDePontos(5);
                            break;

                        default:
                            throw new InvalidOperationException("ERRO: Opção inválida, tente um valor de 0 a 6");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // Mostra mensagem de erro para o usuário
                    Helpers.MensagemDeExcessao(ex.Message);
                }
            }
        }
    }
}
