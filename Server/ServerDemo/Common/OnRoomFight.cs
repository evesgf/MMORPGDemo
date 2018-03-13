using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class OnRoomFight
    {
        /// <summary>
        /// 进入
        /// </summary>
        public const byte Enter = 0;

        /// <summary>
        /// 获取数据
        /// </summary>
        public const byte GetInfo = 1;

        /// <summary>
        /// 移动
        /// </summary>
        public const byte Walk = 2;

        /// <summary>
        /// 伤害
        /// </summary>
        public const byte Damage = 3;

        /// <summary>
        /// 游戏结束
        /// </summary>
        public const byte GameOver = 4;
    }
}
