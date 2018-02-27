using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class Vector3Data
    {
        //TODO:LitJson不支持float类型，待处理
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}
