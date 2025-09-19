# LUDOTECA.NET

LUDOTECA.NET é um sistema em **C#** para gerenciar jogos, permitindo cadastrar membros, cadastrar jogos, emprestar e devolver jogos, com persistência em arquivos JSON.

---

## Funcionalidades

- Cadastro de jogos e membros.
- Listagem de jogos com status de disponibilidade.
- Empréstimo e devolução de jogos.
- Cálculo automático de multa por atraso (R$2/dia).
- Persistência de dados em JSON (`Biblioteca.json` e `Membros.json`).

---

## Estrutura

- **Models:** Classes `Jogo` e `Membro`.
- **Services / Service:** Funções de cadastro, listagem, empréstimo e devolução.
- **Utils:** Helpers e manipulação de JSON.
- **Program.cs:** Menu principal em console.

---

## Pré-requisitos

- **.NET SDK 7.0 ou superior** ([Download](https://dotnet.microsoft.com/download/dotnet))  
- Git (opcional, para clonar o repositório)

### Instalação e execução

```bash
# Clonar o repositório
git clone https://github.com/User-JaoAndrade/LUDOTECA.NET.git
cd LUDOTECA.NET

# Compilar
dotnet build

# Executar
dotnet run
