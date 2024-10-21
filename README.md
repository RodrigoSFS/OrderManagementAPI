# OrderManagementAPI

<h3>👋 Esta é uma WebAPI construída em ASP.NET 6.0 para gerenciar pedidos de produtos de uma loja.</h3>

<h4>🛠 As tecnologias e bibliotecas utilizadas são essas abaixo:</h4>

![Tecnologias ASPNET](https://github.com/user-attachments/assets/e26550dd-cf45-40b8-a6ee-a86dd8b5fe91)

<p>A documentação da API foi feito utilizando o Swagger, para verificação sem precisar rodar o projeto, há um PDF chamado "Documentação Swagger OrderManageAPI.PDF", no diretório raiz do projeto.</p>

<p>A arquitetura do projeto é similar ao MVC(Model View Controller), exceto pela ausência das Views e utilização de Services para abstrair Regras de Negócio dos Controllers.<br>Este sendo apenas responsável pelas Rotas.</p>

<h3>💡 Como Usar</h3>
<ol>
  <li>Clone este repositório:</li>
  <br>
  <pre><code>git clone https://github.com/RodrigoSFS/OrderManagementAPI.git</code></pre>
  <li>Faça o Build do projeto, administrando todas as versões e dependências e acesse os Endpoints documentados</li>
  <li>O projeto usa uma conexão do banco de dados, tornando necessário ajustar as credenciais de conexão com o banco MySQL no arquivo appsettings.Development.json</li>
</ol>
