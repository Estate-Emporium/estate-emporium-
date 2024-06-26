CREATE TABLE accounts (
    id INT IDENTITY(1,1) PRIMARY KEY,
    account_number CHAR(12) NOT NULL,
    company_name VARCHAR(50) NOT NULL
);
