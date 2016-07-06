using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace Model
{
      public class ProjectTemplate:BaseModel
    {
        public ProjectTemplate()
        {

        }
        public ProjectTemplate(int projecttemplateid,string taskname,int roomid,string description)
        {
            this.ProjectTemplateId = projecttemplateid;
            this.TaskName = taskname;
            this.RoomId = roomid;
            this.Description = description;
        }
        public int ProjectTemplateId { get; set; }
        public string TaskName { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
    }
}
