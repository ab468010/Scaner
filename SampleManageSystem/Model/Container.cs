using System;
namespace Model
{
    public class  Container: BaseModel
    {

        public Container()
        { }
        public Container(int containerId, string name, string size, string containerCode,int statusCode,int projectId)
        {
            this.ContainerId = containerId;
            this.Name = name;
            this.Size = size;
            this.ContainerCode = containerCode;
            this.StatusCode = statusCode;
            this.ProjectId = projectId;
        }
        public int ContainerId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string ContainerCode { get; set; }
        public int StatusCode { get; set; }
        public int? ProjectId { get; set; }

        public string Description {get;set;}
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        /*Join*/
        public string ProjectIdName { get; set; }
        public int ProjectStatusCode { get; set; }
    }
}
