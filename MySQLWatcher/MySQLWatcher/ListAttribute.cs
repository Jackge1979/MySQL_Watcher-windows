using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLWatcher
{
    /// <summary>
    /// 属性参数
    /// </summary>
    class ListAttribute : Attribute
    {

        public string[] lists;

        public ListAttribute()
        {
            lists = new string[] { "ON", "OFF"}; 

        }
    }

}
