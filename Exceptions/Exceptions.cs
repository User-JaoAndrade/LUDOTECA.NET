using System;

namespace LUDOTECA.Exceptions
{
    public class LudotecaException : Exception
    {
        public LudotecaException(string message) : base(message) { }
    }

    public class MembroNaoEncontradoException : LudotecaException
    {
        public MembroNaoEncontradoException(int id) 
            : base($"Membro com ID {id} não encontrado.") { }
    }

    public class JogoNaoEncontradoException : LudotecaException
    {
        public JogoNaoEncontradoException(int id) 
            : base($"Jogo com ID {id} não encontrado.") { }
    }

    public class JogoIndisponivelException : LudotecaException
    {
        public JogoIndisponivelException(string nomeJogo) 
            : base($"O jogo '{nomeJogo}' já está alugado.") { }
    }

    public class MembroSemJogoException : LudotecaException
    {
        public MembroSemJogoException(string nomeMembro) 
            : base($"{nomeMembro} não possui jogo alugado.") { }
    }

    public class MembroComJogoException : LudotecaException
    {
        public MembroComJogoException(string nomeMembro, string nomeJogo) 
            : base($"{nomeMembro} já alugou: {nomeJogo}.") { }
    }

    public class AnoInvalidoException : LudotecaException
    {
        public AnoInvalidoException() : base("Ano de lançamento inválido.") { }
    }
}
