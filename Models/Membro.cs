using System.Text.Json.Serialization;

namespace LUDOTECA.Models
{
    public class Membro
    {
        private static Random rnd = new Random();

        public int _Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Jogo_alugado { get; private set; } = "Nenhum";
        public DateTime Data_do_aluguel { get; private set; } = default;
        public DateTime Data_de_devolucao { get; private set; } = default;

        // Construtor padrão para cadastro de novo membro
        public Membro(string nome, List<Membro> Lista_de_membros)
        {
            Nome = nome;

            int novo_id;
            do
            {
                novo_id = rnd.Next(100000, 1000000);
            } while (Lista_de_membros.Exists(m => m._Id == novo_id));

            _Id = novo_id;
        }

        // Construtor para o JSON
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
