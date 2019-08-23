# Bookloan-BackEnd
a simple manager of book loan


## Link WebAPI
CI e CD com AppServices do Azure
Link: https://bookloan.azurewebsites.net/swagger/index.html

## Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/mo00pbulcadi5eva/branch/master?svg=true)](https://ci.appveyor.com/project/mateusggeracino/bookloan-backend/branch/master)


## Para user
Primeiro crie um banco de dados e altere a ConnectionStrings presente no appsettings.json.
Após, execute no seu schema de banco de dados os script presentes na pasta:
https://github.com/mateusggeracino/Bookloan-BackEnd/tree/master/Bookloan/scripts

Assim, é necessário cadastrar um novo Cliente e após logar no sistema através da api: Auth. Com isso teremos um token para a navegação no sistema.

## Funcionalidades ↓
* Criar, Atualizar, Delete e Ler inforamções de Livros.
* Criar, Atualizar, Delete e Ler informações de Clientes.
* Criar e Ler informações das reservas que estão ligados a Cliente e Livro


## Tecnologias utilizadas ↓
* Swagger (Incluindo autenticação de Token)
* JWT
* Claims
* Sql Server
* Unit Tests
* Logger Errors
* HealthCheck
