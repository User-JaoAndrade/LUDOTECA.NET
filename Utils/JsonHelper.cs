using System.Text.Json;

namespace LUDOTECA.Utils
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions opcoes = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public static void SalvarLista<T>(List<T> lista, string caminho)
        {
            string dir = Path.GetDirectoryName(caminho) ?? "Data";
            Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(lista, opcoes); // [AV1-3]
            File.WriteAllText(caminho, json); // [AV1-3]
        }

        public static List<T> CarregarLista<T>(string caminho)
        {
            if (!File.Exists(caminho))
                return new List<T>();

            string json = File.ReadAllText(caminho); // [AV1-3]
            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            try
            {
                return JsonSerializer.Deserialize<List<T>>(json, opcoes) ?? new List<T>(); // [AV1-3]
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
