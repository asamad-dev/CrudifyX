CREATE PROCEDURE sp_GetAllProducts AS BEGIN SELECT * FROM Products END;


CREATE PROCEDURE sp_InsertProduct (@Name NVARCHAR(100), @Price DECIMAL(10,2), @Quantity INT)
AS BEGIN INSERT INTO Products(Name, Price, Quantity) VALUES (@Name, @Price, @Quantity) END;

CREATE PROCEDURE sp_UpdateProduct (@Id INT, @Name NVARCHAR(100), @Price DECIMAL(10,2), @Quantity INT)
AS BEGIN UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id END;

CREATE PROCEDURE sp_DeleteProduct (@Id INT)
AS BEGIN DELETE FROM Products WHERE Id = @Id END;
