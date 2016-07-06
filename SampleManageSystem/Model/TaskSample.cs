using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace Model
{
     public  class TaskSample:BaseModel
    {
        public TaskSample()
        {

        }
        public int TaskSampleId { get; set; }
        public int TaskId { get; set; }
        public int SampleId { get; set; }
    }
    
}
