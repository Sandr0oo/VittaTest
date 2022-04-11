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
CREATE TABLE [MoneyInflow](
[id] int PRIMARY KEY IDENTITY,
[date] date NOT NULL,
[money_amount] decimal NOT NULL,
[rest_money] decimal NOT NULL
)
GO
CREATE TABLE [Payments](
[id] int PRIMARY KEY IDENTITY,
[order_id] int FOREIGN KEY REFERENCES [Orders]([id]) NULL,
[money_inflow_id] int FOREIGN KEY REFERENCES [MoneyInflow]([id]) NULL,
[payment_amount] decimal NOT NULL
)