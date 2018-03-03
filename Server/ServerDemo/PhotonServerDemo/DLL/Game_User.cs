using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonServerDemo.DLL
{
    /// <summary>
    /// 用户在游戏里的玩家数据
    /// </summary>
    public class Game_User
    {
        public int Id { get; set; }

        public Sys_User User { get; set; }

        public string NickName { get; set; }
        public string HeadImgPath { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 当级经验
        /// </summary>
        public double EXP { get; set; }
        /// <summary>
        /// 玩家拥有的角色
        /// </summary>
        public ICollection<Game_Character> HasCharacters { get; set; }
        /// <summary>
        /// 当前选择的主角色
        /// </summary>
        public Game_Character Game_Character { get; set; }
    }
}
