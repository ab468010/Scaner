using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Services;
using Logics;
using Model;
using System.Xml;

namespace SystemService
{
    /// <summary>
    /// UserService 的摘要说明
    /// </summary>
    [WebService(Namespace = "Users")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class UserService : System.Web.Services.WebService
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
        public XmlDocument GetProjectList(int userId)
        {
            ProjectLogics projectLogics = new ProjectLogics();
            IList<Project> projectList = projectLogics.GetProjectListByUser(userId);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = Xml<Project>.EntityToXml(projectList);
            return xmlDoc;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


    }
}
