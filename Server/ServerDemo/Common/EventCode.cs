using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 区分服务器像客户端发送的事件的类型
    /// </summary>
    public enum EventCode:byte
    {
        NewPlayer,
        SyncPosition
    }
}
