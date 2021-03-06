﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 区分传送数据时候的参数的类型
    /// </summary>
    public enum ParameterCode:byte
    {
        UserId,
        UserName,
        passWord,

        GameUser,
        CharacterId,

        ChatInfo,

        MatchRoom,
        MatchMuliti,

        RoomFight,

        PositionX,
        PositionY,
        PositionZ,

        RotationX,
        RotationY,
        RotationZ,
        RotationW,

        Animation,

        OnlineUserNameList,
        PlayerDataList
    }
}
