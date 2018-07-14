using System;
using System.Collections.Generic;
using System.Text;
using ETModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Model.RageBoy.DB
{
    [BsonIgnoreExtraElements]
    public class DB_AchievementInfo
    {
         public short DataId { get; set; }
         public short Progress { get; set; }
    }
}
