Hello, this is the Db Architecture with Liquibase (and SQL, but it can be adapted for MongoDB).
Actually I don't have any DB / Server Opened, so I do it without the possibility to test it.

How is it work?
I Create a ps1 file to execute to create the architecture of the Database:

.\UpdateDatabase.ps1 -si SERVER_INSTANCE  -dn DATABASE_NAME  -du DATABASE_USER [ -c rollback ]
pwd: USER_PASSWORD