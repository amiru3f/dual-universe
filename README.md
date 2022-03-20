# Simple CQRS Sandbox
## READ/WRITE query splitting

This sandbox tries to describe a simple way to partition READ/WRITE  queries without affecting the main repository of Application layer (This case our repo is EFCore DbContext) using availability group listener



## What is an Always On availability group?
  
 An *availability group* supports a replicated environment for a discrete set of user databases, known as *availability databases*. You can create an availability group for high availability (HA) or for read-scale. An HA availability group is a group of databases that fail over together. A read-scale availability group is a group of databases that are copied to other instances of SQL Server for read-only workload. An availability group supports one set of primary databases and one to eight sets of corresponding secondary databases. Secondary databases are *not* backups. Continue to back up your databases and their transaction logs on a regular basis.  
  
### <a name="AvDbs"></a> Availability databases  
 To add a database to an availability group, the database must be an online, read-write database that exists on the server instance that hosts the primary replica. When you add a database, it joins the availability group as a primary database, while remaining available to clients. No corresponding secondary database exists until backups of the new primary database are restored to the server instance that hosts the secondary replica (using RESTORE WITH NORECOVERY). The new secondary database is in the RESTORING state until it is joined to the availability group. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server?view=sql-server-ver15).  
### <a name="AvailabilityModes"></a> Availability modes

The availability mode is a property of each availability replica. The availability mode determines whether the primary replica waits to commit transactions on a database until a given secondary replica has written the transaction log records to disk (hardened the log). supports two availability modes-*asynchronous-commit mode* and *synchronous-commit mode*.  
  
-   **Asynchronous-commit mode**  
  
     An availability replica that uses this availability mode is known as an *asynchronous-commit replica*. Under asynchronous-commit mode, the primary replica commits transactions without waiting for acknowledgment from asynchronous-commit secondary replicas to harden their transaction logs. Asynchronous-commit mode minimizes transaction latency on the secondary databases but allows them to lag behind the primary databases, making some data loss possible.  
  
-   **Synchronous-commit mode**  
  
     An availability replica that uses this availability mode is known as a *synchronous-commit replica*. Under synchronous-commit mode, before committing transactions, a synchronous-commit primary replica waits for a synchronous-commit secondary replica to acknowledge that it has finished hardening the log. Synchronous-commit mode ensures that once a given secondary database is synchronized with the primary database, committed transactions are fully protected. This protection comes at the cost of increased transaction latency. Optionally, SQL Server 2017 introduced a *required synchronized secondaries* feature to further increase safety at the cost of latency when desired. The REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT feature can be enabled to require a specified number of synchronous replicas to commit a transaction before a primary replica is allowed to commit. 
  
 For more information, see [Availability Modes &#40;Always On Availability Groups&#41;](https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups?view=sql-server-ver15). 

 ## How to run?
In order to run the project using containerized mode you can run the following command.
```
docker-compose up --build -d
```
It will run azure-sql-server as SQLServer database and a self-hosted dotnet app that seperates Command and query requests using [Readonly] attribute. 