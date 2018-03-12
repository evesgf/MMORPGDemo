using System;

namespace Common
{
    [Serializable]
    public class PlayerData
    {
        public int userId { get; set; }
        public int gameUserId { get; set; }

        public int characterId { get; set; }

        public Vector3Data pos { get; set; }
    }
}
