﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
    public interface ITaskSampleAccess:IBaseAccess<Model.TaskSample>
    {
        bool CreateTaskSample(int taskid, int sampleid);

        IList<TaskSample> GetTaskSampleList();
    }
}
