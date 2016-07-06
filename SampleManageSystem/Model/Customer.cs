namespace Model
{
    public  class Customer:BaseModel
    {
        public Customer()
        {

        }
 
        public Customer(int customer,string name,string description,int statecode)
        {
            this.Customerid = customer;
            this.Name = name;
            this.Description = description;
            this.StateCode = statecode;
        }
        public Customer(long count)
        {
            this.Count = count;
        }
        public Customer(int customerid,string name,string description)
        {
            this.Customerid = customerid;
            this.Name = name;
            this.Description = description;
        }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public long Count { get; set; }
        public int Customerid { get; set; }
        public string Name { get; set; }
        public  string  Description { get; set; }
        public int StateCode { get; set; }
    }
}
