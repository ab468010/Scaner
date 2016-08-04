using Logics;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace SystemService
{
    /// <summary>
    /// WorkService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WorkService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "HelloWorld";
        }

        [WebMethod]
        public XmlDocument GetUserList()
        {
            UserLogics userLogics = new UserLogics();
            IList<User> userList = userLogics.GetUserList();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<User>.EntityToXml(userList);
            return xmlDoc;
        }

        [WebMethod]
        public int[] usx(int[] a)
        {
            return a;
        }
        [WebMethod]
        public bool UpdateSampleContainerId(int[] sampleIds, int containerId,int systemuserId)
        {
            SampleLogics sampleLogics = new SampleLogics();
            int[] sampleIdList = sampleIds;
            foreach (int sampleId in sampleIdList)
            {
                if (!sampleLogics.UpdateSampleContainerId(sampleId, containerId,systemuserId))
                    return false;
            }
            return true;
        }
        [WebMethod]
        public bool UpdateSampleProjectId(int[] sampleIds, int projectId, int systemuserId)
        {
            SampleLogics sampleLogics = new SampleLogics();
            int[] sampleIdList = sampleIds;
            foreach(int sampleId in sampleIdList)
            {
                if (!sampleLogics.UpdateSampleProjectId(sampleId, projectId, systemuserId))
                    return false;
            }
            return true;
        }
        [WebMethod]
        public bool UpdateTaskSample(int[] sampleIds, int taskId)
        {
            SampleLogics sampleLogics = new SampleLogics();
            int[] sampleIdList = sampleIds;
            foreach (int sampleId in sampleIdList)
            {
                if (!sampleLogics.UpdateTaskSample(sampleId, taskId))
                    return false;
            }
            return true;
        }
        [WebMethod]
        public bool UpdateShelfId(int[] sampleIds, int shelfId, int systemuserId)
        {
            SampleLogics sampleLogics = new SampleLogics();
            int[] sampleIdList = sampleIds;
            foreach(int sampleId in sampleIdList)
            {
                if(!sampleLogics.UpdateShelfId(sampleId, shelfId,systemuserId))
                {
                    return false;
                }
            }
            return true;
        }
        [WebMethod]
        public bool UpdateTaskActualEnd(int taskid,int systemuserId)
        {
            TaskLogics task = new TaskLogics();
            return task.UpdateTaskActualEnd(taskid, systemuserId);
        }
        [WebMethod]
        public bool UpdateProjectEndtime(int projectId,int systemuserId)
        {
            ProjectLogics project = new ProjectLogics();
            return project.UpdateProjectEndtime(projectId, systemuserId);
        }
        /// <summary>
        /// 获得状态为'创建项目'、'项目测试'、'测试完成'的项目列表
        /// </summary>
        /// <returns>Xml文档</returns>
        [WebMethod]
        public XmlDocument GetProjectList()
        {
            ProjectLogics projectLogics = new ProjectLogics();
            IList<Project> projectList = projectLogics.GetProjectList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Project>.EntityToXml(projectList);
            return xmlDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="status"></param>
        /// <returns>Xml文档</returns>
        [WebMethod]
        public bool UpdateProjectStatus(int projectId,int status)
        {
            ProjectLogics projectLogics = new ProjectLogics();
            return projectLogics.UpdateStatus(projectId,status);
        }
        [WebMethod]
        public bool UpdateContainerProject(int containerId, int projectId)
        {
            ContainerLogics containerLogics = new ContainerLogics();
            return containerLogics.UpdateContainerProject(containerId, projectId);
        }

        [WebMethod]
         public bool UpdateContainerProjectId(int[] containerIds, int projectId, int systemuserId)
        {
            ContainerLogics containerLogics = new ContainerLogics();
            int[] containerIdList = containerIds;
            foreach(int containerId in containerIdList)
            {
                if (!containerLogics.UpdateContainerProjectId(containerId, projectId, systemuserId))
                {
                    return false;
                }
            
            } 
            return true;
        }
        [WebMethod]
        public XmlDocument GetContainerList()
        {
            ContainerLogics containerLogics = new ContainerLogics();
            IList<Container> containerList = containerLogics.GetContainerList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Container>.EntityToXml(containerList);
            return xmlDoc;
        }

        [WebMethod]
        public XmlDocument GetSampleList()
        {
            SampleLogics sampleLogics = new SampleLogics();
            IList<Sample> sampleList = sampleLogics.GetSampleList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Sample>.EntityToXml(sampleList);
            return xmlDoc;
        }

        [WebMethod]
        public XmlDocument GetRoomList()
        {
            RoomLogics roomLogics = new RoomLogics();
            IList<Room> roomList = roomLogics.GetRoomList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Room>.EntityToXml(roomList);
            return xmlDoc;
        }

        [WebMethod]
        public XmlDocument GetShelfList()
        {
            ShelfLogics shelfLogics = new ShelfLogics();
            IList<Shelf> shelfList = shelfLogics.GetShelfList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Shelf>.EntityToXml(shelfList);
            return xmlDoc;
        }


        /// <summary>
        /// 获得状态为'创建项目'、'项目测试'、'测试完成'的项目下的所有任务
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public XmlDocument GetTaskList()
        {
            TaskLogics taskLogics = new TaskLogics();
            IList<Task> taskList = taskLogics.GetTaskList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Task>.EntityToXml(taskList);
            return xmlDoc;
        }
    }
}
