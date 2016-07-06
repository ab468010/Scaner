using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
     public  class Role:BaseModel
    {
        public Role() { }
        public Role(int roleid,string rolename,string description)
        {
            this.RoleId = roleid;
            this.RoleName = rolename;
            this.Description = description;
        }
        public int RoleType { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
