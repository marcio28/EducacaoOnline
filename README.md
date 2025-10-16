# **Plataforma de Educação Online**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **Plataforma de Educação Online**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Arquitetura, Modelagem e Qualidade de Software**.
O objetivo é desenvolver uma plataforma educacional com múltiplos contextos delimitados, aplicando DDD, TDD, CQRS e padrões arquiteturais para gestão eficiente de conteúdos educacionais, alunos e processos financeiros.

### **Autor(es)**
- **Márcio Gomes**

## **2. Proposta do Projeto**

O projeto consiste em:

- **API RESTful:** Exposição dos recursos da plataforma para servir front-ends e/ou outras aplicações
- **Autenticação e Autorização:** Implementação de controle de acesso
- **Aplicação:** Implementação dos serviços da plataforma
- **Domínio**: Definição dos modelos de domínio e das regras de negócio
- **Acesso a Dados:** Implementação da persistência de dados através de ORM

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C# .NET 8
- **Frameworks:**
  - ASP.NET Core Web API
  - Entity Framework Core
- **Bancos de Dados:**
  - SQL Server
  - SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- src/
  - EducacaoOnline.Api/ - API RESTful
  - EducacaoOnline.Core/ - Modelos básicos de dados, objetos de domínio e mensagens compartilhados com as diversas camadas da solução
  - EducacaoOnline.GestaoDeAlunos.Application - Serviços de aplicação de gestão de matrículas e certificados
  - EducacaoOnline.GestaoDeAlunos.Data - Persistência de dados de matrículas e certificados
  - EducacaoOnline.GestaoDeAlunos.Domain - Modelos de domínio e regras de negócio da gestão de matrículas e certificados
  - EducacaoOnline.GestaoDeConteudo.Application - Serviços de aplicação de gestão de aulas e conteúdo programático
  - EducacaoOnline.GestaoDeConteudo.Data - Persistência de dados de aulas e conteúdo programático
  - EducacaoOnline.GestaoDeConteudo.Domain - Modelos de domínio e regras de negócio da gestão de aulas e conteúdo programático
  - EducacaoOnline.PagamentoEFaturamento.Application - Serviços de aplicação de pagamento das matrículas e controle do faturamento
  - EducacaoOnline.PagamentoEFaturamento.Data - Persistência de dados de pagamento e faturamento
  - EducacaoOnline.PagamentoEFaturamento.Domain - Modelos de domínio e regras de negócio de pagamento de matrículas e controle do faturamento
- tests/ - Testes de cada projeto referido acima
- README.md - Arquivo de documentação do projeto
- FEEDBACK.md - Arquivo para consolidação dos feedbacks
- .gitignore - Arquivo de ignoração do Git

## **5. Funcionalidades Implementadas**

- **API RESTful:** Autenticação de usuários, Cadastro de Curso, Cadastro de Aula, Matrícula do Aluno, Realização do Pagamento, Realização da Aula e Finalização do Curso
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server (ambiente de produção) / SQLite (ambiente de desenvolvimento)
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/marcio28/EducacaoOnline.git`
   - `cd marcio28/EducacaoOnline`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a API:**
   - `cd src/EducacaoOnline.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5001/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido à configuração do Seed de dados

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5001/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele
