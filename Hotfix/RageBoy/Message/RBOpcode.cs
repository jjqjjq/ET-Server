
namespace ETHotfix.RageBoy
{
    public static class RBOpcode
    {
        //登录
        public const ushort C2R_RBLogin = 20001;
        public const ushort R2C_RBLogin = 20002;
        //注册
        public const ushort C2R_Register = 20003;
        public const ushort R2C_Register = 20004;
        //登录网关
        public const ushort C2G_RBLoginGate = 20005;
        public const ushort G2C_RBLoginGate = 20006;
        //获取用户信息
        public const ushort C2G_GetUserInfo = 20007;
        public const ushort G2C_GetUserInfo = 20008;
        //单局游戏结束
        public const ushort C2G_GameStart = 20009;
        public const ushort G2C_GameStart = 20010;
        //单局游戏结束
        public const ushort C2G_GameOver = 20011;
        public const ushort G2C_GameOver = 20012;
        //充值海钻
        public const ushort C2G_ChargeCrystal = 20013;
        public const ushort G2C_ChargeCrystal = 20014;
        //消费海钻
        public const ushort C2G_CostCrystal = 20015;
        public const ushort G2C_CostCrystal = 20016;
        //切换角色
        public const ushort C2G_ChangeRole = 20017;
        public const ushort G2C_ChangeRole = 20018;
        //解锁
        public const ushort C2G_Unlock = 20019;
        public const ushort G2C_Unlock = 20020;
        //更新图鉴
        public const ushort C2G_UpdateCollection = 20021;
        public const ushort G2C_UpdateCollection = 20022;
        //更新成就
        public const ushort C2G_UpdateAchievement = 20023;
        public const ushort G2C_UpdateAchievement = 20024;
        //心跳
        public const ushort C2G_HeartTick = 20025;
        public const ushort G2C_HeartTick = 20026;
        
        public const ushort UserInfo = 30001;
        public const ushort CollectionInfo = 30002;
        public const ushort AchievementInfo = 30003;
    }
}
