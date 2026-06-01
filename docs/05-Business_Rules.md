# Business Rules

## Identity and Access

| ID | Regra |
| --- | --- |
| BR-001 | Um `User` deve possuir um email válido e único. |
| BR-002 | A senha de um `User` deve ser armazenada apenas como `PasswordHash`. |
| BR-003 | Um `User` deve escolher exatamente um tipo de perfil durante o cadastro: `Mentor` ou `Learner`. |
| BR-004 | Um `User Registration` inicia com status `InProgress`. |
| BR-005 | Um `User Registration` deve ser marcado como `Completed` somente após a criação do perfil selecionado. |
| BR-006 | Um `User` não pode completar um perfil diferente do `SelectedProfileType`. |
| BR-007 | Um `User` não pode completar mais de um perfil. |
| BR-008 | Ações exclusivas de `Mentor` só podem ser executadas por usuários autenticados com perfil `Mentor`. |
| BR-009 | Ações exclusivas de `Learner` só podem ser executadas por usuários autenticados com perfil `Learner`. |

## Profile

| ID | Regra |
| --- | --- |
| BR-010 | `Mentor` e `Learner` devem possuir `Name` válido. |
| BR-011 | `BirthDate` de `Mentor` e `Learner` não pode estar no futuro. |
| BR-012 | Um `Mentor` deve possuir `Education` válida. |
| BR-013 | A `Bio` do `Mentor` é opcional. |
| BR-014 | Quando informada, a `Bio` deve respeitar o tamanho mínimo e máximo definidos pelo domínio. |
| BR-015 | Um `Mentor` inicia com `Availability` igual a `Unavailable`. |
| BR-016 | Um `Mentor` só pode ficar `Available` se possuir ao menos uma `Stack`. |
| BR-017 | Um `Mentor` não pode adicionar a mesma `Stack` mais de uma vez. |
| BR-018 | Um `Mentor` não pode remover sua única `Stack` se isso deixar o perfil sem nenhuma stack. |

## Stack

| ID | Regra |
| --- | --- |
| BR-019 | Uma `Stack` deve possuir `Name` válido. |
| BR-020 | Uma `Stack` deve possuir `Key` única. |
| BR-021 | A busca de `Stacks` deve considerar o termo informado pelo usuário. |
| BR-022 | Uma busca de `Stacks` sem termo deve retornar lista vazia. |

## Mentor Discovery

| ID | Regra |
| --- | --- |
| BR-023 | Apenas `Mentors` com `Availability` igual a `Available` devem aparecer na descoberta de mentores. |
| BR-024 | A descoberta deve retornar apenas `Mentors` associados à `Stack` selecionada pelo `Learner`. |
| BR-025 | `Mentors` indisponíveis não devem receber novos `Mentorship Requests`. |

## Mentorship Request

| ID | Regra |
| --- | --- |
| BR-026 | Apenas um `Learner` pode criar um `Mentorship Request`. |
| BR-027 | Um `Mentorship Request` deve ser direcionado a um `Mentor` existente. |
| BR-028 | Um `Mentorship Request` deve referenciar uma `Stack` existente. |
| BR-029 | O `Mentor` do `Mentorship Request` deve possuir a `Stack` solicitada. |
| BR-030 | O `Mentor` do `Mentorship Request` deve estar `Available`. |
| BR-031 | Um `Mentorship Request` deve conter um `Goal` informado pelo `Learner`. |
| BR-032 | Um `Mentorship Request` deve iniciar com status `Pending`. |
| BR-033 | Apenas o `Mentor` destinatário pode aceitar ou recusar um `Mentorship Request`. |
| BR-034 | Apenas o `Learner` solicitante pode cancelar um `Mentorship Request`. |
| BR-035 | Um `Mentorship Request` só pode ser aceito, recusado ou cancelado enquanto estiver `Pending`. |
| BR-036 | Um `Mentorship Request` aceito deve criar exatamente uma `Mentorship`. |
| BR-037 | Um `Mentorship Request` recusado ou cancelado não deve criar uma `Mentorship`. |

## Mentorship

| ID | Regra |
| --- | --- |
| BR-038 | Uma `Mentorship` deve ser criada somente a partir de um `Mentorship Request` aceito. |
| BR-039 | Uma `Mentorship` deve iniciar com status `Active`. |
| BR-040 | Apenas o `Mentor` e o `Learner` participantes podem acessar dados da `Mentorship`. |
| BR-041 | Uma `Mentorship` só pode ser concluída se estiver `Active`. |
| BR-042 | Uma `Mentorship` concluída deve registrar `CompletedAt`. |
| BR-043 | Uma `Mentorship` cancelada deve encerrar o acompanhamento ativo entre `Mentor` e `Learner`. |

## Initial Assessment

| ID | Regra |
| --- | --- |
| BR-044 | Apenas o `Mentor` da `Mentorship` pode criar um `Initial Assessment`. |
| BR-045 | Um `Initial Assessment` deve pertencer a uma `Mentorship` existente e `Active`. |
| BR-046 | Um `Initial Assessment` deve possuir ao menos uma pergunta antes de ser publicado. |
| BR-047 | Um `Initial Assessment` inicia com status `Draft`. |
| BR-048 | Um `Initial Assessment` publicado deve ficar disponível para resposta do `Learner`. |
| BR-049 | Apenas o `Learner` da `Mentorship` pode responder o `Initial Assessment`. |
| BR-050 | Um `Initial Assessment` respondido deve registrar `SubmittedAt`. |
| BR-051 | Um `Initial Assessment` pode ter uma `DueDate`, mas ela não é obrigatória. |
| BR-052 | Uma `DueDate` vencida deve indicar atraso, mas não deve impedir que o `Learner` envie respostas. |
| BR-053 | O `Initial Assessment` é recomendado para orientar o `Learning Plan`, mas não é obrigatório para toda `Mentorship`. |

## Learning Plan

| ID | Regra |
| --- | --- |
| BR-054 | Apenas o `Mentor` da `Mentorship` pode criar ou alterar o `Learning Plan`. |
| BR-055 | Um `Learning Plan` deve pertencer a uma `Mentorship` existente e `Active`. |
| BR-056 | Um `Learning Plan` inicia com status `Draft`. |
| BR-057 | Um `Learning Plan` deve possuir ao menos uma `Task` antes de ser publicado. |
| BR-058 | Um `Learning Plan` publicado deve ficar disponível para visualização do `Learner`. |
| BR-059 | O `Learning Plan` deve considerar o `Goal` do `Learner`. |
| BR-060 | Quando houver respostas do `Initial Assessment`, o `Learning Plan` deve considerá-las. |

## Task

| ID | Regra |
| --- | --- |
| BR-061 | Apenas o `Mentor` da `Mentorship` pode criar `Tasks` no `Learning Plan`. |
| BR-062 | Uma `Task` deve pertencer a um `Learning Plan`. |
| BR-063 | Uma `Task` deve possuir título e descrição. |
| BR-064 | Uma `Task` inicia com status `Pending`. |
| BR-065 | Apenas o `Learner` da `Mentorship` pode atualizar o andamento da `Task`. |
| BR-066 | Uma `Task` pode mudar de `Pending` para `InProgress`. |
| BR-067 | Uma `Task` pode mudar de `InProgress` para `Blocked`. |
| BR-068 | Uma `Task` pode mudar de `Blocked` para `InProgress`. |
| BR-069 | Uma `Task` pode mudar para `Submitted` quando o `Learner` enviá-la para avaliação. |
| BR-070 | Apenas o `Mentor` da `Mentorship` pode marcar uma `Task` como `Completed`. |
| BR-071 | Uma `Task` pode ter uma `DueDate`, mas ela não é obrigatória. |
| BR-072 | Uma `DueDate` vencida deve indicar atraso, mas não deve impedir o envio da `Task` para avaliação. |

## Feedback

| ID | Regra |
| --- | --- |
| BR-073 | Apenas o `Mentor` da `Mentorship` pode criar `Feedback` para uma `Task`. |
| BR-074 | `Feedback` deve estar associado a uma `Task` da mesma `Mentorship`. |
| BR-075 | `Feedback` deve possuir comentário. |
| BR-076 | Uma `Task` submetida pode receber `Feedback` antes de ser marcada como `Completed`. |

## Rating

| ID | Regra |
| --- | --- |
| BR-077 | Apenas o `Learner` da `Mentorship` pode avaliar o `Mentor`. |
| BR-078 | O `Learner` só pode criar `Rating` após a conclusão da `Mentorship`. |
| BR-079 | Uma `Mentorship` pode possuir no máximo um `Rating`. |
| BR-080 | `Stars` deve estar dentro da escala definida pelo produto. |
| BR-081 | O comentário do `Rating` é opcional. |
| BR-082 | `Ratings` devem ser considerados no ranqueamento futuro de `Mentors`. |

## Domain Events

| ID | Regra |
| --- | --- |
| BR-083 | Domain Events não são necessários no MVP para `Mentorship Request`, pois o pedido e seu consumo pelo mentor pertencem ao mesmo `Mentorship Context`. |
| BR-084 | A criação de um `Mentorship Request` deve ser resolvida pelo caso de uso principal e disponibilizada ao mentor por consulta. |
| BR-085 | Domain Events podem ser reavaliados no futuro se surgirem efeitos colaterais desacoplados, como notificações assíncronas, email, auditoria avançada, analytics ou integração com outro contexto/sistema. |
