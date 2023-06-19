# Sql表

```sql
CREATE TABLE [dbo].[BookTb1] (
    [BId]     INT            IDENTITY (100, 1) NOT NULL,
    [BTitle]  NVARCHAR (100) NOT NULL,
    [BAuthor] NVARCHAR (50)  NOT NULL,
    [BCat]    NVARCHAR (50)  NOT NULL,
    [BQty]    INT            NOT NULL,
    [BPrice]  FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([BId] ASC),
    UNIQUE NONCLUSTERED ([BTitle] ASC)
);

```

```sql
CREATE TABLE [dbo].[BillTb1] (
    [BillId] INT          IDENTITY (1000, 1) NOT NULL,
    [UName]  VARCHAR (50) NOT NULL,
    [Amount] FLOAT (53)   NOT NULL,
    PRIMARY KEY CLUSTERED ([BillId] ASC)
);


```

```sql
CREATE TABLE [dbo].[Admin] (
    [AdId]   INT        IDENTITY (10000, 1) NOT NULL,
    [AdName] NCHAR (20) NOT NULL,
    [AdPwd]  NCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AdId] ASC)
);

```

```sql
CREATE TABLE [dbo].[BorrowTb2] (
    [BrId]       INT            IDENTITY (200, 1) NOT NULL,
    [BName]      NVARCHAR (100) NOT NULL,
    [UserName]   VARCHAR (20)   NOT NULL,
    [BorrowDate] DATETIME       NOT NULL,
    [BookNum]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([BrId] ASC)
);

```

```sql
CREATE TABLE [dbo].[UserTb1] (
    [UId]       INT           IDENTITY (500, 1) NOT NULL,
    [UName]     VARCHAR (20)  NOT NULL,
    [UPhone]    VARCHAR (11)  NOT NULL,
    [UAdd]      NVARCHAR (50) NOT NULL,
    [UPassword] VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([UId] ASC),
    UNIQUE NONCLUSTERED ([UName] ASC)
);
```

