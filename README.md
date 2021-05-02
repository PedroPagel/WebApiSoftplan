# WebApiSoftplan
Teste Softplan

Projetos

WebApiSoftplan1:
Contem a API RetornarTaxaJuros, que retorna a taxa fixa de juros.

WebApiSoftplan2:
API CalcularJuros, faz o calculo composto com o valor inicial indicado, tempo "meses" e a taxa fixa de juros.

Infrastructure:
Contem um gerenciador de API, faz uma chamado async de uma api

Contract:
API de contratos result, models, requests

Services:
Contem serviços com objetos de negocio

TestProject
Incluidos os testes unitarios e de integração.

-----------------

WebApiSoftplan1 pode ser executado separadamente, ira abrir o swagger da API

WebApiSoftplan2 chamará as três API, pois a WebApiSoftplan1 esta referenciada.

TestProject basta dar run tests.

-----------------

Docker, está com algum erro na minha máquina, apesar de montar as imagens e configurar o dockerfile, o local host não esta subindo.
