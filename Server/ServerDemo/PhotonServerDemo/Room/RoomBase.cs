﻿using Common;
using ExitGames.Threading;
using Photon.SocketServer;
using PhotonServerDemo.Threads;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonServerDemo.Room
{
    /// <summary>
    /// 房间基类
    /// </summary>
    public class RoomBase
    {
        /// <summary>
        /// 房间Id
        /// </summary>
        public int Id;

        /// <summary>
        /// 连接对象的集合
        /// </summary>
        public List<Client> ClientList;

        /// <summary>
        /// 房间的容纳量
        /// </summary>
        public int Count;

        /// <summary>
        /// 定时器
        /// </summary>
        public Timer timer;

        /// <summary>
        /// 定时任务的ID
        /// </summary>
        public Guid GUID;

        //玩家同步数据的队列
        public ConcurrentDictionary<int, PlayerData> dict_UserData = new ConcurrentDictionary<int, PlayerData>();

        //同步数据的线程
        public SyncPositionThread syncPositionThread = new SyncPositionThread();

        public RoomBase(int id, int count)
        {
            this.Id = id;
            this.Count = count;
            ClientList = new List<Client>();
            GUID = new Guid();
            timer = new Timer();
            timer.Start();
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="client"></param>
        protected bool EnterRoom(Client client)
        {
            if (ClientList.Contains(client))
            {
                return false;
            }
            ClientList.Add(client);
            return true;
        }

        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected bool LeaveRoom(Client client)
        {
            if (!ClientList.Contains(client))
            {
                return false;
            }
            ClientList.Remove(client);
            return true;
        }

        /// <summary>
        /// 开启定时任务
        /// </summary>
        /// <param name="utcTime"></param>
        /// <param name="callback"></param>
        public void StartSchedule(DateTime utcTime, Action callback)
        {
            this.GUID = timer.AddAction(utcTime, callback);
        }

        /// <summary>
        /// 广播给房间的所有用户
        /// </summary>
        /// <param name="value"></param>
        public void Broadcast(object value)
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.RoomFight, value);

            EventData ed = new EventData((byte)EventCode.RoomFight);
            ed.Parameters = data;
            foreach (var p in ClientList)
            {
                p.SendEvent(ed, new SendParameters());
            }
        }
    }
}
