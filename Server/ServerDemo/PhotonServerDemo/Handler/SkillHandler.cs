using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;

namespace PhotonServerDemo.Handler
{
    class SkillHandler: HandlerBase
    {
        public SkillHandler()
        {
            opCode = OperationCode.Skill;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            //先判断 是不是普通攻击
            //参数：1、攻击者ID，2、被攻击者ID
            //是技能 从技能配置表里面通过技能ID获取到技能信息 然后再广播
            //技能类型
        }
    }
}
