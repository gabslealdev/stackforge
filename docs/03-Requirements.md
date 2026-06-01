# Requirements

## Requisitos Funcionais

### Identity and Access

| ID | Requisito |
| --- | --- |
| RF-001 | O sistema deve permitir que um usuário se cadastre com email, senha e tipo de perfil selecionado: `Mentor` ou `Learner`. |
| RF-002 | O sistema deve permitir que um usuário cadastrado faça login com email e senha. |
| RF-003 | O sistema deve identificar o tipo de perfil do usuário autenticado e restringir ações exclusivas de `Mentor` e `Learner`. |

### Mentor Onboarding

| ID | Requisito |
| --- | --- |
| RF-004 | O sistema deve permitir que um `Mentor` complete seu perfil com nome, data de nascimento, informações de formação e bio opcional. |
| RF-005 | O sistema deve permitir que um `Mentor` adicione `Stacks` de especialidade ao seu perfil. |
| RF-006 | O sistema deve permitir que um `Mentor` marque seu perfil como disponível ou indisponível para mentorias. |
| RF-007 | O sistema deve impedir que um `Mentor` fique disponível sem possuir ao menos uma `Stack` cadastrada. |

### Learner Onboarding

| ID | Requisito |
| --- | --- |
| RF-008 | O sistema deve permitir que um `Learner` complete seu perfil com nome e data de nascimento. |
| RF-009 | O sistema deve permitir que um `Learner` autenticado acesse funcionalidades de descoberta de mentorias. |

### Mentor Discovery

| ID | Requisito |
| --- | --- |
| RF-010 | O sistema deve permitir que um `Learner` pesquise `Stacks`. |
| RF-011 | O sistema deve permitir que um `Learner` selecione a `Stack` que deseja aprender. |
| RF-012 | O sistema deve permitir que um `Learner` informe seu objetivo final para a mentoria. |
| RF-013 | O sistema deve listar `Mentors` disponíveis que possuam a `Stack` selecionada. |
| RF-014 | O sistema deve permitir que um `Learner` visualize informações do perfil de um `Mentor` antes de solicitar mentoria. |

### Mentorship Request

| ID | Requisito |
| --- | --- |
| RF-015 | O sistema deve permitir que um `Learner` envie um `Mentorship Request` para um `Mentor` selecionado. |
| RF-016 | O `Mentorship Request` deve conter o `Learner`, o `Mentor`, a `Stack` selecionada e o objetivo final do `Learner`. |
| RF-017 | O sistema deve permitir que um `Mentor` visualize os `Mentorship Requests` recebidos. |
| RF-018 | O sistema deve permitir que um `Mentor` aceite um `Mentorship Request`. |
| RF-019 | O sistema deve criar uma `Mentorship` ativa quando um `Mentorship Request` for aceito. |

### Initial Assessment

| ID | Requisito |
| --- | --- |
| RF-020 | O sistema deve permitir que um `Mentor` crie um `Initial Assessment` para uma `Mentorship` ativa. |
| RF-021 | O sistema deve permitir que o `Mentor` defina perguntas personalizadas no `Initial Assessment`. |
| RF-022 | O sistema deve enviar ou publicar o `Initial Assessment` para o `Learner` dentro da `Mentorship`. |
| RF-023 | O sistema deve permitir que o `Learner` responda o `Initial Assessment`. |
| RF-024 | O sistema deve disponibilizar as respostas do `Initial Assessment` para o `Mentor`. |

### Learning Plan

| ID | Requisito |
| --- | --- |
| RF-025 | O sistema deve permitir que um `Mentor` crie um `Learning Plan` para uma `Mentorship` ativa. |
| RF-026 | O `Learning Plan` deve considerar o objetivo final do `Learner` e as respostas do `Initial Assessment`. |
| RF-027 | O sistema deve permitir que o `Mentor` defina `Tasks` dentro de um `Learning Plan`. |
| RF-028 | O sistema deve publicar o `Learning Plan` para o `Learner`. |
| RF-029 | O sistema deve permitir que o `Learner` visualize o `Learning Plan` e suas `Tasks`. |

### Task Execution and Feedback

| ID | Requisito |
| --- | --- |
| RF-030 | O sistema deve permitir que um `Learner` atualize o status de uma `Task`. |
| RF-031 | O sistema deve permitir que um `Learner` envie uma `Task` para avaliação do `Mentor`. |
| RF-032 | O sistema deve permitir que um `Mentor` avalie uma `Task` submetida. |
| RF-033 | O sistema deve permitir que um `Mentor` envie `Feedback` sobre uma `Task` submetida. |

### Closure and Reputation

| ID | Requisito |
| --- | --- |
| RF-034 | O sistema deve permitir que uma `Mentorship` seja concluída. |
| RF-035 | O sistema deve permitir que o `Learner` avalie o `Mentor` após a conclusão da `Mentorship`. |
| RF-036 | O sistema deve usar `Ratings` como sinal de ranqueamento de `Mentors` em buscas futuras. |

## Requisitos Não Funcionais

| ID | Requisito |
| --- | --- |
| RNF-001 | O sistema deve proteger rotas autenticadas usando autenticação baseada em token. |
| RNF-002 | O sistema deve armazenar senhas apenas como hashes. |
| RNF-003 | O sistema deve validar os dados de entrada antes de criar ou alterar entidades de domínio. |
| RNF-004 | O sistema deve impedir que ações exclusivas de `Mentor` sejam acessadas por `Learners` e que ações exclusivas de `Learner` sejam acessadas por `Mentors`. |
| RNF-005 | O sistema deve manter dados de uma `Mentorship` acessíveis apenas aos usuários que fazem parte dela. |

## Status Sugeridos

### Mentorship Request Status

| Status | Significado |
| --- | --- |
| Pending | Pedido enviado pelo `Learner` e aguardando decisão do `Mentor`. |
| Accepted | Pedido aceito pelo `Mentor`. |
| Rejected | Pedido recusado pelo `Mentor`. |
| Cancelled | Pedido cancelado antes do aceite. |

### Mentorship Status

| Status | Significado |
| --- | --- |
| Active | `Mentorship` aceita e em andamento. |
| Completed | `Mentorship` finalizada com sucesso. |
| Cancelled | `Mentorship` encerrada antes da conclusão. |

### Task Status

| Status | Significado |
| --- | --- |
| Pending | `Task` criada, mas ainda não iniciada pelo `Learner`. |
| InProgress | `Task` em execução pelo `Learner`. |
| Blocked | `Task` bloqueada por dúvida, dependência ou fator externo. |
| Submitted | `Task` enviada ao `Mentor` para avaliação. |
| Completed | `Task` aprovada ou considerada finalizada após avaliação do `Mentor`. |
