using System;
namespace Model
{
    public class Project : BaseModel
    {

        public Project()
        {}

        /// <summary>
        /// 只用于列表的Project构造方法
        /// </summary>
        public Project(int projectId, string projectNo, string name, int engineerId,
            string engineerIdName, int stateCode, int statusCode,int testerId,string testerIdName)
        {
            this.ProjectId = projectId;
            this.ProjectNo = projectNo;
            this.Name = name;
            this.EngineerId = engineerId;
            this.EngineerIdName = engineerIdName;
            this.TesterId = testerId;
            this.TesterIdName = testerIdName;
            this.StatusCode = statusCode ;
            this.StateCode = stateCode;
        }
        public Project(int projectId, string projectNo, string name, int engineerId,
    string engineerIdName, int stateCode, int statusCode, int testerId, string testerIdName,string description)
        {
            this.ProjectId = projectId;
            this.ProjectNo = projectNo;
            this.Name = name;
            this.EngineerId = engineerId;
            this.EngineerIdName = engineerIdName;
            this.TesterId = testerId;
            this.TesterIdName = testerIdName;
            this.StatusCode = statusCode;
            this.StateCode = stateCode;
            this.Description = description;
        }

        public int? ProjectId { get; set; }
        public string ProjectNo { get; set; }
        public string Name { get; set; }
        public int? EngineerId { get; set; }
        public int? TesterId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public int? StatusCode { get; set; }
        public int? StateCode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        /*Join字段*/
        public string EngineerIdName { get; set; }
        public string TesterIdName { get; set; }
        public string CustomerIdName { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
    }
}
