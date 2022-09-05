using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public abstract partial class Record
    {
        public abstract SqlParameter[] SqlParameters();
    }
}
