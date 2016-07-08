using System.Web.Services;
using Newtonsoft.Json;
using Logics;
using Model;

public partial class handle_UserHandler : System.Web.UI.Page
{
    /// <summary>
    /// 用户登录获得当前用户信息
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// 
    [WebMethod]
    public static string GetUserByRole(string rolename)
    {
        UserLogics us = new UserLogics();
        return JsonConvert.SerializeObject(us.GetUserByRole(rolename));
    }
  [WebMethod]
  public static string ChangeUserPwd(User user)
    {
        UserLogics us = new UserLogics();
        return JsonConvert.SerializeObject(us.ChangeUserPwd(user));
    }
    [WebMethod]
    public static string UpdateUser(User user)
    {
        UserLogics us = new UserLogics();
        return JsonConvert.SerializeObject(us.Update(user));
    }
  
    [WebMethod]
    public static string Login(User user)
    {
        UserLogics userLogics = new UserLogics();
        
        return JsonConvert.SerializeObject(userLogics.UserLogin(user.Username, user.Password));
    }
    [WebMethod]
    public static string GetUserCount()
    {
        UserLogics us = new UserLogics();
        return JsonConvert.SerializeObject(us.GetUserCount());
    }

    [WebMethod]
    public static string GetUserList()
    {
        UserLogics userLogics = new UserLogics();

        return JsonConvert.SerializeObject(userLogics.GetUserList());
    }

    [WebMethod]
    public static string GetUserList(int number)
    {
        UserLogics userLogics = new UserLogics();

        return JsonConvert.SerializeObject(userLogics.GetUserList(number));
    }

    [WebMethod]
    public static string CreateUser(User user)
    {
        UserLogics userLogics = new UserLogics();

        return JsonConvert.SerializeObject(userLogics.CreateUser(user));
    }
    [WebMethod]
    public static string GetUserByRoleId(int roleid)
    {
        UserLogics user = new UserLogics();
        return JsonConvert.SerializeObject(user.GetUserByRoleId(roleid));
    }

    [WebMethod]
    public static string GetUserById(int userId)
    {
        UserLogics userLogics = new UserLogics();

        return JsonConvert.SerializeObject(userLogics.GetModel(userId));
    }

    [WebMethod]
    public static string DeleteUser(int userid)
    {
        UserLogics userLogics = new UserLogics();

        return JsonConvert.SerializeObject(userLogics.DeleteUser(userid));
    }



    /*Project*/
    [WebMethod]
    public static string GetEngineerList()
    {
        //Engineer : 2
        //Tester : 3
        UserLogics userLogics = new UserLogics();
        return JsonConvert.SerializeObject(userLogics.GetUserListByRole(2));
    }

    [WebMethod]
    public static string GetTesterList()
    {
        //Engineer : 2
        //Tester : 3
        UserLogics userLogics = new UserLogics();
        return JsonConvert.SerializeObject(userLogics.GetUserListByRole(3));
    }
    [WebMethod]
    public static string GetUProjectCount(int systemuserId,int roleId)
    {
        ProjectLogics project = new ProjectLogics();
        return JsonConvert.SerializeObject(project.GetUProjectCount(systemuserId,roleId));
    }
    [WebMethod]
    public static string GetUprojectListByUser(int systemuserId,int roleId,int page)
    {
        ProjectLogics projectLogics = new ProjectLogics();
        return JsonConvert.SerializeObject(projectLogics.GetUProjectListByUser(systemuserId,roleId,page));
    }
    [WebMethod]
    public static string GetProjectListByUserId(int userId,int roleId)
    {
        ProjectLogics projectLogics = new ProjectLogics();
        return JsonConvert.SerializeObject(projectLogics.GetProjectListByUserId(userId,roleId));
    }

    [WebMethod]
    public static string CreateProject(Project project)
    {
        ProjectLogics projectLogics = new ProjectLogics();
        return JsonConvert.SerializeObject(projectLogics.CreateProject(project));
    }
    [WebMethod]
    public static string UpdateProjectStatusCode(int projectId)
    {
        ProjectLogics project = new ProjectLogics();
        return JsonConvert.SerializeObject(project.UpdateProjectStatusCode(projectId));
    }
    [WebMethod]
    public static string GetProjectListByStatusCode(int userId,int roleId,int statusCode)
    {
        ProjectLogics project = new ProjectLogics();
        return JsonConvert.SerializeObject(project.GetProjectListByStatusCode(userId, roleId, statusCode));
    }
    [WebMethod]
    public static string GetProject(int projectId)
    {
        ProjectLogics projectLogics = new ProjectLogics();
        return JsonConvert.SerializeObject(projectLogics.GetModel(projectId));
    }
    [WebMethod]
    public static string DeleteProject(int projectId)
    {
        ProjectLogics projectLogics = new ProjectLogics();
        return JsonConvert.SerializeObject(projectLogics.DeleteProject(projectId));
    }
    [WebMethod]
    public static string UpdateProject(Project project)
    {
        ProjectLogics pro = new ProjectLogics();
        return JsonConvert.SerializeObject(pro.Update(project));
    }
 

    //contact
    [WebMethod]
    public static string GetContactCount()
    {
        ContactLogics co = new ContactLogics();
        return JsonConvert.SerializeObject(co.GetContactCount());
    }
    [WebMethod]
    public static string Selectcontact(int number)
    {
        ContactLogics contact = new ContactLogics();
        return JsonConvert.SerializeObject(contact.Selectcontact(number));
    }
    [WebMethod]
    public static string DeleteContact(int contactid)
    {
        ContactLogics contact = new ContactLogics();
        return JsonConvert.SerializeObject(contact.Delete(contactid));
    }
    [WebMethod]
    public static string Selectcustomer()
    {
        CustomerLogics customer = new CustomerLogics();
        return JsonConvert.SerializeObject(customer.Selectcustomer());
    }
    [WebMethod]
    public static string Scontactid(int contactid)
    {
        ContactLogics contact = new ContactLogics();
        return JsonConvert.SerializeObject(contact.GetModel(contactid));
    }
    [WebMethod]
    public static string Incontact(Contact contact)
    {
        ContactLogics con = new ContactLogics();
        return JsonConvert.SerializeObject(con.Incontact(contact));
    }
    [WebMethod]
    public static string Scount()
    {
        CustomerLogics customer = new CustomerLogics();
        return JsonConvert.SerializeObject(customer.Scount());
    }


    [WebMethod]
    public static string GoPage(int number)
    {
        CustomerLogics customer = new CustomerLogics();
        return JsonConvert.SerializeObject(customer.GoPage(number));
    }
    [WebMethod]
    public static string Scustomerd(int customerid)
    {
        CustomerLogics customer = new CustomerLogics();
        return JsonConvert.SerializeObject(customer.GetModel(customerid));
    }
    [WebMethod]
    public static string Update(Customer customer)
    {
        CustomerLogics cus = new CustomerLogics();
        return JsonConvert.SerializeObject(cus.Update(customer));
    }
    [WebMethod]
    public static string ScustomerId()
    {
        CustomerLogics cus = new CustomerLogics();
        return JsonConvert.SerializeObject(cus.ScustomerId());
    }
    [WebMethod]
    public static string InCustomer(Customer customer)
    {
        CustomerLogics cus = new CustomerLogics();
        return JsonConvert.SerializeObject(cus.InCustomer(customer));
    }
    [WebMethod]
    public static string UpdateContact(Contact contact)
    {
        ContactLogics con = new ContactLogics();
        return JsonConvert.SerializeObject(con.UpdateContact(contact));
    }
    [WebMethod]
    public static string DeleteCustomer(int customerid)
    {
        CustomerLogics cus = new CustomerLogics();
        return JsonConvert.SerializeObject(cus.DeleteCustomer(customerid));

    }

    [WebMethod]
    public static string Scontact(int customerid)
    {
        ContactLogics cont = new ContactLogics();
        return JsonConvert.SerializeObject(cont.Scontact(customerid));
    }

    [WebMethod]
    public static string CreateShelf(Shelf shelf)
    {
        ShelfLogics sh = new ShelfLogics();
        return JsonConvert.SerializeObject(sh.CreateShelf(shelf));
    }
    [WebMethod]
    public static string GetShelfCount()
    {
        ShelfLogics sh = new ShelfLogics();
        return JsonConvert.SerializeObject(sh.GetShelfCount());
    }
    [WebMethod]
    public static string GetShelf(int number)
    {
        ShelfLogics sh = new ShelfLogics();
        return JsonConvert.SerializeObject(sh.GetShelf(number));

    }
    [WebMethod]
    public static string DeleteShelf(int shelfid)
    {
        ShelfLogics sh = new ShelfLogics();
        return JsonConvert.SerializeObject(sh.DeleteShelf(shelfid));
    }
    [WebMethod]
    public static string SelectShelf(int shelfid)
    {
        ShelfLogics sh = new ShelfLogics();
        return JsonConvert.SerializeObject(sh.GetModel(shelfid));
    }
    [WebMethod]
    public static string UpdateShelf(Shelf shelf)
    {
        ShelfLogics sh = new ShelfLogics();
        return JsonConvert.SerializeObject(sh.UpdateShelf(shelf));
    }

    [WebMethod]
    public static string GetProjectListByStatus()
    {
        ProjectLogics po = new ProjectLogics();
        return JsonConvert.SerializeObject(po.GetProjectListByStatus());
    }
    [WebMethod]
    public static string SelectRoom()
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.GetRoomList());
    }
    [WebMethod]
    public static string GetRoomList(int number)
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.GetRoomList(number));
    }
    [WebMethod]
    public static string GetRoomCount()
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.GetRoomCount());
    }
    [WebMethod]
    public static string GetRoom(int roomid)
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.GetModel(roomid));
    }
    [WebMethod]
    public static string CreateRoom(Room room)
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.CreateRoom(room));
    }
    [WebMethod]
    public static string UpdateRoom(Room room)
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.Update(room));
    }
    [WebMethod]
    public static string DeleteRoom(int roomid)
    {
        RoomLogics ro = new RoomLogics();
        return JsonConvert.SerializeObject(ro.DeleteRoom(roomid));
    }
    //task
    [WebMethod]
    public static string GetGoingProjectCount()
    {
        TaskLogics task = new TaskLogics();
       return JsonConvert.SerializeObject(task.GetGoingProjectCount());
    }
    [WebMethod]
    public static string GetFinishProjectCount()
    {
        TaskLogics task = new TaskLogics();
        return JsonConvert.SerializeObject(task.GetFinishProjectCount());
    }
    [WebMethod]
    public static string GetDelayProjectCount()
    {
        TaskLogics task = new TaskLogics();
        return JsonConvert.SerializeObject(task.GetDelayProjectCount());
    }
    [WebMethod]
    
    public static string GetDelayProjectList(int systemuserId,int roleId)
    {
        ProjectLogics project = new ProjectLogics();
        return JsonConvert.SerializeObject(project.GetDelayProjectList(systemuserId, roleId));
    }
    [WebMethod]
    public static string GetDelayTaskCount()
    {
        TaskLogics task = new TaskLogics();
        return JsonConvert.SerializeObject(task.GetDelayTaskCount());
    }
    [WebMethod]
    public static string GetTaskCount()
    {
        TaskLogics ta = new TaskLogics();
        return JsonConvert.SerializeObject(ta.GetTaskCount());
    }
    [WebMethod]
    public static string CreateTask(Task task)
    {
        TaskLogics ta = new TaskLogics();
        return JsonConvert.SerializeObject(ta.CreateTask(task));
    }
    [WebMethod]
    public static string GetTaskListByProjectId(int projectId)
    {
        TaskLogics task = new TaskLogics();
        return JsonConvert.SerializeObject(task.GetTaskListByProjectId(projectId));
    }
    [WebMethod]
    public static string SelectTask(int number,int systemuserId,int roleId)
    {
        TaskLogics ta = new TaskLogics();
        return JsonConvert.SerializeObject(ta.SelectTask(number,systemuserId,roleId));
    }
    [WebMethod]
    public static string DeleteTask(int taskid)
    {
        TaskLogics ta = new TaskLogics();
        return JsonConvert.SerializeObject(ta.DeleteTask(taskid));
    }
    [WebMethod]
    public static string SelectTaskId(int taskid)
    {
        TaskLogics ta = new TaskLogics();
        return JsonConvert.SerializeObject(ta.GetModel(taskid));
    }
    [WebMethod]
    public static string UpdateTask(Task task)
    {
        TaskLogics ta = new TaskLogics();
        return JsonConvert.SerializeObject(ta.UpdateTask(task));
    }
    [WebMethod]
    public static string GetPageRoleList(int number)
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.GetPageRoleList(number));
    }
    [WebMethod]
    public static string GetRoleList()
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.GetRoleList());
    }
    [WebMethod]
    public static string GetRoleCount()
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.GetRoleCount());
    }
    [WebMethod]
    public static string CreateRole(Role role)
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.CreateRole(role));
    }
    [WebMethod]
    public static string GetRoleType(int roleid)
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.GetRoleType(roleid));
    }
    [WebMethod]
    public static  string GetRole(int roleid)
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.GetRole(roleid));
    }
    [WebMethod]
    public static string UpdateRole(Role role)
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.Update(role));
    }
    [WebMethod]
    public static string DeleteRole(int roleid)
    {
        RoleLogics ro = new RoleLogics();
        return JsonConvert.SerializeObject(ro.DeleteRole(roleid));
    }
    [WebMethod]
    public static string GetModuleList()
    {
        ModuleLogics mo = new ModuleLogics();
        return JsonConvert.SerializeObject(mo.GetModuleList());
    }
    [WebMethod]
    public static string CreatePrivilege(Privilege privilege)
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.Add(privilege));
    }
    [WebMethod]
    public static string GetPrivilegeList()
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.GetPrivilegeList());
    }
    [WebMethod]
    public static string GetPrivilege(int id)
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.GetModel(id));
    }
    [WebMethod]
    public static string UpdatePrivilege(Privilege privilege)
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.Update(privilege));
    }
    [WebMethod]
    public static string DeletePrivilege(int privilegeid)
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.DeletePrivilege(privilegeid));
    }
    [WebMethod]
    public static string CreateRolePrivilege(Privilege privilege,int roleId)
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.CreateRolePrivilege(privilege, roleId));
    }
    [WebMethod]
    public static string GetRolePrivilegeList(int roleid)
    {
        PrivilegeLogics pr = new PrivilegeLogics();
        return JsonConvert.SerializeObject(pr.GetRolePrivilegeList(roleid));
    }
    [WebMethod]
    public static string GetContainerList()
    {
        ContainerLogics containerLogics = new ContainerLogics();
        return JsonConvert.SerializeObject(containerLogics.GetContainerList());
    }
    [WebMethod]
    public static bool CreateContainer(Container container)
    {
        ContainerLogics containerLogics = new ContainerLogics();
        return containerLogics.CreateContainer(container);
    }
    [WebMethod]
    public static string GContainerList(int taskid)
    {
        ContainerLogics containerLogics = new ContainerLogics();
        return JsonConvert.SerializeObject(containerLogics.GContainerList(taskid));
    }
    [WebMethod]
    public static bool DeleteContainer(int containerId)
    {
        ContainerLogics containerLogics = new ContainerLogics();
        return containerLogics.DeleteContainer(containerId);
    }
    [WebMethod]
    public static string GetContainer(int containerId)
    {
        ContainerLogics containerLogics = new ContainerLogics();
        return JsonConvert.SerializeObject(containerLogics.GetModel(containerId));
    }
 
    [WebMethod]
    public static string GetUseBigContainer()
    {
        ContainerLogics cont = new ContainerLogics();
        return JsonConvert.SerializeObject(cont.GetUseBigContainer());
    }
    [WebMethod]
    public static string GetBigContainer()
    {
        ContainerLogics con = new ContainerLogics();
        return JsonConvert.SerializeObject(con.GetBigContainer());
    }
    [WebMethod]
    public static string GetSmallContainer()
    {
        ContainerLogics con = new ContainerLogics();
        return JsonConvert.SerializeObject(con.GetSmallContainer());
    }
    [WebMethod]
    public static bool UpdateContainer(Container container)
    {
        ContainerLogics containerLogics = new ContainerLogics();
        return containerLogics.UpdateContainer(container);
    }
    [WebMethod]
    public static string GetContainerPageList(int pageNo)
    {
        ContainerLogics container = new ContainerLogics();
        return JsonConvert.SerializeObject(container.GetContainerPageList(pageNo));
    }
    [WebMethod]
    public static string GetContainerCount()
    {
        ContainerLogics con = new ContainerLogics();
        return JsonConvert.SerializeObject(con.GetContainerCount());
    }
    [WebMethod]
    public static string GetUseSmallContainer()
    {
        ContainerLogics con = new ContainerLogics();
        return JsonConvert.SerializeObject(con.GetUseSmallContainer());
    }
    [WebMethod]
    public static string GetUseContainerCount()
    {
        ContainerLogics con = new ContainerLogics();
        return JsonConvert.SerializeObject(con.GetUseContainerCount());
    }
    //sample
    [WebMethod]
    public static string GetSampleCount()
    {
        SampleLogics sample = new SampleLogics();
        return JsonConvert.SerializeObject(sample.GetSampleCount());
    }
    [WebMethod]
    public static string GetSamplePageList(int pageNo)
    {
        SampleLogics sample = new SampleLogics();
        return JsonConvert.SerializeObject(sample.GetSamplePageList(pageNo));
    }
  
    [WebMethod]
    public static string UpdateContainerId(int sampleId,int containerId)
    {
        SampleLogics sample = new SampleLogics();
        return JsonConvert.SerializeObject(sample.UpdateContainerId(sampleId,containerId));
    }

    [WebMethod]
    public static string GetSampleListByContainerId(int containerId)
    {
        SampleLogics sampleLogics = new SampleLogics();
        return JsonConvert.SerializeObject(sampleLogics.GetSampleListByContainerId(containerId));
    }
    [WebMethod]
    public static bool CreateSample(Sample sample)
    {
        SampleLogics sampleLogics = new SampleLogics();
        return sampleLogics.CreateSample(sample);
    }
    [WebMethod]
    public static string GetSample(int sampleId)
    {
        SampleLogics sampleLogics = new SampleLogics();
        return JsonConvert.SerializeObject(sampleLogics.GetModel(sampleId));
    }

    [WebMethod]
    public static bool UpdateSample(Sample sample)
    {
        SampleLogics sampleLogics = new SampleLogics();
        return sampleLogics.UpdateSample(sample);
    }

    [WebMethod]
    public static string GetSampleList()
    {
        SampleLogics sampleLogics = new SampleLogics();
        return JsonConvert.SerializeObject(sampleLogics.GetSampleList());
    }
    [WebMethod]
    public static bool DeleteSample(int sampleId)
    {
        SampleLogics sampleLogics = new SampleLogics();
        return sampleLogics.DeleteSample(sampleId);
    }
    [WebMethod]
    public static string GetSampleListByTaskId(int taskid)
    {
        SampleLogics sample = new SampleLogics();
        return JsonConvert.SerializeObject(sample.GetSampleListByTaskId(taskid));
    }
    [WebMethod]
    public static string ExceptSampleList(int taskid,int projectid)
    {
        SampleLogics sa = new SampleLogics();
        return JsonConvert.SerializeObject(sa.ExceptSampleList(taskid, projectid));
    }
    [WebMethod]
    public static string DeleteTaskSample(int sampleId)
    {
        TaskSampleLogics ta = new TaskSampleLogics();
        return JsonConvert.SerializeObject(ta.Delete(sampleId));
    }
   //projecttemplate
   [WebMethod]
   public static string CreateProjectTemplate(ProjectTemplate projecttemplate)
    {
        ProjectTemplateLogics pro = new ProjectTemplateLogics();
        return JsonConvert.SerializeObject(pro.Add(projecttemplate));
    }
    [WebMethod]
    public static string GetProjectTemplateList()
    {
        ProjectTemplateLogics pro = new ProjectTemplateLogics();
        return JsonConvert.SerializeObject(pro.GetProjectTemplateList());
    }
    [WebMethod]
    public static string DeleteProjectTemplate(int projectTemplateId)
    {
        ProjectTemplateLogics pro = new ProjectTemplateLogics();
        return JsonConvert.SerializeObject(pro.Delete(projectTemplateId));
    }
    [WebMethod]
    public static string GetProjectTemplate(int projectTemplateId)
    {
        ProjectTemplateLogics pro = new ProjectTemplateLogics();
        return JsonConvert.SerializeObject(pro.GetModel(projectTemplateId));
    }
    [WebMethod]
    public static string UpdateProjectTemplate(ProjectTemplate projecttemplate)
    {
        ProjectTemplateLogics pro = new ProjectTemplateLogics();
        return JsonConvert.SerializeObject(pro.Update(projecttemplate));
    }
  //tasksample
  [WebMethod]
  public static string CreateTaskSample(int taskid,int sampleid)
    {
        TaskSampleLogics ts = new TaskSampleLogics();
        return JsonConvert.SerializeObject(ts.CreateTaskSample(taskid, sampleid));
    }
    [WebMethod]
    public static string GetContainerWarning(int id)
    {
        ContainerWarningLogics co = new ContainerWarningLogics();
        return JsonConvert.SerializeObject(co.GetContainerWarning(id));
    }
    [WebMethod]
    public static string UpdateContainerWarning(ContainerWarning containerwarning)
    {
        ContainerWarningLogics co = new ContainerWarningLogics();
        return JsonConvert.SerializeObject(co.Update(containerwarning));
    }
}