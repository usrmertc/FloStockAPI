
<h1 align=center> FloStockAPI </h1>

`FloStockAPI` is a streamlined solution designed to efficiently manage small-scale stock inventories within departments. This API provides a simple yet effective way to keep track of stock levels, ensuring that your department’s inventory is always up-to-date and well-organized.


## ⚠️ Disclaimer

This API project was created as an example for an internship program. It is not intended for use in any production environment and has no affiliation with any company. Feel free to build upon this project as you wish.

## 🚀 Quick Start

To get started, clone the repository and navigate to the  `FloStockAPI`  directory:


```bash
git clone https://github.com/usrmertc/FloStockAPI.git
cd FloStockAPI

```

### 🐳 1) Build with Docker

Building  `FloStockAPI`  with Docker, is so simple. Just run the following commands in the root directory:

```bash
docker compose build
docker compose up -d

```

### 🛠️ 2) Manual Build

If you prefer a manual build, follow these steps. First, set up a PostgreSQL Database. You can either install it yourself or run it as a Docker container:

```bash
docker run --name StocksDB -e POSTGRES_PASSWORD=P@ssWord!23 -e POSTGRES_DB=stocksdb -p 5432:5432 -d postgres:16.4
```

Next, simply restore, build and run your application:

```bash
dotnet restore 
dotnet build
dotnet run --project FloAPI
```

## 🧪 Running the Tests
To run the tests, simply execute:

```bash
dotnet test
```