USE master
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Vitta')
BEGIN
	CREATE DATABASE [Vitta]
END
GO
USE [Vitta]
GO
CREATE TABLE [Orders](
[id] int PRIMARY KEY IDENTITY,
[date] date NOT NULL,
[money_amount] decimal NOT NULL,
[amount_payable] decimal NOT NULL
)
GO
CREATE TABLE [MoneyFlows](
[id] int PRIMARY KEY IDENTITY,
[date] date NOT NULL,
[money_amount] decimal NOT NULL,
[rest_money] decimal NOT NULL
)
GO
CREATE TABLE [Payments](
[id] int PRIMARY KEY IDENTITY,
[order_id] int FOREIGN KEY REFERENCES [Orders]([id]) NULL,
[moneyflows_id] int FOREIGN KEY REFERENCES [MoneyFlows]([id]) NULL,
[payment_amount] decimal NOT NULL
)