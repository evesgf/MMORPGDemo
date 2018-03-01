using System;

namespace Common
{
    [Serializable]
    public class PlayerData
    {
        public Vector3Data pos { get; set; }
        public int userId { get; set; }
    }
}
