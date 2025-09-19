using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    /// <summary>
    /// Representa um jogo da Ludoteca.
    /// Contém informações como ID, nome, categoria, ano de lançamento e disponibilidade.
    /// </summary>
    public class Jogo
    {
        private static Random rnd = new Random();
        public int _Id { get; private set; } // Identificador ÚNICO do jogo (usado como chave no dict)
        public string? Nome { get; private set; } // Nome do jogo
        public string? Categoria { get; private set; } // Categoria que o jogo se encontra
        public int AnoDeLancamento { get; private set; } // Ano que o jogo foi lançado
        public bool Disponivel { get; private set; } = true; // Disponibilidade do jogo

        public Jogo(string nome, string categoria, int ano, List<Jogo> Lista_de_Jogos)
        {
            Nome = nome;
            Categoria = categoria;
            AnoDeLancamento = ano;

            int novo_id;
            do
            {
                novo_id = rnd.Next(100000, 1000000);
            } while (Lista_de_Jogos.Exists(j => j._Id == novo_id)); // Repete a operação caso a ID já exista

            _Id = novo_id;
        }

        /// <summary>
        /// Construtor usado pelo JSON para desserialização.
        /// </summary>
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

        /// <summary>
        /// Mostra informações detalhadas do jogo recém-cadastrado
        /// </summary>
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
