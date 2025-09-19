using System.Text.Json;

namespace LUDOTECA.Utils
{
    public static class JsonHelper
    {
        public static void SalvarLista<T>(List<T> lista, string caminhoArquivo)
        {
            var opcoes = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(lista, opcoes);
            File.WriteAllText(caminhoArquivo, json);
        }

        public static List<T> CarregarLista<T>(string caminhoArquivo)
        {
            // Se o arquivo não existir, retorna lista vazia
            if (!File.Exists(caminhoArquivo))
                return new List<T>();

            string json = File.ReadAllText(caminhoArquivo);

            // Se o arquivo estiver vazio ou contiver apenas espaços, retorna lista vazia
            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            try
            {
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (JsonException)
            {
                // Caso o JSON esteja corrompido, evita a exceção e retorna lista vazia
                return new List<T>();
            }
        }
    }
}
