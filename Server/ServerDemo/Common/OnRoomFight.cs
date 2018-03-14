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
        /// 全部准备完成
        /// </summary>
        public const byte RDY = 1;

        /// <summary>
        /// 获取数据
        /// </summary>
        public const byte GetInfo = 2;

        /// <summary>
        /// 移动
        /// </summary>
        public const byte Walk = 3;

        /// <summary>
        /// 伤害
        /// </summary>
        public const byte Damage = 4;

        /// <summary>
        /// 游戏结束
        /// </summary>
        public const byte GameOver = 5;
    }
}
