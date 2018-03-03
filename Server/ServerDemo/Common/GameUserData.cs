using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class GameUserData
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public string NickName { get; set; }
        public string HeadImgPath { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 当级经验
        /// </summary>
        public double EXP { get; set; }
    }
}
