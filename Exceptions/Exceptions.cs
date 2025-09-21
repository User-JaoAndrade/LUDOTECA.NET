namespace LUDOTECA.Exceptions
{
    public class LudotecaException : Exception // [AV1-5]
    {
        public LudotecaException(string message) : base(message) { }
    }

    public class MembroNaoEncontradoException : LudotecaException // [AV1-5]
    {
        public MembroNaoEncontradoException(int id) 
            : base($"Membro com ID {id} não encontrado.") { }
    }

    public class JogoNaoEncontradoException : LudotecaException // [AV1-5]
    {
        public JogoNaoEncontradoException(int id) 
            : base($"Jogo com ID {id} não encontrado.") { }
    }

    public class JogoIndisponivelException : LudotecaException // [AV1-5]
    {
        public JogoIndisponivelException(string nomeJogo) 
            : base($"O jogo '{nomeJogo}' já está alugado.") { }
    }

    public class MembroSemJogoException : LudotecaException // [AV1-5]
    {
        public MembroSemJogoException(string nomeMembro) 
            : base($"{nomeMembro} não possui jogo alugado.") { }
    }

    public class MembroComJogoException : LudotecaException // [AV1-5]
    {
        public MembroComJogoException(string nomeMembro, string nomeJogo) 
            : base($"{nomeMembro} já alugou: {nomeJogo}.") { }
    }

    public class AnoInvalidoException : LudotecaException // [AV1-5]
    {
        public AnoInvalidoException() : base("Ano de lançamento inválido.") { }
    }
}
