using Common;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Net
{
    public abstract class RequestBase: MonoBehaviour
    {
        public OperationCode opCode;

        public abstract void DefaultRequest();
        public abstract void OnOperationResponse(OperationResponse operationResponse);

        public virtual void Start()
        {
            PhotonManager.Instance.AddReuqest(this);
        }

        public virtual void OnDestroy()
        {
            PhotonManager.Instance.RemoveRequest(this);
        }
    }
}
