namespace LUDOTECA.Utils
{
    public static class Logger
    {
        private static readonly string CaminhoLog = "Data/debug.log";

        public static void Log(string mensagem)
        {
            try
            {
                Directory.CreateDirectory("Data");
                using (var writer = new StreamWriter(CaminhoLog, true))
                {
                    writer.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {mensagem}");
                }
            }
            catch
            {
                // Não propaga erro de log (pra não travar o sistema caso o log falhe)
            }
        }

        public static void LogErro(Exception ex)
        {
            Log($"[ERRO] {ex.GetType().Name}: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
    }
}
