# Model Domain

## Visão Geral

O domínio do StackForge é organizado em torno de uma jornada de mentoria estruturada:

1. `User` escolhe um tipo de perfil.
2. `Mentor` ou `Learner` completa seu perfil.
3. `Learner` busca `Mentors` por `Stack`.
4. `Learner` envia um `Mentorship Request`.
5. `Mentor` aceita o pedido e uma `Mentorship` é criada.
6. `Mentor` conduz diagnóstico, planejamento, execução, feedback e encerramento.

## Contextos

| Contexto | Responsabilidade |
| --- | --- |
| `Identity Context` | Cadastro, autenticação, tipo de perfil e autorização. |
| `Profile Context` | Dados de `Mentor`, `Learner`, disponibilidade e stacks de especialidade. |
| `Mentorship Context` | Descoberta, pedidos de mentoria, mentorias ativas, assessments, planos, tasks, feedback e rating. |
| `Stacks Context` | Catálogo de stacks disponíveis para busca e associação com mentors. |

## Entidades

### User

Representa a conta usada para autenticação.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do usuário. |
| `Email` | Email usado para login. |
| `PasswordHash` | Hash da senha do usuário. |

Relações:

- Um `User` possui um `User Registration`.
- Um `User` deve completar exatamente um perfil: `Mentor` ou `Learner`.

### User Registration

Representa o processo de seleção e conclusão do tipo de perfil do usuário.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do registro. |
| `UserId` | Usuário relacionado. |
| `SelectedProfileType` | Tipo de perfil escolhido: `Mentor` ou `Learner`. |
| `Status` | Estado do cadastro do perfil. |

Estados:

| Status | Significado |
| --- | --- |
| `InProgress` | Usuário criado, mas perfil ainda não concluído. |
| `Completed` | Perfil escolhido foi criado com sucesso. |

### Mentor

Representa o perfil responsável por conduzir mentorias.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do perfil de mentor. |
| `UserId` | Usuário dono do perfil. |
| `Name` | Nome do mentor. |
| `BirthDate` | Data de nascimento. |
| `Education` | Formação do mentor. |
| `Bio` | Descrição opcional do mentor. |
| `Availability` | Disponibilidade para mentorias. |
| `Stacks` | Stacks de especialidade do mentor. |
| `CreatedAt` | Data de criação do perfil. |

Estados de disponibilidade:

| Status | Significado |
| --- | --- |
| `Available` | Mentor disponível para receber pedidos de mentoria. |
| `Unavailable` | Mentor indisponível para receber pedidos de mentoria. |

Relações:

- Um `Mentor` pertence a um `User`.
- Um `Mentor` possui uma ou mais `Stacks` para poder ficar `Available`.
- Um `Mentor` pode receber vários `Mentorship Requests`.
- Um `Mentor` pode conduzir várias `Mentorships`.

### Learner

Representa o perfil que busca mentoria.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do perfil de learner. |
| `UserId` | Usuário dono do perfil. |
| `Name` | Nome do learner. |
| `BirthDate` | Data de nascimento. |
| `CreatedAt` | Data de criação do perfil. |

Relações:

- Um `Learner` pertence a um `User`.
- Um `Learner` pode criar vários `Mentorship Requests`.
- Um `Learner` pode participar de várias `Mentorships`.

### Stack

Representa uma tecnologia, linguagem, framework, ferramenta ou área técnica.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único da stack. |
| `Name` | Nome exibido da stack. |
| `Key` | Chave única usada para identificação e busca. |

Relações:

- Uma `Stack` pode estar associada a vários `Mentors`.
- Um `Mentor` pode possuir várias `Stacks`.
- Um `Mentorship Request` deve indicar a `Stack` desejada pelo `Learner`.

### Mentorship Request

Representa o pedido de mentoria enviado por um `Learner` para um `Mentor`.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do pedido. |
| `LearnerId` | Learner que solicitou a mentoria. |
| `MentorId` | Mentor escolhido pelo learner. |
| `StackId` | Stack principal desejada pelo learner. |
| `Goal` | Objetivo final informado pelo learner. |
| `Status` | Estado atual do pedido. |
| `CreatedAt` | Data de criação do pedido. |
| `DecidedAt` | Data da decisão do mentor, quando existir. |

Estados:

| Status | Significado |
| --- | --- |
| `Pending` | Pedido enviado e aguardando decisão do mentor. |
| `Accepted` | Pedido aceito pelo mentor. |
| `Rejected` | Pedido recusado pelo mentor. |
| `Cancelled` | Pedido cancelado antes da decisão. |

Relações:

- Um `Mentorship Request` pertence a um `Learner`.
- Um `Mentorship Request` é direcionado a um `Mentor`.
- Um `Mentorship Request` referencia uma `Stack`.
- Um `Mentorship Request` aceito cria uma `Mentorship`.

### Mentorship

Representa a relação ativa de mentoria entre um `Mentor` e um `Learner`.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único da mentorship. |
| `MentorshipRequestId` | Pedido que originou a mentorship. |
| `MentorId` | Mentor responsável. |
| `LearnerId` | Learner mentorado. |
| `StackId` | Stack principal da mentorship. |
| `Goal` | Objetivo final acordado. |
| `Status` | Estado da mentorship. |
| `StartedAt` | Data de início. |
| `CompletedAt` | Data de conclusão, quando existir. |

Estados:

| Status | Significado |
| --- | --- |
| `Active` | Mentorship aceita e em andamento. |
| `Completed` | Mentorship finalizada com sucesso. |
| `Cancelled` | Mentorship encerrada antes da conclusão. |

Relações:

- Uma `Mentorship` possui um `Mentor`.
- Uma `Mentorship` possui um `Learner`.
- Uma `Mentorship` pode possuir um `Initial Assessment`.
- Uma `Mentorship` pode possuir um `Learning Plan`.
- Uma `Mentorship` pode possuir um `Rating` ao final.

### Initial Assessment

Representa a avaliação inicial de nivelamento enviada ao `Learner`.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do assessment. |
| `MentorshipId` | Mentorship relacionada. |
| `Questions` | Perguntas criadas pelo mentor. |
| `Answers` | Respostas enviadas pelo learner. |
| `Status` | Estado do assessment. |
| `DueDate` | Data limite opcional para resposta do assessment. |
| `CreatedAt` | Data de criação. |
| `SubmittedAt` | Data de resposta, quando existir. |

Estados:

| Status | Significado |
| --- | --- |
| `Draft` | Assessment criado, mas ainda não enviado ao learner. |
| `Published` | Assessment disponível para resposta. |
| `Answered` | Assessment respondido pelo learner. |

Relações:

- Um `Initial Assessment` pertence a uma `Mentorship`.
- Um `Initial Assessment` é criado pelo `Mentor`.
- Um `Initial Assessment` é respondido pelo `Learner`.
- Um `Initial Assessment` pode ter uma `DueDate`, mas o vencimento não impede que o `Learner` envie respostas.
- O `Initial Assessment` é recomendado para orientar o `Learning Plan`, mas não deve ser uma trava obrigatória para toda `Mentorship`.

### Learning Plan

Representa o plano de aprendizado da `Mentorship`.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do plano. |
| `MentorshipId` | Mentorship relacionada. |
| `Title` | Título do plano. |
| `Description` | Descrição geral do plano. |
| `Status` | Estado do plano. |
| `Tasks` | Atividades definidas pelo mentor. |
| `CreatedAt` | Data de criação. |
| `PublishedAt` | Data de publicação para o learner. |

Estados:

| Status | Significado |
| --- | --- |
| `Draft` | Plano em elaboração pelo mentor. |
| `Published` | Plano publicado para o learner. |
| `Completed` | Plano concluído. |

Relações:

- Um `Learning Plan` pertence a uma `Mentorship`.
- Um `Learning Plan` possui uma ou mais `Tasks`.

### Task

Representa uma atividade definida dentro do `Learning Plan`.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único da task. |
| `LearningPlanId` | Plano ao qual a task pertence. |
| `Title` | Título da task. |
| `Description` | Descrição da atividade. |
| `Status` | Estado atual da task. |
| `Order` | Ordem da task no plano. |
| `DueDate` | Data limite opcional para conclusão ou envio da task. |
| `SubmittedAt` | Data de envio para avaliação, quando existir. |
| `CompletedAt` | Data de conclusão, quando existir. |

Estados:

| Status | Significado |
| --- | --- |
| `Pending` | Task criada, mas ainda não iniciada pelo learner. |
| `InProgress` | Task em execução pelo learner. |
| `Blocked` | Task bloqueada por dúvida, dependência ou fator externo. |
| `Submitted` | Task enviada ao mentor para avaliação. |
| `Completed` | Task aprovada ou considerada finalizada. |

Relações:

- Uma `Task` pertence a um `Learning Plan`.
- Uma `Task` pode possuir `Feedback`.
- Uma `Task` pode ter uma `DueDate`, mas o prazo é opcional e não impede o envio da task para avaliação após o vencimento.

### Feedback

Representa a avaliação qualitativa do mentor.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do feedback. |
| `TaskId` | Task avaliada. |
| `MentorId` | Mentor responsável pelo feedback. |
| `Comment` | Comentário enviado ao learner. |
| `CreatedAt` | Data de criação. |

Relações:

- Um `Feedback` pertence a uma `Task`.
- Um `Feedback` é criado pelo `Mentor`.

### Rating

Representa a avaliação do mentor feita pelo learner ao final da mentorship.

| Atributo | Descrição |
| --- | --- |
| `Id` | Identificador único do rating. |
| `MentorshipId` | Mentorship avaliada. |
| `MentorId` | Mentor avaliado. |
| `LearnerId` | Learner que avaliou. |
| `Stars` | Nota em estrelas. |
| `Comment` | Comentário opcional. |
| `CreatedAt` | Data de criação. |

Relações:

- Um `Rating` pertence a uma `Mentorship`.
- Um `Rating` é criado pelo `Learner`.
- Um `Rating` influencia o ranqueamento futuro do `Mentor`.

## Value Objects

| Value Object | Atributos | Uso |
| --- | --- | --- |
| `Email` | `Value` | Identifica o usuário para login. |
| `PasswordHash` | `Value` | Armazena a senha protegida. |
| `Name` | `FirstName`, `LastName` | Nome de `Mentor` e `Learner`. |
| `Education` | `CourseName`, `Institution`, `Status`, `ConclusionDate` | Formação do `Mentor`. |
| `Bio` | `Value` | Descrição opcional do `Mentor`. |
| `Key` | `Value` | Chave única de uma `Stack`. |

## Agregados Sugeridos

### Identity Aggregate

Raiz: `User`

Inclui:

- `User`
- `User Registration`

Responsável por:

- cadastro
- credenciais
- escolha do tipo de perfil
- conclusão do onboarding

### Mentor Aggregate

Raiz: `Mentor`

Inclui:

- `Mentor`
- relação com `Stacks`
- disponibilidade

Responsável por:

- dados do mentor
- stacks de especialidade
- disponibilidade para mentorias

### Learner Aggregate

Raiz: `Learner`

Inclui:

- `Learner`

Responsável por:

- dados do learner

### Mentorship Request Aggregate

Raiz: `Mentorship Request`

Responsável por:

- solicitação de mentoria
- decisão do mentor
- criação da `Mentorship` após aceite

### Mentorship Aggregate

Raiz: `Mentorship`

Inclui:

- `Initial Assessment`
- `Learning Plan`
- `Task`
- `Feedback`
- `Rating`

Responsável por:

- condução da mentoria
- diagnóstico inicial
- plano de aprendizado
- execução das tasks
- feedback
- encerramento e avaliação

## Observação Sobre Implementação Atual

O backend atual já possui implementação para `User`, `User Registration`, `Mentor`, `Learner`, `Stack`, autenticação, autorização por perfil, cadastro de stacks do mentor, disponibilidade e busca de mentors por stacks.

Ainda são fases futuras do domínio: `Mentorship Request`, `Mentorship`, `Initial Assessment`, `Learning Plan`, `Task`, `Feedback` e `Rating`.
