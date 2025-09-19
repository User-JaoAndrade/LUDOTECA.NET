using LUDOTECA.Utils;
using LUDOTECA.Models;

namespace LUDOTECA.Service
{
    public static class EmprestimoService
    {
        public static void EmprestarJogo()
        {
            var lista_de_membros = JsonHelper.CarregarLista<Membro>("Data/Membros.json")
                                            ?.ToDictionary(m => m._Id, m => m)
                                            ?? new Dictionary<int, Membro>();

            var lista_de_jogos = JsonHelper.CarregarLista<Jogo>("Data/Biblioteca.json")
                                           ?.ToDictionary(j => j._Id, j => j)
                                           ?? new Dictionary<int, Jogo>();

            while (true)
            {
                try
                {
                    Console.WriteLine("\n\n\n\n==== Emprestar Jogo ====\n" +
                                      "Prazo de devolução: > 7 DIAS < a partir da data do aluguel\n" +
                                      "Multa: Em caso de ATRASO, cobraremos R$2,00 para CADA dia de atraso\n\n");

                    Console.Write("Informe o ID do membro\n-> ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados().Trim(), out int id_membro))
                        throw new FormatException("ID do membro inválido! Informe um número inteiro.");

                    if (!lista_de_membros.ContainsKey(id_membro))
                        throw new KeyNotFoundException("ATENÇÃO: Membro não encontrado");

                    var membro = lista_de_membros[id_membro];

                    if (membro.Jogo_alugado != "Nenhum")
                        throw new Exception($"\n\n{membro.Nome} já alugou um jogo\n\n" +
                                            $"Nome do jogo: {membro.Jogo_alugado}\n" +
                                            $"Dia do aluguel: {membro.Data_do_aluguel.Date}");

                    Console.Write("Informe o ID do jogo que deseja alugar\n-> ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados().Trim(), out int id_jogo))
                        throw new FormatException("ID do jogo inválido! Informe um número inteiro.");

                    if (!lista_de_jogos.ContainsKey(id_jogo))
                        throw new KeyNotFoundException("ATENÇÃO: Jogo não encontrado");

                    var jogo = lista_de_jogos[id_jogo];

                    if (!jogo.Disponivel)
                        throw new Exception("Jogo já alugado, volte outro dia");

                    jogo.MudarJogoParaIndisponivel();
                    membro.AlterarNomeDoJogoAlugado(jogo.Nome);
                    membro.AlterarDataDoAluguel(DateTime.Now.Date);
                    membro.AlterarDataDaDevolucao(DateTime.Now.Date.AddDays(7.0));

                    JsonHelper.SalvarLista(lista_de_jogos.Values.ToList(), "Data/Biblioteca.json");
                    JsonHelper.SalvarLista(lista_de_membros.Values.ToList(), "Data/Membros.json");

                    Console.Write("Alugando jogo");
                    Helpers.AnimacaoDePontos(3);
                    Console.WriteLine( "\n > JOGO ALUGADO COM SUCESSO <\n\n" +
                                      $"Membro e ID: {membro.Nome} - {membro._Id}\n" +
                                      $"Jogo Alugado: {membro.Jogo_alugado}\n" +
                                      $"Dia do Aluguel: {membro.Data_do_aluguel}\n" +
                                      $"Dia de devolução: {membro.Data_de_devolucao}\n");
                    Console.Write("Aperte ENTER para voltar ao menu...");
                    Console.ReadLine();
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
