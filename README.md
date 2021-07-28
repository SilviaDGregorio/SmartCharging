# SmartCharging
Smart Charging Assignment

# Database

I'm using SQL Server as a Database. To be able to have it running go to this link -> https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services?view=sql-server-ver15

On my localhost I don't have any User/Password but if you add it one/have it then go to SmartCharging/src/SmartCharging.Api/appsettings.json and add the User/Password.
Example:

```
appsettings.json
{
    "ConnectionStrings": {
        "DataBase": "Server=localhost;Database=SmartCharging;User Id=myUsername;Password=myPassword;Trusted_Connection=True;"
    }
}
```

If you can't create a localhost database you can run the docker-compose.yml to create a docker container with SQL Server, then you need to change the connectionString as well:

$docker-compose up -d (to run the docker-compose.yml)
```
appsettings.json->
{
 "ConnectionStrings":{
    "Database": "Data Source=localhost;User ID=SA;Password=chAnge1t!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
}
```


# Errors
If you get an error like 'Failed to generate SSPI' its because the application can't reach the db, change the connectionString.

