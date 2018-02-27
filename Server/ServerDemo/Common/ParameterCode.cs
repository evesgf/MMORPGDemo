using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 区分传送数据时候的参数的类型
    /// </summary>
    public enum ParameterCode:byte
    {
        UserName,
        passWord,
        PositionX,
        PositionY,
        PositionZ,
        OnlineUserNameList,
        PlayerDataList
    }
}
