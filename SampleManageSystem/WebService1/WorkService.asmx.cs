using Logics;
using Model;
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
        public XmlDocument GetUserList()
        {
            UserLogics userLogics = new UserLogics();
            IList<User> userList = userLogics.GetUserList();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<User>.EntityToXml(userList);
            return xmlDoc;
        }
        [WebMethod]
        public XmlDocument GetProjectList()
        {
            ProjectLogics projectLogics = new ProjectLogics();
            IList<Project> projectList = projectLogics.GetProjectList();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Project>.EntityToXml(projectList);
            return xmlDoc;
        }
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
    }
}
