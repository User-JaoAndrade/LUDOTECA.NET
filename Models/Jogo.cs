using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    public class Jogo
    {
        private static Random rnd = new Random();

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public int AnoDeLancamento { get; private set; }
        public bool Disponivel { get; private set; } = true;

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

        // Construtor JSON
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
