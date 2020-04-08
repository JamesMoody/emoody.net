# Overview

This implements the DAO designs in eMoody.Infrastructure interfaces. 



* DataAccess
    * Nice and simple. Implements eMoody.Infrastructure.iDataAccess
    * SQLite config data is injected into this class via eMoody.Infrastructure.Configs.DataConfig.

* Bible_Sqlite
    * Implements eMoody.Infrastructure.iBible
    * Hits a local, read-only SQLite database for data.

* Writing_InMemory
    * Implements eMoody.Infrastructure.iWriting
    * Uses in-memory objects for data. (Might move later)

* WritingArtices
    * Holds the data for Writing_InMemory.


