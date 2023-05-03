﻿using System;

namespace eMoody.Infrastructure
{
    public interface iDataAccess
    {
        iWriting Writing { get; }
        iBible   Bible   { get; }
    }
}
