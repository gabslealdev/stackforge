# StackForge

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-20-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-5.8-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-Tests-5C2D91?style=for-the-badge)

StackForge é uma plataforma para conectar `Learners` e `Mentors` em uma jornada de mentoria estruturada. A proposta não é apenas encontrar mentores, mas organizar o processo completo: descoberta, pedido de mentoria, diagnóstico inicial, plano de aprendizado, execução de tarefas, feedback e reputação.

## Domínio

O fluxo principal do produto é:

1. `Mentor` cria conta, completa perfil, cadastra `Stacks` e fica disponível.
2. `Learner` cria conta e acessa a descoberta de mentores.
3. `Learner` escolhe uma `Stack`, informa seu objetivo e envia um `Mentorship Request`.
4. `Mentor` aceita o pedido e uma `Mentorship` ativa é criada.
5. `Mentor` pode criar um `Initial Assessment` para nivelamento.
6. `Mentor` cria um `Learning Plan` com `Tasks`.
7. `Learner` executa tarefas e recebe `Feedback`.
8. Ao final, o `Learner` avalia o `Mentor` com `Rating`.

## Tecnologias

### Backend

- .NET 9
- C#
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL com Npgsql
- JWT Bearer Authentication
- FluentValidation
- Argon2 para hashing de senha
- Swagger / OpenAPI
- xUnit, Shouldly, Bogus e Coverlet para testes

### Frontend

- Angular 20
- TypeScript
- RxJS
- SCSS
- Lucide Angular
- Jasmine/Karma para testes

### Infraestrutura

- Docker Compose
- PostgreSQL 16

## Estrutura do Projeto

```txt
.
├── docs
│   ├── 01-Vision.md
│   ├── 02-Glossary.md
│   ├── 03-Requirements.md
│   ├── 04-Model_Domain.md
│   └── 05-Business_Rules.md
├── src
│   ├── api
│   │   ├── src
│   │   │   ├── StackForge.Api
│   │   │   ├── StackForge.Application
│   │   │   ├── StackForge.Domain
│   │   │   └── StackForge.Infrastructure
│   │   └── tests
│   │       ├── StackForge.Application.Tests
│   │       └── StackForge.Domain.Tests
│   └── web
│       └── stackforge-web
└── README.md
```

## Backend

A API segue uma organização em camadas:

- `StackForge.Api`: controllers, autenticação, autorização e configuração HTTP.
- `StackForge.Application`: casos de uso, contratos de comandos/queries, validações e resultados.
- `StackForge.Domain`: entidades, value objects, enums, erros e regras centrais de domínio.
- `StackForge.Infrastructure`: EF Core, repositórios, mapeamentos, autenticação JWT, hashing de senha e seed de dados.

Funcionalidades já implementadas:

- Cadastro de usuário com tipo de perfil.
- Login com JWT.
- Registro de perfil `Mentor` e `Learner`.
- Autorização por policy: `MentorOnly` e `LearnerOnly`.
- Cadastro de `Stacks` no perfil do `Mentor`.
- Atualização de disponibilidade do `Mentor`.
- Busca de `Stacks`.
- Busca de `Mentors` por `Stacks`.

## Como Rodar

### Pré-requisitos

- .NET SDK 9
- Node.js compatível com Angular 20
- npm
- Docker e Docker Compose

### Banco de Dados

```bash
cd src/api
docker compose up -d
```

O PostgreSQL sobe com:

```txt
Host: localhost
Port: 5432
Database: stackforge
User: postgres
Password: postgres
```

### API

```bash
cd src/api
dotnet restore
dotnet run --project src/StackForge.Api/StackForge.Api.csproj
```

Em ambiente de desenvolvimento, o Swagger fica disponível na URL configurada pelo ASP.NET Core.

### Frontend

```bash
cd src/web/stackforge-web
npm install
npm start
```

Por padrão, o Angular roda em:

```txt
http://localhost:4200
```

## Testes

### Backend

```bash
cd src/api
dotnet test
```

### Frontend

```bash
cd src/web/stackforge-web
npm test
```

## Documentação

A documentação do produto está em `docs/`:

- `01-Vision.md`: visão do produto.
- `02-Glossary.md`: termos do domínio.
- `03-Requirements.md`: requisitos funcionais e não funcionais.
- `04-Model_Domain.md`: modelo de domínio.
- `05-Business_Rules.md`: regras de negócio.
- `06-Backlog.md`: backlog do produto.
- `07-Traceabillity.md`: rastreabilidade.

## Status

O projeto está em evolução. O backend já cobre identidade, perfis, stacks, disponibilidade e descoberta inicial. As próximas fases do domínio incluem `Mentorship Request`, `Mentorship`, `Initial Assessment`, `Learning Plan`, `Task`, `Feedback` e `Rating`.
