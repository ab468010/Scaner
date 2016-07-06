using System;
using System.Collections.Generic;
using System.Data;
using Model;
using SqlHelper;
using Npgsql;
namespace DataAccess
{
    public class RoomAccess : IDataAccess.IRoomAccess
    {
        public bool ExistsRoomCode(Room room)
        {
            string st = "select count(1) from dbo.room where roomcode=@roomcode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roomcode",room.RoomCode)
            };
            if ((long)(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExistsTaskByRoomId(int roomid)
        {
            string st = "select count(1) from dbo.task where roomid=@roomid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roomid",roomid)
            };
            if ((long)(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CreateRoom(Room room)
        {
            string st = "insert into dbo.room (name,roomcode) values(@name,@roomcode)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",room.Name),
                new NpgsqlParameter("@roomcode",room.RoomCode)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<Room> GetRoomList(int number)
        {
            string sqlStr = @"SELECT roomid, name,roomcode
                                FROM dbo.room limit 10 offset @number";


            IList<Room> roomList = new List<Room>();
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Room room = new Room();
                    room.RoomId = Convert.ToInt32(rdr["roomid"]);
                    room.Name = rdr["name"].ToString();
                    room.RoomCode = rdr["roomcode"].ToString();
                    roomList.Add(room);
                    //roomList.Add(new Room(Convert.ToInt32(rdr["roomid"]),
                    //    rdr["name"].ToString(), rdr["size"].ToString(), rdr["roomcode"].ToString(), Convert.ToInt32(rdr["statuscode"])
                    //    , Convert.ToInt32(rdr["projectid"])));
                }
            }
            return roomList;
        }
        public long GetRoomCount()
        {
            string st = "select count(1) count from dbo.room";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public bool Delete(int roomid)
        {
            string st = "delete from dbo.room where roomid=@roomid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roomid",roomid)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.Room GetModel(int roomid)
        {
            Room room = null;
           string st="select roomid,name,roomcode from dbo.room where roomid=@roomid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roomid",roomid)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    room = new Room(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2));
                }
            }
            return room;
        }

        public IList<Room> GetRoomList()
        {
            string sqlStr = @"SELECT roomid, name,roomcode
                                FROM dbo.room";


            IList<Room> roomList = new List<Room>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Room room = new Room();
                    room.RoomId = Convert.ToInt32(rdr["roomid"]);
                    room.Name = rdr["name"].ToString();
                    room.RoomCode = rdr["roomcode"].ToString();
                    roomList.Add(room);
                    //roomList.Add(new Room(Convert.ToInt32(rdr["roomid"]),
                    //    rdr["name"].ToString(), rdr["size"].ToString(), rdr["roomcode"].ToString(), Convert.ToInt32(rdr["statuscode"])
                    //    , Convert.ToInt32(rdr["projectid"])));
                }
            }
            return roomList;
        }

        public bool Create(Model.Room room)
        {
            return false;
        }
        public bool Disable(int roomid)
        {
            string st = "delete from dbo.room  where roomid = @roomid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roomid",roomid),
              
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update(Room room)
        {
            string st = "update dbo.room set name = @name ,roomcode=@roomcode where roomid = @roomid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roomid",room.RoomId),
                new NpgsqlParameter("@name",room.Name),
                new NpgsqlParameter("@roomcode",room.RoomCode)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
