# CrudFsbr

Projeto ASP.NET MVC para gerenciamento de produtos com operações de CRUD (Create, Read, Update, Delete).  
Inclui testes unitários usando xUnit.

---

## Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) ou superior
- IDE recomendada: Visual Studio 2022
- MSSQL Server ou outro SQL Server compatível

## Como rodar o projeto

1. Clone o repositório:
```
	git clone https://github.com/DanCostaDev/CRUD-FSBR.git
```
2. Restaure os pacotes NuGet:
```
	dotnet restore
```
3. Verifique o appsettings.json na raiz do projeto CRUD_FSBR:
```
	"ConnectionStrings": {
  	"Database": "Server=SERVER_NAME\\SQLEXPRESS;Database=DB_CRUD_FSBR;User id=USER;Password=PASSWORD;TrustServerCertificate=True"
	}
```
4. Altere a string de conexão conforme o seu ambiente (nome do servidor, usuário e senha do SQL Server)

5. Compile a aplicação
```
	dotnet build
```
6. Rode a aplicação:
```
	cd CRUD_FSBR
```
```
	dotnet run
```
A aplicação estará disponível em: https://localhost:5121 (ou a porta indicada no terminal).

---

## Como rodar os testes

1. Acesse a pasta da solução
2. Execute os testes com:
```
	dotnet test
```

Para testar e visualizar pelo Visual Studio, abra a solução e no projeto clique com o botão direito na solução e depois em "Executar Testes". Todos os testes estão na classe ```ProdutoControllerTestes.cs```