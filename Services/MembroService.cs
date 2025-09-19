using LUDOTECA.Models;
using LUDOTECA.Utils;

namespace LUDOTECA.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar operações relacionadas aos membros da ludoteca,
    /// incluindo cadastro e persistência em arquivo JSON.
    /// </summary>
    public static class MembroService
    {
        public static Dictionary<int, Membro> Lista_de_membros = new Dictionary<int, Membro>();

        private static string CaminhoJson = "Data/Membros.json";

        /// <summary>
        /// Salva a lista de membros atual no arquivo JSON definido em <see cref="CaminhoJson"/>.
        /// </summary>
        public static void SalvarMembros()
        {
            JsonHelper.SalvarLista(Lista_de_membros.Values.ToList(), CaminhoJson);
        }

        /// <summary>
        /// Realiza o processo de cadastro de um novo membro,
        /// solicitando os dados ao usuário pelo console.
        /// Após o cadastro, os dados são salvos em JSON.
        /// </summary>
        public static void CadastrarMembro()
        {
            while (true)
            {
                try
                {
                    // Carrega os membros já existentes do JSON para evitar sobrescrita
                    var membros_existentes = JsonHelper.CarregarLista<Membro>(CaminhoJson);
                    Lista_de_membros = membros_existentes.ToDictionary(m => m._Id, m => m);

                    Console.WriteLine("\n\n\n\n==== CADASTRANDO MEMBRO ====\n");
                    Console.Write("Nome do Membro: ");
                    string nome_do_membro = Helpers.LerEntradaDeDados();

                    // Cria o novo membro garantindo ID único
                    Membro novo_membro = new Membro(nome_do_membro, Lista_de_membros.Values.ToList());
                    Lista_de_membros.Add(novo_membro._Id, novo_membro);

                    // Salva no JSON e mostra mensagem de confirmação
                    SalvarMembros();
                    novo_membro.MostrarNovoMembroCadastrado();
                    break;
                }
                catch (Exception ex)
                {
                    Helpers.MensagemDeExcessao(ex.Message);

                    if (!Helpers.VerificarSeUsuarioDesejaContinuar())
                        break;
                }
            }
        }
    }
}
