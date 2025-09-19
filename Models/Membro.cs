using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    /// <summary>
    /// Representa um membro da Ludoteca.
    /// Contém informações como ID, nome, jogo alugado e datas de aluguel e devolução.
    /// </summary>
    public class Membro
    {
        private static Random rnd = new Random();
        public int _Id { get; private set; } // Identificador ÚNICO do membro (usado como chave no dict)
        public string? Nome { get; private set; } // Nome do membro
        public string? Jogo_alugado { get; private set; } = "Nenhum"; // Nome do jogo que o usuário alugou
        public DateTime Data_do_aluguel { get; private set; } = default; // Dia que o jogo foi alugado
        public DateTime Data_de_devolucao { get; private set; } = default; // Dia que o jogo precisa ser devolvido

        public Membro(string nome, List<Membro> Lista_de_membros)
        {
            Nome = nome;

            int novo_id;
            do
            {
                novo_id = rnd.Next(100000, 1000000);
            } while (Lista_de_membros.Exists(m => m._Id == novo_id)); // Repete a operação caso o ID já exista

            _Id = novo_id;
        }

        /// <summary>
        /// Construtor usado pelo JSON para desserialização.
        /// </summary>
        [JsonConstructor]
        public Membro(int _Id, string? Nome, string? Jogo_alugado, DateTime Data_do_aluguel, DateTime Data_de_devolucao)
        {
            this._Id = _Id;
            this.Nome = Nome;
            this.Jogo_alugado = Jogo_alugado;
            this.Data_do_aluguel = Data_do_aluguel;
            this.Data_de_devolucao = Data_de_devolucao;
        }

        public void AlterarDataDoAluguel(DateTime nova_data)
        {
            Data_do_aluguel = nova_data;
        }

        public void AlterarDataDaDevolucao(DateTime nova_data)
        {
            Data_de_devolucao = nova_data;
        }

        public void AlterarNomeDoJogoAlugado(string? nome_jogo)
        {
            Jogo_alugado = nome_jogo;
        }

        /// <summary>
        /// Mostra informações detalhadas do membro recém-cadastrado
        /// </summary>
        public void MostrarNovoMembroCadastrado()
        {
            Console.Write("\n\n>>> MEMBRO CADASTRADO COM SUCESSO <<<\n\n" +
                             $"          ID: {_Id}\n" +
                             $"        Nome: {Nome}\n" +
                             $"Jogo Alugado: {Jogo_alugado}\n\n" +
                              "Aperte ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
