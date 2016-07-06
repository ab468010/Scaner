namespace Model
{
    public  class Contact:BaseModel
    {
        public Contact()
        {

        }
        public Contact(int contactid,string contactname,int customerid)
        {
            this.Contactid = contactid;
            this.Name = contactname;
            this.Customerid = customerid;
        }
        public Contact(int contactid,string contactname,string customername)
        {
            this.Contactid = contactid;
            this.Name = contactname;
            this.Customername = customername;
        }
        public Contact(int contactid ,string contactname, string description,string customername)
        {
            this.Contactid = contactid;
            this.Name = contactname;
            this.Customername = customername;
            this.Description = description;
           
        }
        public Contact(long count)
        {
            this.Count = count;
        }
        public long Count { get; set; }
        public string Description { get; set; }     
        public int Contactid { get; set; }
        public string Name { get; set; }
        public int Customerid { get; set; }
       public string Customername { get; set; }
    }
}
