CREATE DATABASE ExemploCRUD;
GO 
USE ExemploCRUD;

CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

GO  

CREATE PROCEDURE usp_GetClientes
AS
BEGIN
    SELECT * FROM Clientes;
END;

GO  

CREATE PROCEDURE usp_GetClienteById
    @Id INT
AS
BEGIN
    SELECT * FROM Clientes WHERE Id = @Id;
END;

GO 
 


GO 

CREATE PROCEDURE usp_AtualizarCliente
    @Id INT,
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Clientes SET Nome = @Nome, Email = @Email WHERE Id = @Id;
END;

GO 

CREATE PROCEDURE usp_DeletarCliente
    @Id INT
AS
BEGIN
    DELETE FROM Clientes WHERE Id = @Id;
END;


CREATE PROCEDURE usp_InserirCliente
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100),
    @InsertedId INT OUTPUT
AS
BEGIN
    INSERT INTO Clientes (Nome, Email)
    VALUES (@Nome, @Email);

    -- Retorna o ID inserido
    SET @InsertedId = SCOPE_IDENTITY();
END;
