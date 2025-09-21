using LUDOTECA.Models;
using System.Text;

namespace LUDOTECA.Service
{
    public static class RelatorioService
    {
        private static string CaminhoRelatorio = "Data/relatorio.txt";

        public static void AtualizarRelatorio(Biblioteca biblioteca)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== RELATÓRIO LUDOTECA.NET ===");
            sb.AppendLine($"Geração: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine("------------------------------");
            sb.AppendLine($"Total de Jogos: {biblioteca.Jogos.Count}");
            sb.AppendLine($"Jogos Disponíveis: {biblioteca.Jogos.Values.Count(j => j.Disponivel)}");
            sb.AppendLine($"Jogos Alugados: {biblioteca.Jogos.Values.Count(j => !j.Disponivel)}");
            sb.AppendLine($"Total de Membros: {biblioteca.Membros.Count}");
            sb.AppendLine($"Membros com Jogos: {biblioteca.Membros.Values.Count(m => m.JogoAlugado != "Nenhum")}");
            sb.AppendLine("------------------------------");
            sb.AppendLine("JOGOS ALUGADOS");

            var alugados = biblioteca.Membros.Values.Where(m => m.JogoAlugado != "Nenhum");
            foreach (var m in alugados)
            {
                sb.AppendLine($"Membro: {m.Nome} | ID: {m.Id}");
                sb.AppendLine($"Jogo: {m.JogoAlugado}");
                sb.AppendLine($"Data Aluguel: {m.DataAluguel:dd/MM/yyyy}");
                sb.AppendLine($"Data Devolução: {m.DataDevolucao:dd/MM/yyyy}");
                sb.AppendLine("------------------------------");
            }

            System.IO.Directory.CreateDirectory("Data");
            System.IO.File.WriteAllText(CaminhoRelatorio, sb.ToString());
        }

        public static void MostrarRelatorio()
        {
            if (!System.IO.File.Exists(CaminhoRelatorio))
            {
                Console.WriteLine("Relatório ainda não foi gerado.");
                Console.Write("Aperte ENTER...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine(System.IO.File.ReadAllText(CaminhoRelatorio));
            Console.Write("Aperte ENTER...");
            Console.ReadLine();
        }
    }
}
