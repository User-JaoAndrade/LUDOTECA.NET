using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    public class Membro
    {
        private static Random rnd = new Random();

        public int Id { get; private set; } // [AV1-2]
        public string Nome { get; private set; }// [AV1-2]
        public string JogoAlugado { get; private set; } = "Nenhum"; // [AV1-2]
        public DateTime DataAluguel { get; private set; } = default; // [AV1-2]
        public DateTime DataDevolucao { get; private set; } = default; // [AV1-2]

        public Membro(string nome, List<Membro> listaExistente)
        {
            Nome = nome;

            int novoId;
            do
            {
                novoId = rnd.Next(100000, 1000000);
            } while (listaExistente.Exists(m => m.Id == novoId));

            Id = novoId;
        }

        [JsonConstructor]
        public Membro(int id, string nome, string jogoAlugado, DateTime dataAluguel, DateTime dataDevolucao)
        {
            Id = id;
            Nome = nome;
            JogoAlugado = jogoAlugado ?? "Nenhum";
            DataAluguel = dataAluguel;
            DataDevolucao = dataDevolucao;
        }

        public void AlterarJogoAlugado(string jogo) => JogoAlugado = jogo ?? "Nenhum";
        public void AlterarDataAluguel(DateTime data) => DataAluguel = data;
        public void AlterarDataDevolucao(DateTime data) => DataDevolucao = data;

        public void MostrarInfo()
        {
            Console.Clear();
            Console.WriteLine($@"
>>> MEMBRO CADASTRADO <<<

  ID: {Id}
Nome: {Nome}
");
            Console.Write("Aperte ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
