using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class RoomLogics : BaseLogics<Room>
    {
        private const string _Type = "RoomAccess";
        private IDataAccess.IRoomAccess _Dal;

        public RoomLogics()
            : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IRoomAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }


        public IList<Room> GetRoomList()
        {
            return _Dal.GetRoomList();
        }
        public long GetRoomCount()
        {
            return _Dal.GetRoomCount();
        }
        public IList<Room> GetRoomList(int number)
        {
            return _Dal.GetRoomList(number);
        }
        public bool CreateRoom(Room room)
        {
            if (_Dal.ExistsRoomCode(room))
            {
                return false;
            }
            else
            {
                return _Dal.CreateRoom(room);
            }
            
        }
        public bool DeleteRoom(int roomid)
        {
            if (_Dal.ExistsTaskByRoomId(roomid))
            {
                return false;
            }
            else
            {
                return _Dal.Delete(roomid);
            }
        }
       
    }
}
