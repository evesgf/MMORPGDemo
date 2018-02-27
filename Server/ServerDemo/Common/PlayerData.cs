using System;

namespace Common
{
    [Serializable]
    public class PlayerData
    {
        public Vector3Data pos { get; set; }
        public string userName { get; set; }
    }
}
