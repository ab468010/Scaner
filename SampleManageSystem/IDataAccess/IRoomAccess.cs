using Model;
using System.Collections.Generic;

namespace IDataAccess
{
    public interface IRoomAccess : IBaseAccess<Model.Room>
    {

        /// <summary>
        /// 获得所有可用周转箱
        /// </summary>
        /// <returns></returns>
        IList<Room> GetRoomList();
        long GetRoomCount();
        IList<Room> GetRoomList(int number);
        bool CreateRoom(Room room);
        bool ExistsTaskByRoomId(int roomid);
        bool ExistsRoomCode(Room room);
    }
}
