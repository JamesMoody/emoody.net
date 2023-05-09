# Overview

I expect to be flipping between data sources. Might flip for the experience, cost, or just for fun. Either way, these interfaces will make sure I don't miss anything. 

##### General Assumptions

* iDataAccess
    * Expect this class to be littered everywhere. As such, it must be extremely light. It should hold as little data as possible. It’s basically a container to launch the database instances. 
    * It is assumed to be created via dependency injection, but it’s not required. 


* Database interfaces (iWriting & iBible)
    * I don’t see a need to create a hard database interface yet. So iWriting & iBible are the “database” interfaces. They are assumed to be heavy, short lived, and holding an active connection. 
    * The IDisposable interface is pulled in to enforce the use of a using() statement. It is assumed at least some derived classes will use an RDB, and thus something needing System.Data.Common.DbConnection. (Think Azure SQL, SQLite, etc.) The IDisposable pattern seems a good fit in this case. 

* DataModels contains simple POCO data models. 

* Configs contains simple POCO data models for config data. 
