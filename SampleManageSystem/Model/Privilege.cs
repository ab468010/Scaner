using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Privilege:BaseModel
    {
        public Privilege()
        {

        }
        public Privilege(int privilegeid,int moduleid,string name,bool canread,bool cancreate,bool candelete,bool canwrite,bool canmanage ,string modulename,string tablename)
        {
            this.PrivilegeId = privilegeid;
            this.Name = name;
            this.ModuleName = modulename;
            this.CanRead = canread;
            this.CanCreate = cancreate;
            this.CanDelete = candelete;
            this.CanWrite = canwrite;
            this.ModuleId = moduleid;
            this.CanManage = canmanage;
            this.Tablename = tablename;
        }
      
        public int PrivilegeId { get; set; }
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanWrite { get; set; }
        public int ModuleId { get; set; }
        public bool CanManage { get; set; }
        public string Tablename { get; set; }
    }
}
