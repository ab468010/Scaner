using System;
namespace Model
{
     public class Task:BaseModel
    {
        public Task()
        {

        }
        public Task(int taskid,string name,int projectid,int roomid,DateTime estimatedstart,DateTime estimatedend,DateTime actualstart,DateTime actualend,int statuscode,int createdby,DateTime createdon,int modifiedby,DateTime modifiedon,string description)
        {
            this.TaskId = taskid;
            this.Name = name;
            this.ProjectId = projectid;
            this.RoomId = roomid;
            this.EstimatedStart = estimatedstart;
            this.EstimatedEnd = estimatedend;
            this.ActualStart = actualstart;
            this.ActualEnd = actualend;
            this.StatusCode = statuscode;
            this.CreatedBy = createdby;
            this.CreatedOn = createdon;
            this.ModifiedBy = modifiedby;
            this.ModifiedOn = modifiedon;
            this.Description = description;
        }
        public Task(int taskid,string taskname,string projectname,string roomname, DateTime estimatedstart, DateTime estimatedend, DateTime actualstart, DateTime actualend,int projectstatuscode)
        {
            this.TaskId = taskid;
            this.Name = taskname;
            this.ProjectName = projectname;
            this.RoomName = roomname;
            this.EstimatedStart = estimatedstart;
            this.EstimatedEnd = estimatedend;
            this.ActualStart = actualstart;
            this.ActualEnd = actualend;
            this.ProjectStatusCode = projectstatuscode;
        }
        public Task(int taskid, string taskname, string projectname, string roomname, DateTime estimatedstart, DateTime estimatedend,string description)
        {
            this.TaskId = taskid;
            this.Name = taskname;
            this.ProjectName = projectname;
            this.RoomName = roomname;
            this.EstimatedStart = estimatedstart;
            this.EstimatedEnd = estimatedend;
            this.Description = description;
            
        }
  
        public string ProjectName { get; set; }
        public string RoomName { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int RoomId { get; set; }
        public DateTime? EstimatedStart { get; set; }
        public DateTime? EstimatedEnd { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualEnd { get; set; }
        public int StatusCode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Description { get; set; }
        public int Tester1 { get; set; }
        public int Tester2 { get; set; }
      public int ProjectStatusCode { get; set; }

    }
}
