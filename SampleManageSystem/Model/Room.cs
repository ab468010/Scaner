using System;
namespace Model
{
    public class Room : BaseModel
    {

        public Room()
        { }
        public Room(long count)
        {
            this.Count = count;
        }
        public Room(int roomId, string name, string roomCode)
        {
            this.RoomId = roomId;
            this.Name = name;
            this.RoomCode = roomCode;
        }
        public long Count { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string RoomCode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
