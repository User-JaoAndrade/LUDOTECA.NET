using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    public class Jogo
    {
        private static Random rnd = new Random();

        public int Id { get; private set; } // [AV1-2]
        public string Nome { get; private set; } // [AV1-2]
        public string Categoria { get; private set; } // [AV1-2]
        public int AnoDeLancamento { get; private set; } // [AV1-2]
        public bool Disponivel { get; private set; } = true; // [AV1-2]

        // Construtor para novo jogo
        public Jogo(string nome, string categoria, int ano, List<Jogo> listaExistente)
        {
            Nome = nome;
            Categoria = categoria;
            AnoDeLancamento = ano;

            int novoId;
            do
            {
                novoId = rnd.Next(100000, 1000000);
            } while (listaExistente.Exists(j => j.Id == novoId));

            Id = novoId;
        }

        // Construtor que usa o arquivo JSON
        [JsonConstructor]
        public Jogo(int id, string nome, string categoria, int anoDeLancamento, bool disponivel)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            AnoDeLancamento = anoDeLancamento;
            Disponivel = disponivel;
        }

        public void TornarDisponivel() => Disponivel = true;
        public void TornarIndisponivel() => Disponivel = false;

        public void MostrarInfo()
        {
            Console.Clear();
            Console.WriteLine($@"
>>> JOGO CADASTRADO <<<

       ID: {Id}
     Nome: {Nome}
Categoria: {Categoria}
      Ano: {AnoDeLancamento}
   Status: {(Disponivel ? "DISPONÍVEL" : "INDISPONÍVEL")}
");
            Console.Write("Aperte ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
