USE master;

IF DB_ID('bobshirt') IS NOT NULL
BEGIN

    ALTER DATABASE bobshirt 
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    
    DROP DATABASE bobshirt;
    
    PRINT 'Database bobshirt dropped successfully.';
END
ELSE
BEGIN
    PRINT 'Database bobshirt does not exist.';
END