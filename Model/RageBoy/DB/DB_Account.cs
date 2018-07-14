using System.Collections.Generic;
using ETModel;
using MongoDB.Bson.Serialization.Attributes;

namespace RageBoy
{
    /// <summary>
    /// 账号信息
    /// </summary>
    [BsonIgnoreExtraElements]
    public class DB_Account:Entity
    {
        //用户名
        public string Account { get; set; }

        //密码
        public string Password { get; set; }
        
    }
}
