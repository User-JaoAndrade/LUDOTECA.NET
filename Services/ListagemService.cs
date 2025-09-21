using LUDOTECA.Models;

namespace LUDOTECA.Service
{
    public static class ListagemService
    {
        public static void ListarJogos(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE JOGOS ===\n");

            if (!biblioteca.Jogos.Any())
            {
                Console.WriteLine("Nenhum jogo cadastrado.");
            }
            else
            {
                foreach (var jogo in biblioteca.Jogos.Values)
                {
                    string disponibilidade = jogo.Disponivel ? "DISPONÍVEL" : "INDISPONÍVEL";

                    Console.WriteLine($@"
ID:             {jogo.Id}
Nome:           {jogo.Nome}
Categoria:      {jogo.Categoria}
Ano:            {jogo.AnoDeLancamento}
Disponibilidade: {disponibilidade}
----------------------------------------------------------------
");
                }
            }

            Console.Write("Aperte ENTER para continuar...");
            Console.ReadLine();
        }

        public static void ListarMembros(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE MEMBROS ===\n");

            if (!biblioteca.Membros.Any())
            {
                Console.WriteLine("Nenhum membro cadastrado.");
            }
            else
            {
                foreach (var membro in biblioteca.Membros.Values)
                {
                    string jogoAlugado = membro.JogoAlugado != "Nenhum" ? membro.JogoAlugado : "Nenhum";
                    string dataAluguel = membro.DataAluguel != default ? membro.DataAluguel.ToString("dd/MM/yyyy") : "-";
                    string dataDevolucao = membro.DataDevolucao != default ? membro.DataDevolucao.ToString("dd/MM/yyyy") : "-";

                    Console.WriteLine($@"
ID:           {membro.Id}
Nome:         {membro.Nome}
Jogo Alugado: {jogoAlugado}
Data Aluguel: {dataAluguel}
Data Devolução: {dataDevolucao}
----------------------------------------------------------------
");
                }
            }

            Console.Write("Aperte ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
