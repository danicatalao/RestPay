# RestPay

API RESTFull para transações financeiras.

Aplicação web feita em ASP.NET Core, utilizando MongoDB para persistências dos dados.

## Especificações

- O sistema possui 2 tipos de usuários, os comuns e lojistas, ambos têm carteira com dinheiro e realizam transferências entre eles. Ambos usuários possuem do nome, e-mail e senha. Além dessas informações, usuários comuns possuem CPF e lojistas possuem CNPJ. CPF/CNPJ e e-mails são únicos no sistema.
- Usuários podem enviar dinheiro (efetuar transferência) para lojistas e entre usuários.
- Lojistas só recebem transferências, não enviam dinheiro para ninguém.
- Transferências são autorizadas por um serviço externo.
- Transferências são transações a nível de base.
- Na conclusão de uma transferência, o valor transferido e o contato do beneficiário são despachados à um serviço externo de envio de notificação.

## Arquitetura do sistema

O sistema aqui concebido consiste de 2 aplicações conteinerizadas e uma externa.

*Container:*
- Aplicação ASP.NET Core com a API descrita.
- Base de dados local MongoDB.

*Aplicação externa:*
- Base de dados MongoDB fragmentada (https://docs.mongodb.com/manual/sharding/).

A API permite que transações sejam feitas na base externa de usuários enquanto armazena os registros das transações na base local. 

### Requisitos

- Docker Engine para execução dos containers.
- Sharded Cluster disponível para base de usuários. Há um disponível listado na montagem de ambiente.

### Montagem de ambiente

`$ docker compose up --build` para build e deploy das seguintes componentes:
- api-server: API para transações
- mongo: Base local de transações
- mongo-express: Interface para visualização da base

No arquivo [appsettings.json](https://github.com/danicatalao/RestPay/blob/main/appsettings.json), a chave UserProviderSettings possui a connectionString do servidor de usuários. A ConnectionString: "mongodb+srv://restpay-default-user:29twLBreFn4wM2ES@restpay.zaoxk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority" no repositório é um Sharded Cluster válido para testes. Está populado com os seguintes dados:
```
{
  "_id": {
    "$oid": "609a149c7b5239e87a126ba9"
  },
  "_t": [
    "User",
    "NormalPerson"
  ],
  "nome": "Red Guy",
  "senha": "$2y$12$1x//ASzyeZ0Tegxk3eObe.zsEjE4pfpVF/pzyYn9SEeEjz5UrBhBa",
  "email": "redguy@gmail.com",
  "carteira": {
    "$numberDecimal": "5000.00"
  },
  "cpf": "12345678911"
}
```

```
{
  "_id": {
    "$oid": "609a149c7b5239e87a126baa"
  },
  "_t": [
    "User",
    "NormalPerson"
  ],
  "nome": "Yellow Guy",
  "senha": "$2y$12$/1HVewZglsliW9FLoP0.jeLQNkF1K0CWU1HdV7OqLDfotr6ELkrBO",
  "email": "yellowguy@gmail.com",
  "carteira": {
    "$numberDecimal": "5000.00"
  },
  "cpf": "12345678912"
}
```

```
{
  "_id": {
    "$oid": "609a149c7b5239e87a126bab"
  },
  "_t": [
    "User",
    "NormalPerson"
  ],
  "nome": "Duck",
  "senha": "$2y$12$tjQiNdcnQvj3k2RWM5VNY.dLe4sEBNcBwUOox/q76jjkDy.jm2ZBC",
  "email": "duck@gmail.com",
  "carteira": {
    "$numberDecimal": "5000.00"
  },
  "cpf": "12345678913"
}
```

```
{
  "_id": {
    "$oid": "609a149c7b5239e87a126bac"
  },
  "_t": [
    "User",
    "LegalPerson"
  ],
  "nome": "Tony the Clock",
  "senha": "$2y$12$BrUGsxFcQepDqFctxVEx6eR5NRyNM28p8LaX8jLLg/kDlnergnqrS",
  "email": "tonytheclock@gmail.com",
  "carteira": {
    "$numberDecimal": "20000"
  },
  "cnpj": "12345678912345"
}
```

```
{
  "_id": {
    "$oid": "609a149c7b5239e87a126bad"
  },
  "_t": [
    "User",
    "LegalPerson"
  ],
  "nome": "Colin the Computer",
  "senha": "$2y$12$WU/MTfVJ2AM8/.zhG/NJq.QK7j5UryAtPgzvQZs9fTLN0pdeiMFCC",
  "email": "colinthecomputer@gmail.com",
  "carteira": {
    "$numberDecimal": "20000.00"
  },
  "cnpj": "12345678912346"
}
```

```
{
  "_id": {
    "$oid": "609a149c7b5239e87a126bae"
  },
  "_t": [
    "User",
    "LegalPerson"
  ],
  "nome": "Sketchbook",
  "senha": "$2y$12$rv87/eQpjh.mJsqSVrW7he3cTPRl0MAFQ/XN3zqRx5N.uOT/Jzz0y",
  "email": "sketchbook@gmail.com",
  "carteira": {
    "$numberDecimal": "20200.00"
  },
  "cnpj": "123456789123457"
}
```

Para popular um servidor distinto, utilize o recurso [LINK]


## Endpoints

### Mockusers

### Transact

#### Exemplo de requisição



