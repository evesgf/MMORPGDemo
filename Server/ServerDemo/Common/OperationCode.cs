namespace Common
{
    /// <summary>
    /// 操作的类型
    /// 区分请求和响应的类型
    /// </summary>
    public enum OperationCode:byte
    {
        Default,
        Login,
        Register,
        SyncPosition,
        SyncPlayer,

        Skill,
        Damage
    }
}
