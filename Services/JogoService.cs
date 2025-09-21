using LUDOTECA.Models;
using LUDOTECA.Utils;
using LUDOTECA.Exceptions;

namespace LUDOTECA.Service
{
    public static class JogoService
    {
        public static void CadastrarJogo(Biblioteca biblioteca)
        {
            while (true)
            {
                try // [AV1-5]
                {
                    Console.WriteLine("\n=== CADASTRAR JOGO ===\n");

                    Console.Write("Nome do jogo: ");
                    string nome = Helpers.LerEntradaDeDados();

                    Console.Write("Categoria: ");
                    string categoria = Helpers.LerEntradaDeDados();

                    Console.Write("Ano de lançamento: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados(), out int ano))
                        throw new AnoInvalidoException();
                    if (ano > DateTime.Now.Date.Year)
                        throw new AnoInvalidoException();

                    Jogo novoJogo = new Jogo(nome, categoria, ano, biblioteca.Jogos.Values.ToList());
                    biblioteca.AdicionarJogo(novoJogo);

                    Biblioteca.SalvarBiblioteca(biblioteca);
                    RelatorioService.AtualizarRelatorio(biblioteca);
                    Logger.Log($"NOVO JOGO: {nome} ({novoJogo.Id}) cadastrado com sucesso");

                    novoJogo.MostrarInfo();
                    break;
                }
                catch (LudotecaException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine($"ERRO: {ex.Message}");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (Exception ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine($"{ex.Message}");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
            }
        }
    }
}
