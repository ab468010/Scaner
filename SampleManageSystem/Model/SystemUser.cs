namespace Model
{
    public  class SystemUser:BaseModel
    {
        public SystemUser()
        {

        }

        public SystemUser(int systemuserid,string name,string username,string password,int statecode)
        {
            this.UserName = username;
            this.SystemUserId = systemuserid;
            this.Password = password;
            this.Name = name;
            this.StateCode = statecode;
        }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int SystemUserId { get; set; }
        public int StateCode { get; set; }
    }
}
