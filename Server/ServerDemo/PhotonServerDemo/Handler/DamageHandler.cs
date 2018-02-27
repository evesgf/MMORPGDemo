using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace PhotonServerDemo.Handler
{
    class DamageHandler: HandlerBase
    {
        public DamageHandler()
        {
            opCode = OperationCode.Damage;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            //1.获取房间模型
            //2.判断是谁攻击 谁被攻击
            //攻击者的数据模型
            //被攻击者的数据模型
            //3.根据技能ID 判断出 是 普通攻击 还是 特殊技能
            //4.根据伤害表 根据技能id 获取ISKILL 调用damage 计算伤害
            //获取skill
            //计算出伤害
            //6.给房间内的客户端广播数据模型
            //结算
        }
    }
}
