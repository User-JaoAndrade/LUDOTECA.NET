using LUDOTECA.Models;
using LUDOTECA.Utils;
using LUDOTECA.Exceptions;

namespace LUDOTECA.Service
{
    public class RemocaoService
    {
        public void ExcluirMembro(Biblioteca biblioteca)
        {
            while (true)
            {
                try // [AV1-5]
                {
                    Console.WriteLine("\n=== REMOVER MEMBRO ===\n");
                    Console.Write("ID do membro que deseja excluir: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados(), out int idMembro))

                    if (!biblioteca.Membros.ContainsKey(idMembro))
                        throw new MembroNaoEncontradoException(idMembro);

                    var membro = biblioteca.Membros[idMembro];

                    if (membro.JogoAlugado != "Nenhum")
                        throw new LudotecaException("Não é possível excluir membro que possui jogo alugado.");

                    biblioteca.Membros.Remove(idMembro);
                    Biblioteca.SalvarBiblioteca(biblioteca);
                    RelatorioService.AtualizarRelatorio(biblioteca);
                    Logger.Log($"MEMBRO DELETADO: {membro.Nome} ({membro.Id})");

                    Console.WriteLine($"\nMembro {membro.Nome} removido com sucesso!");
                    break;
                }
                catch (ArgumentException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (FormatException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine("ERRO: Informe um número inteiro.");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (LudotecaException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine($"ERRO: {ex.Message}");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
            }
        }

        public void ExcluirJogo(Biblioteca biblioteca)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n=== REMOVER JOGO ===\n");
                    Console.Write("ID do jogo que deseja excluir: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados(), out int idJogo))

                    if (!biblioteca.Jogos.ContainsKey(idJogo))
                        throw new JogoNaoEncontradoException(idJogo);

                    var jogo = biblioteca.Jogos[idJogo];

                    if (!jogo.Disponivel)
                        throw new LudotecaException("Não é possível excluir jogo que está alugado.");

                    biblioteca.Jogos.Remove(idJogo);
                    Biblioteca.SalvarBiblioteca(biblioteca);
                    RelatorioService.AtualizarRelatorio(biblioteca);
                    Logger.Log($"JOGO DELETADO: {jogo.Nome} ({jogo.Id})");

                    Console.WriteLine($"\nJogo {jogo.Nome} removido com sucesso!");
                    break;
                }
                catch (ArgumentException ex)
                {
                    Logger.LogErro(ex);
                    Console.WriteLine(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (FormatException ex)
                {
                    Logger.LogErro(ex);
                    Console.WriteLine("ERRO: Informe um número inteiro.");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (LudotecaException ex)
                {
                    Logger.LogErro(ex);
                    Console.WriteLine($"ERRO: {ex.Message}");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
            }
        }
    }
}
