using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace Model
{
    public class TaskContainer : BaseModel
    {
        public int TaskContainerId { get; set; }
        public int TaskId { get; set; }
        public int ContainerId { get; set; }
    }

}
