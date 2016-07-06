using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class ContainerWarning:BaseModel
    {
        public ContainerWarning()
        {

        }
        public int ContainerId { get; set; }
        public int BigContainer { get; set; }
        public int SmallContainer { get; set; }
        public string BigMessage { get; set; }
        public string SmallMessage { get; set; }
    }
}
