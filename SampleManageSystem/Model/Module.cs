using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Module : BaseModel
    {
        public Module()
        {

        }

        public Module(int moduleid, string modulename, string tablename)
        {
            this.ModuleId = moduleid;
            this.Name = modulename;
            this.TableName = tablename;
        }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }

    }
}
