using LUDOTECA.Models;
using LUDOTECA.Utils;
using System.Collections.Generic;
using System.Linq;

namespace LUDOTECA.Services
{
    public static class MembroService
    {
        public static Dictionary<int, Membro> Lista_de_membros = new Dictionary<int, Membro>();
        private static string CaminhoJson = "Data/Membros.json";

        public static void SalvarMembros()
        {
            JsonHelper.SalvarLista(Lista_de_membros.Values.ToList(), CaminhoJson);
        }

        public static void CadastrarMembro()
        {
            while (true)
            {
                try
                {
                    var membros_existentes = JsonHelper.CarregarLista<Membro>(CaminhoJson);
                    Lista_de_membros = membros_existentes.ToDictionary(m => m._Id, m => m);

                    Console.WriteLine("\n\n\n\n==== CADASTRANDO MEMBRO ====\n");
                    Console.Write("Nome do Membro: ");
                    string nome_do_membro = Helpers.LerEntradaDeDados();

                    Membro novo_membro = new Membro(nome_do_membro, Lista_de_membros.Values.ToList());
                    Lista_de_membros.Add(novo_membro._Id, novo_membro);

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
