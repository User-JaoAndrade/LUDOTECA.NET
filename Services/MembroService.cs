using LUDOTECA.Models;
using LUDOTECA.Utils;
using LUDOTECA.Exceptions;

namespace LUDOTECA.Service
{
    public static class MembroService
    {
        public static void CadastrarMembro(Biblioteca biblioteca)
        {
            while (true)
            {
                try // [AV1-5]
                {
                    Console.WriteLine("\n=== CADASTRAR MEMBRO ===\n");

                    Console.Write("Nome do membro: ");
                    string nome = Helpers.LerEntradaDeDados();

                    Membro novoMembro = new Membro(nome, biblioteca.Membros.Values.ToList());
                    biblioteca.AdicionarMembro(novoMembro);

                    Biblioteca.SalvarBiblioteca(biblioteca);
                    RelatorioService.AtualizarRelatorio(biblioteca);
                    Logger.Log($"NOVO USUARIO: {nome} ({novoMembro.Id}) cadastrado com sucesso");

                    novoMembro.MostrarInfo();
                    break;
                }
                catch (ArgumentException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
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
                    Console.WriteLine($"ERRO inesperado: {ex.Message}");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
            }
        }
    }
}
