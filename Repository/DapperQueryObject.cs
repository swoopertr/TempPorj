using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DapperQueryObject
    {
        public DapperQueryObject()
        {
            _params = new List<parameterObject> { };
        }
        public string query { get; set; }
        public List<parameterObject> _params { get; set; }
    }

    public class parameterObject {
        public parameterObject()
        {

        }
        public string parameterName { get; set; }
        public object value { get; set; }
        public System.Data.DbType DbType { get; set; }
    }
    
}
