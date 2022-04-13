USE master
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Vitta')
BEGIN
	CREATE DATABASE [Vitta]
END
GO
USE [Vitta]
GO
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Orders](
	[id] int PRIMARY KEY IDENTITY,
	[date] date NOT NULL,
	[money_amount] decimal NOT NULL,
	[amount_payable] decimal NOT NULL
	)
END
GO
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[MoneyInflow]') AND type in (N'U'))
BEGIN
	CREATE TABLE [MoneyInflow](
	[id] int PRIMARY KEY IDENTITY,
	[date] date NOT NULL,
	[money_amount] decimal NOT NULL,
	[rest_money] decimal NOT NULL
	)
END
GO
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Payments]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Payments](
	[id] int PRIMARY KEY IDENTITY,
	[order_id] int FOREIGN KEY REFERENCES [Orders]([id]) NULL,
	[money_inflow_id] int FOREIGN KEY REFERENCES [MoneyInflow]([id]) NULL,
	[payment_amount] decimal NOT NULL
	)
END
GO
IF EXISTS (SELECT name FROM sysobjects
      WHERE name = 'payments_insert' AND type = 'TR')
   DROP TRIGGER payments_insert
GO
CREATE TRIGGER payments_insert
ON [Payments]
AFTER INSERT 
AS 
DECLARE @order_summ DECIMAL = 0,
    @order_payedsumm DECIMAL = 0,
    @inflow_summ DECIMAL = 0,
    @inflow_rest DECIMAL = 0,
    @pay_summ DECIMAL = 0,
    @needToPay DECIMAL = 0
    SELECT
	@order_summ = o.money_amount,
	@order_payedsumm = o.amount_payable,
	@inflow_summ = mi.money_amount,
	@inflow_rest = mi.rest_money,
	@pay_summ = p.payment_amount
FROM
	INSERTED AS p
INNER JOIN
   Orders AS o
   ON
	p.order_id = o.id
INNER JOIN
   MoneyInflow AS mi
   ON
	mi.id = p.money_inflow_id
SET
	@needToPay = @order_summ - @order_payedsumm
IF @needToPay = 0
BEGIN
	RAISERROR('Заказ уже полностью оплачен', 16, 1)
	ROLLBACK TRANSACTION
	RETURN
END
ELSE
	BEGIN 
		IF @inflow_rest = 0
		BEGIN
			RAISERROR('В выбранном приходе денег остаток равен 0', 16, 1)
			ROLLBACK TRANSACTION
			RETURN
		END
	END
IF @needToPay>0 AND @needToPay >= @inflow_rest
BEGIN 
	UPDATE ord 
	SET ord.amount_payable = ord.amount_payable + @inflow_rest
	FROM Orders ord
	INNER JOIN 
	INSERTED pay
	ON pay.order_id = ord.id;
	UPDATE mi
	SET mi.rest_money = 0
	FROM MoneyInflow mi
	INNER JOIN 
	INSERTED pay
	ON pay.money_inflow_id  = mi.id;
END
IF @needToPay > 0 AND @needToPay < @inflow_rest
BEGIN 
	UPDATE ord 
	SET ord.amount_payable = ord.amount_payable + @needToPay
	FROM Orders ord
	INNER JOIN 
	INSERTED pay
	ON pay.order_id = ord.id;
	UPDATE mi
	SET mi.rest_money = @inflow_rest - @needToPay
	FROM MoneyInflow mi
	INNER JOIN 
	INSERTED pay
	ON pay.money_inflow_id  = mi.id;
	UPDATE p
	SET p.payment_amount = @needToPay
	FROM Payments p
	INNER JOIN INSERTED i
	ON i.Id = p.Id
END