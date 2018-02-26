using Common;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Net
{
    public abstract class EventBase : MonoBehaviour
    {
        public EventCode eventCode;

        public abstract void OnEvent(EventData eventData);

        public virtual void Start()
        {
            PhotonManager.Instance.AddEvent(this);
        }

        public virtual void OnDestroy()
        {
            PhotonManager.Instance.RemoveEvent(this);
        }
    }
}
