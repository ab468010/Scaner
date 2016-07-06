namespace Model
{
    public class User : BaseModel
    {

        public User()
        {}
        public User(int userId,string name,string username,string password,int stateCode,int roleId)
        {
            this.SystemUserId = userId;
            this.Name = name;
            this.Username = username;
            this.Password = password;
            this.StateCode = stateCode;
            this.RoleId = roleId;
        }
  

        public User(int userId, string name, string username, string password, int stateCode, int roleId,string roleIdName)
        {
            this.SystemUserId = userId;
            this.Name = name;
            this.Username = username;
            this.Password = password;
            this.StateCode = stateCode;
            this.RoleId = roleId;
            this.RoleIdName = roleIdName;
        }
        public User(int userid,string name,string username,string email,string roleidname,string usercode)
        {
            this.SystemUserId = userid;
            this.Name = name;
            this.Username = username;
            this.RoleIdName = roleidname;
            this.Email = email;
            this.UserCode = usercode;
        }
        public User(int userid,string sname,string username,int statecode, string usercode,string email,string description,int roleid,string roleidname )
        {
            this.SystemUserId = userid;
            this.Name = sname;
            this.Username = username;
            this.UserCode = usercode;
            this.Email = email;
            this.Description = description;
            this.RoleId = roleid;
            this.StateCode = statecode;
            this.RoleIdName = roleidname;
        }
        public string Description { get; set; }
        public string Email { get; set; }
        public string UserCode { get; set; }
        public int SystemUserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int StateCode { get; set; }
        public int RoleId { get; set; }


        /*Join字段*/
        public string RoleIdName { get; set; }
    }
}
