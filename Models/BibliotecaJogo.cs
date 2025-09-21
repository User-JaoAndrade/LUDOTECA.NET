using LUDOTECA.Utils;

namespace LUDOTECA.Models
{
    public class Biblioteca
    {
        public Dictionary<int, Jogo> Jogos { get; private set; } = new Dictionary<int, Jogo>(); // [AV1-2]
        public Dictionary<int, Membro> Membros { get; private set; } = new Dictionary<int, Membro>(); // [AV1-2]

        private static string CaminhoJogos = "Data/Biblioteca.json";
        private static string CaminhoMembros = "Data/Membros.json";

        public void AdicionarJogo(Jogo jogo) => Jogos[jogo.Id] = jogo;
        public void AdicionarMembro(Membro membro) => Membros[membro.Id] = membro;
        public void RemoverJogo(int id) => Jogos.Remove(id);
        public void RemoverMembro(int id) => Membros.Remove(id);

        public static Biblioteca CarregarBiblioteca()
        {
            var biblioteca = new Biblioteca
            {
                Jogos = JsonHelper.CarregarLista<Jogo>(CaminhoJogos).ToDictionary(j => j.Id, j => j),
                Membros = JsonHelper.CarregarLista<Membro>(CaminhoMembros).ToDictionary(m => m.Id, m => m)
            };
            return biblioteca;
        }

        public static void SalvarBiblioteca(Biblioteca biblioteca)
        {
            JsonHelper.SalvarLista(biblioteca.Jogos.Values.ToList(), CaminhoJogos);
            JsonHelper.SalvarLista(biblioteca.Membros.Values.ToList(), CaminhoMembros);
        }
    }
}