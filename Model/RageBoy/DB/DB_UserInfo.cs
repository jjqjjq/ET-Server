using System;
using System.Collections.Generic;
using System.Text;
using ETModel;
using Model.RageBoy.DB;
using MongoDB.Bson.Serialization.Attributes;

namespace RageBoy
{
    /// <summary>
    /// 账号信息
    /// </summary>
    [BsonIgnoreExtraElements]
    public class DB_UserInfo:Entity
    {
        public string Account { get; set; }//用户名
        public long LastGameStartTime { get; set; }//最近一次开始游戏时间
        public int Crystal{ get; set; }//海钻
        public short CurrRoleDataId{ get; set; }//当前角色
        public List<short> RoleDataList = new List<short>();//已拥有角色
        public List<short> UnlockedTechList = new List<short>();//科技点
        public short UnlockPoint;//解锁点
        //图鉴
        public List<int> MonsterList = new List<int>();
        public List<int> HadOwnItemList = new List<int>();
        public List<int> UnSeeOwnItemList = new List<int>();
        //成就
        public List<DB_AchievementInfo> AchievementList = new List<DB_AchievementInfo>();
    }
}
