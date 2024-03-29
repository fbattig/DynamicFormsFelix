USE [StudentDB]
GO
/****** Object:  Table [dbo].[CustomFields]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFields](
	[FieldName] [nvarchar](100) NULL,
	[FieldType] [nvarchar](100) NULL,
	[Fieldvalue] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentReg]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentReg](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'Firstname', N'Textbox', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'Lastname', N'Textbox', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'IsActive', N'Checkbox', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'State', N'Checkbox', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'City', N'Checkbox', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'Zip', N'Textbox', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'Gender', N'RadioButton', NULL)
INSERT [dbo].[CustomFields] ([FieldName], [FieldType], [Fieldvalue]) VALUES (N'Job', N'DropdownList', NULL)
SET IDENTITY_INSERT [dbo].[StudentReg] ON 

INSERT [dbo].[StudentReg] ([Id], [Name], [City], [Address]) VALUES (1, N'ww', N'ww', N'ww')
INSERT [dbo].[StudentReg] ([Id], [Name], [City], [Address]) VALUES (2, N'333', N'33333', N'33')
SET IDENTITY_INSERT [dbo].[StudentReg] OFF
/****** Object:  StoredProcedure [dbo].[AddNewStudent]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[AddNewStudent]  
(  
   @Name nvarchar (50),  
   @City nvarchar (50),  
   @Address nvarchar (100)  
)  
as  
begin  
   Insert into StudentReg values(@Name,@City,@Address)  
End
GO
/****** Object:  StoredProcedure [dbo].[CREATEMODEL]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[CREATEMODEL]  
(  
     @TableName SYSNAME ,  
     @CLASSNAME VARCHAR(500)   
)  
AS  
BEGIN  
    DECLARE @Result VARCHAR(MAX)  
  
    SET @Result = @CLASSNAME + @TableName + '  
{'  
  
SELECT @Result = @Result + '  
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }'  
FROM  
(  
    SELECT   
        REPLACE(col.NAME, ' ', '_') ColumnName,  
        column_id ColumnId,  
        CASE typ.NAME   
            WHEN 'bigint' THEN 'long'  
            WHEN 'binary' THEN 'byte[]'  
            WHEN 'bit' THEN 'bool'  
            WHEN 'char' THEN 'string'  
            WHEN 'date' THEN 'DateTime'  
            WHEN 'datetime' THEN 'DateTime'  
            WHEN 'datetime2' then 'DateTime'  
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'  
            WHEN 'decimal' THEN 'decimal'  
            WHEN 'float' THEN 'float'  
            WHEN 'image' THEN 'byte[]'  
            WHEN 'int' THEN 'int'  
            WHEN 'money' THEN 'decimal'  
            WHEN 'nchar' THEN 'char'  
            WHEN 'ntext' THEN 'string'  
            WHEN 'numeric' THEN 'decimal'  
            WHEN 'nvarchar' THEN 'string'  
            WHEN 'real' THEN 'double'  
            WHEN 'smalldatetime' THEN 'DateTime'  
            WHEN 'smallint' THEN 'short'  
            WHEN 'smallmoney' THEN 'decimal'  
            WHEN 'text' THEN 'string'  
            WHEN 'time' THEN 'TimeSpan'  
            WHEN 'timestamp' THEN 'DateTime'  
            WHEN 'tinyint' THEN 'byte'  
            WHEN 'uniqueidentifier' THEN 'Guid'  
            WHEN 'varbinary' THEN 'byte[]'  
            WHEN 'varchar' THEN 'string'  
            ELSE 'UNKNOWN_' + typ.NAME  
        END ColumnType,  
        CASE   
            WHEN col.is_nullable = 1 and typ.NAME in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')   
            THEN '?'   
            ELSE ''   
        END NullableSign  
    FROM SYS.COLUMNS col join sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id  
    where object_id = object_id(@TableName)  
) t  
ORDER BY ColumnId  
SET @Result = @Result  + '  
}'  
  
Select @Result  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteStudent]  
(  
   @StdId int  
)  
as   
begin  
   Delete from StudentReg where Id=@StdId  
End
GO
/****** Object:  StoredProcedure [dbo].[GetCustomFields]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetCustomFields]  
as  
begin  
   select * from CustomFields
End
GO
/****** Object:  StoredProcedure [dbo].[GetStudentDetails]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetStudentDetails]  
as  
begin  
   select * from StudentReg
End
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudentDetails]    Script Date: 2019-07-21 5:09:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[UpdateStudentDetails]  
(  
   @StdId int,  
   @Name nvarchar (50),  
   @City nvarchar (50),  
   @Address nvarchar (100)  
)  
as  
begin  
   Update StudentReg   
   set Name=@Name,  
   City=@City,  
   Address=@Address  
   where Id=@StdId  
End
GO
