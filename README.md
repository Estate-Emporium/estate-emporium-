# Project
Estate Emporium

## Sections
- [SQL Migrations instructions](#SQL-Migrations-instructions)
- [Postman](#Postman)
- [Run-instructions](#Run-instrutions)
- [Access the database using MS SQL Server studio](#database-access)

## SQL Migrations instructions
- upload your sql to the migrations folder as a new file with this naming convention V{year}{month}{day}{24hour}{min}__{description}.sql

## Postman
- Launch postman and import postman folder 

## Run-instructions

### Running server
```
cd backend/estate-emporium
dotnet run
open localhost:80
```

### Running frontend
```
cd frontend
npm install
npm run dev
```

### Running static build
```
cd frontend
npm install
npm run build
npm run preview
```

## Access the database using MS SQL Server studio
- Open ms sql sever studio
- Set server type to database engine
- Insert {estate-emporium-db.csxil53pu1c8.eu-west-1.rds.amazonaws.com} as the server name
- Select SQL sever authentication
- login as admin
- Password: 

## ERD Design
