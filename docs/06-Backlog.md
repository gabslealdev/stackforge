# Backlog

## Status

| Status | Significado |
| --- | --- |
| `Done` | Funcionalidade implementada. |
| `Next` | Recomendado para o próximo sprint. |
| `Planned` | Funcionalidade planejada para sprints futuros. |
| `Later` | Funcionalidade importante, mas fora do MVP imediato. |

## Epic 1: Identity and Access

| ID | História | Status |
| --- | --- | --- |
| US-001 | Como usuário, quero criar uma conta com email, senha e tipo de perfil para acessar a plataforma como `Mentor` ou `Learner`. | `Done` |
| US-002 | Como usuário, quero fazer login com email e senha para acessar minha conta. | `Done` |
| US-003 | Como sistema, quero emitir um token JWT com o tipo de perfil do usuário para controlar permissões. | `Done` |
| US-004 | Como sistema, quero restringir ações de `Mentor` e `Learner` conforme o perfil autenticado. | `Done` |

## Epic 2: Profile Onboarding

| ID | História | Status |
| --- | --- | --- |
| US-005 | Como `Mentor`, quero completar meu perfil com nome, data de nascimento, formação e bio opcional. | `Done` |
| US-006 | Como `Learner`, quero completar meu perfil com nome e data de nascimento. | `Done` |
| US-007 | Como sistema, quero impedir que um usuário complete um perfil diferente do tipo selecionado no cadastro. | `Done` |
| US-008 | Como sistema, quero impedir que um usuário complete mais de um perfil. | `Done` |
| US-009 | Como `Mentor`, quero visualizar meu perfil atual. | `Done` |
| US-010 | Como `Learner`, quero visualizar meu perfil atual. | `Done` |

## Epic 3: Stack Management

| ID | História | Status |
| --- | --- | --- |
| US-011 | Como sistema, quero manter um catálogo inicial de `Stacks` para associação com mentors. | `Done` |
| US-012 | Como `Mentor`, quero listar `Stacks` disponíveis para escolher minhas especialidades. | `Done` |
| US-013 | Como `Mentor`, quero adicionar uma `Stack` ao meu perfil. | `Done` |
| US-014 | Como sistema, quero impedir que o `Mentor` adicione a mesma `Stack` mais de uma vez. | `Done` |
| US-015 | Como `Mentor`, quero remover uma `Stack` do meu perfil. | `Planned` |

## Epic 4: Mentor Availability

| ID | História | Status |
| --- | --- | --- |
| US-016 | Como `Mentor`, quero marcar meu perfil como disponível ou indisponível para mentorias. | `Done` |
| US-017 | Como sistema, quero impedir que um `Mentor` fique disponível sem possuir ao menos uma `Stack`. | `Done` |

## Epic 5: Mentor Discovery

| ID | História | Status |
| --- | --- | --- |
| US-018 | Como `Learner`, quero pesquisar `Stacks` para escolher o que desejo aprender. | `Done` |
| US-019 | Como `Learner`, quero buscar `Mentors` por `Stack`. | `Done` |
| US-020 | Como `Learner`, quero ver apenas `Mentors` disponíveis na busca. | `Next` |
| US-021 | Como `Learner`, quero visualizar detalhes do perfil de um `Mentor` antes de solicitar mentoria. | `Next` |
| US-022 | Como sistema, quero ordenar `Mentors` por sinais de relevância, como `Rating`, disponibilidade e aderência à `Stack`. | `Later` |

## Epic 6: Mentorship Request

| ID | História | Status |
| --- | --- | --- |
| US-023 | Como `Learner`, quero informar meu objetivo final para a mentoria. | `Next` |
| US-024 | Como `Learner`, quero enviar um `Mentorship Request` para um `Mentor` escolhido. | `Next` |
| US-025 | Como `Mentor`, quero visualizar meus `Mentorship Requests` pendentes. | `Next` |
| US-026 | Como `Mentor`, quero aceitar um `Mentorship Request`. | `Next` |
| US-027 | Como `Mentor`, quero recusar um `Mentorship Request`. | `Planned` |
| US-028 | Como `Learner`, quero cancelar um `Mentorship Request` pendente. | `Planned` |
| US-029 | Como sistema, quero criar uma `Mentorship` ativa quando um `Mentorship Request` for aceito. | `Next` |

## Epic 7: Mentorship Management

| ID | História | Status |
| --- | --- | --- |
| US-030 | Como `Mentor`, quero visualizar minhas `Mentorships` ativas. | `Planned` |
| US-031 | Como `Learner`, quero visualizar minhas `Mentorships` ativas. | `Planned` |
| US-032 | Como participante, quero visualizar os dados principais de uma `Mentorship`. | `Planned` |
| US-033 | Como participante, quero cancelar uma `Mentorship` ativa. | `Later` |
| US-034 | Como participante, quero concluir uma `Mentorship`. | `Later` |

## Epic 8: Initial Assessment

| ID | História | Status |
| --- | --- | --- |
| US-035 | Como `Mentor`, quero criar um `Initial Assessment` para uma `Mentorship` ativa. | `Planned` |
| US-036 | Como `Mentor`, quero definir perguntas personalizadas no `Initial Assessment`. | `Planned` |
| US-037 | Como `Mentor`, quero definir uma `DueDate` opcional para o `Initial Assessment`. | `Planned` |
| US-038 | Como `Learner`, quero responder um `Initial Assessment`. | `Planned` |
| US-039 | Como `Mentor`, quero visualizar as respostas do `Initial Assessment`. | `Planned` |

## Epic 9: Learning Plan

| ID | História | Status |
| --- | --- | --- |
| US-040 | Como `Mentor`, quero criar um `Learning Plan` para uma `Mentorship` ativa. | `Planned` |
| US-041 | Como `Mentor`, quero criar `Tasks` dentro do `Learning Plan`. | `Planned` |
| US-042 | Como `Mentor`, quero definir uma `DueDate` opcional para cada `Task`. | `Planned` |
| US-043 | Como `Mentor`, quero publicar o `Learning Plan` para o `Learner`. | `Planned` |
| US-044 | Como `Learner`, quero visualizar meu `Learning Plan` e suas `Tasks`. | `Planned` |

## Epic 10: Task Execution and Feedback

| ID | História | Status |
| --- | --- | --- |
| US-045 | Como `Learner`, quero atualizar uma `Task` para `InProgress`, `Blocked` ou `Submitted`. | `Planned` |
| US-046 | Como `Mentor`, quero avaliar uma `Task` submetida. | `Planned` |
| US-047 | Como `Mentor`, quero enviar `Feedback` sobre uma `Task`. | `Planned` |
| US-048 | Como `Mentor`, quero marcar uma `Task` como `Completed`. | `Planned` |

## Epic 11: Rating and Reputation

| ID | História | Status |
| --- | --- | --- |
| US-049 | Como `Learner`, quero avaliar o `Mentor` após a conclusão da `Mentorship`. | `Later` |
| US-050 | Como sistema, quero usar `Ratings` como sinal de ranqueamento de `Mentors`. | `Later` |

## Epic 12: Documentation

| ID | História | Status |
| --- | --- | --- |
| US-051 | Como time, quero documentar a visão do produto. | `Done` |
| US-052 | Como time, quero documentar o glossário do domínio. | `Done` |
| US-053 | Como time, quero documentar requisitos. | `Done` |
| US-054 | Como time, quero documentar o modelo de domínio. | `Done` |
| US-055 | Como time, quero documentar regras de negócio. | `Done` |
| US-056 | Como time, quero documentar backlog e próximos passos. | `Done` |
| US-057 | Como time, quero manter rastreabilidade entre requisitos, regras e backlog. | `Planned` |

## Recomendação Para o Próximo Sprint

O próximo sprint deve focar em fechar o primeiro fluxo de valor ainda incompleto: `Learner` encontrar um `Mentor`, enviar um `Mentorship Request`, o `Mentor` aceitar, e o sistema criar uma `Mentorship` ativa.

Escopo recomendado:

| Item | Motivo |
| --- | --- |
| US-020 | Corrigir a busca para listar apenas `Mentors` disponíveis. |
| US-021 | Permitir que o `Learner` veja detalhes mínimos do `Mentor` antes do pedido. |
| US-023 | Capturar o objetivo final da mentoria. |
| US-024 | Criar `Mentorship Request` com status `Pending`. |
| US-025 | Permitir que o `Mentor` liste requests pendentes. |
| US-026 | Permitir aceite pelo `Mentor`. |
| US-029 | Criar `Mentorship` ativa após aceite. |

Critério de sucesso do sprint:

- Um `Learner` autenticado consegue selecionar uma `Stack`, escolher um `Mentor` disponível, informar um objetivo e enviar um `Mentorship Request`.
- O `Mentor` autenticado consegue visualizar o request pendente e aceitá-lo.
- O sistema cria uma `Mentorship` com status `Active`.
- Requests recusados/cancelados podem ficar para um sprint seguinte se houver necessidade de reduzir escopo.
