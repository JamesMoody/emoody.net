using eMoody.Infrastructure;
using System;

namespace eMoody.DAO
{
    public class DataAccess : iDataAccess
    {
        public iWriting Writing { get => new Writing_InMemory(); }
    }
}
