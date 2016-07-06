
namespace Model
{
     public  class Shelf:BaseModel
    {
        public Shelf()
        {

        }
        public Shelf(int shelfid,string name,string shelfcode,string description)
        {
            this.ShelfId = shelfid;
            this.Name = name;
            this.ShelfCode = shelfcode;
            this.Description = description;
        }
        public int ShelfId { get; set; }
        public string Name { get; set; }
        public string ShelfCode { get; set; }
        public string Description { get; set; }
    }
}
