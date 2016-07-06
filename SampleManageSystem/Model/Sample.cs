using System;
namespace Model
{
    public class  Sample: BaseModel
    {

        public Sample()
        { }

        public int SampleId { get; set; }
        public string Name { get; set; }
        public int? ShelfId { get; set; }
        public int? ContainerId { get; set; }
        public string SampleCode { get; set; }
        public int? ProjectId { get; set; }
        public int? SampleClass { get; set; }
        public string Description { get; set; }

        /*Join*/
        public string ShelfIdName { get; set; }
        public string ContainerIdName { get; set; }
        public string ProjectIdName { get; set; }
    }
}
