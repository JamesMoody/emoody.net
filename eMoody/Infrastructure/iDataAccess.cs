using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.Infrastructure
{
    public interface iDataAccess
    {
        iWriting Writing { get; }
        iBible Bible { get; }
    }
}
