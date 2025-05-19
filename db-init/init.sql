IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ContactsDb')
BEGIN
    CREATE DATABASE [ContactsDb];
END
