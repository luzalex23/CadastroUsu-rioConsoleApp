# **📋 Cadastro de Usuários - Console App (.NET)**

## **Descrição do Projeto**
Esta é uma aplicação simples de console desenvolvida em **C#** e **.NET** que permite o gerenciamento de usuários. A aplicação utiliza os conceitos de **Clean Architecture** e boas práticas de **Clean Code**, além de incluir persistência de dados com **SQLite**.

---

## **🚀 Funcionalidades**

- **Cadastrar Usuários**: Registre usuários informando nome, e-mail e idade, com validações automáticas.
- **Listar Usuários**: Exibe todos os usuários cadastrados no sistema.
- **Buscar Usuários**: Encontre usuários por nome, com suporte a buscas parciais e insensíveis a maiúsculas/minúsculas.
- **Atualizar Usuários**: Modifique informações como nome, e-mail ou idade de um usuário cadastrado.
- **Persistência de Dados**: Garantida com **SQLite**, para manter os dados salvos entre execuções.

---

## **🛠️ Estrutura do Projeto**

A aplicação segue o conceito de **Clean Architecture**, garantindo separação de responsabilidades e organização:

- **Core**: Contém as entidades, interfaces (repositórios) e casos de uso.
- **Infrastructure**: Implementações de persistência de dados (**repositório SQLite**).
- **Presentation**: Interface via console para interação com o usuário.
- **Tests**: Testes unitários que validam as funcionalidades.

---

## **💾 Configuração do Banco de Dados**

- O banco de dados utilizado é o **SQLite**.
- O arquivo do banco (`users.db`) será criado automaticamente na primeira execução.
- As tabelas são inicializadas automaticamente, garantindo a experiência plug-and-play.

---

## **📦 Como Rodar a Aplicação**

1. Certifique-se de ter o **.NET SDK** instalado na máquina.
2. Clone ou baixe este repositório:
   ```bash
   git clone https://github.com/luzalex23/CadastroUsuarioConsoleApp.git
3. Navegue até a pasta raiz do projeto:
   ```bash
       cd seu-repositorio
4. Execute o comando:
   ```bash
       dotnet run
## **🔗 Links Úteis**

- [Documentação do .NET](https://learn.microsoft.com/pt-br/dotnet/)
- [SQLite](https://www.sqlite.org/index.html)
- [Exemplo de Clean Architecture](https://github.com/ardalis/CleanArchitecture)

