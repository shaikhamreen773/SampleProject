use dummy
select * from EmployeesTable
CREATE TABLE [dbo].[EmployeesTable](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](50) NOT NULL,
    [Email] [nvarchar](50) NOT NULL,
    [Phone] [nvarchar](20) NOT NULL,
    [Address] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
   [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE PROCEDURE UpdateEmployee
    @ID int,
    @Name nvarchar(50),
    @Email nvarchar(50),
    @Phone nvarchar(20),
    @Address nvarchar(100)
AS
BEGIN
    UPDATE EmployeesTable SET
        Name = @Name,
        Email = @Email,
        Phone = @Phone,
		Address=@Address
    WHERE ID = @ID
END

CREATE PROCEDURE [dbo].[InsertEmployee]
@Name NVARCHAR(50),
@Email NVARCHAR(50),
@Phone NVARCHAR(20),
@Address NVARCHAR(100)
AS
BEGIN
INSERT INTO EmployeesTable(Name, Email, Phone, Address)
VALUES (@Name, @Email, @Phone, @Address)
END

use dummy
delete from EmployeesTable
SELECT * 
  FROM dummy.INFORMATION_SCHEMA.ROUTINES
 WHERE ROUTINE_TYPE = 'PROCEDURE'