using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    public class Jogo
    {
        private static Random rnd = new Random();

        public int _Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Categoria { get; private set; }
        public int AnoDeLancamento { get; private set; }
        public bool Disponivel { get; private set; } = true;

        // Construtor padrão para criar novo jogo
        public Jogo(string nome, string categoria, int ano, List<Jogo> Lista_de_Jogos)
        {
            Nome = nome;
            Categoria = categoria;
            AnoDeLancamento = ano;

            int novo_id;
            do
            {
                novo_id = rnd.Next(100000, 1000000);
            } while (Lista_de_Jogos.Exists(j => j._Id == novo_id));

            _Id = novo_id;
        }

        // Construtor para o JSON
        [JsonConstructor]
        public Jogo(int _Id, string? Nome, string? Categoria, int AnoDeLancamento, bool Disponivel)
        {
            this._Id = _Id;
            this.Nome = Nome;
            this.Categoria = Categoria;
            this.AnoDeLancamento = AnoDeLancamento;
            this.Disponivel = Disponivel;
        }

        public void MudarJogoParaDisponivel()
        {
            Disponivel = true;
        }

        public void MudarJogoParaIndisponivel()
        {
            Disponivel = false;
        }

        public void MostrarNovoJogoCadastrado()
        {
            Console.Write("\n\n>>> JOGO CADASTRADO COM SUCESSO <<<\n\n" +
                          $"               ID: {_Id}\n" +
                          $"             Nome: {Nome}\n" +
                          $"        Categoria: {Categoria}\n" +
                          $"Ano de lançamento: {AnoDeLancamento}\n\n" +
                          "Aperte ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
