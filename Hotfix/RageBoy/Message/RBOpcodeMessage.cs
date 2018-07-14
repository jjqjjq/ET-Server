using System.Collections.Generic;
using ETModel;
using ProtoBuf;

namespace ETHotfix.RageBoy
{
	[Message(RBOpcode.C2R_RBLogin)]
	[ProtoContract]
	public partial class C2R_RBLogin: IRequest
	{
		[ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
		[ProtoMember(1, IsRequired = true)]public string Account;
		[ProtoMember(2, IsRequired = true)]public string Password;
	}

	[Message(RBOpcode.R2C_RBLogin)]
	[ProtoContract]
	public partial class R2C_RBLogin: IResponse
	{
		[ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
		[ProtoMember(91, IsRequired = true)]public int Error { get; set; }
		[ProtoMember(92, IsRequired = true)]public string Message { get; set; }
		[ProtoMember(1, IsRequired = true)]public string Address;
		[ProtoMember(2, IsRequired = true)]public long Key;
	}
    
    [Message(RBOpcode.C2R_Register)]
    [ProtoContract]
    public partial class C2R_Register: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public string Account;
        [ProtoMember(2, IsRequired = true)]public string Password;
    }

    [Message(RBOpcode.R2C_Register)]
    [ProtoContract]
    public partial class R2C_Register: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }

	[Message(RBOpcode.C2G_RBLoginGate)]
	[ProtoContract]
	public partial class C2G_RBLoginGate: IRequest
	{
		[ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
		[ProtoMember(1, IsRequired = true)]public long Key;
	}

	[Message(RBOpcode.G2C_RBLoginGate)]
	[ProtoContract]
	public partial class G2C_RBLoginGate: IResponse
	{
		[ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
		[ProtoMember(91, IsRequired = true)]public int Error { get; set; }
		[ProtoMember(92, IsRequired = true)]public string Message { get; set; }
		[ProtoMember(1, IsRequired = true)]public long PlayerId;
	}

    [Message(RBOpcode.C2G_GetUserInfo)]
    [ProtoContract]
    public partial class C2G_GetUserInfo: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
    }

    [Message(RBOpcode.G2C_GetUserInfo)]
    [ProtoContract]
    public partial class G2C_GetUserInfo: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
        
        [ProtoMember(1, IsRequired = true)]public int Crystal{ get; set; }//海钻
        [ProtoMember(2, IsRequired = true)]public short CurrRoleDataId{ get; set; }//当前角色
        [ProtoMember(3, IsRequired = true)]public List<short> RoleDataList = new List<short>();//已拥有角色
        [ProtoMember(4, IsRequired = true)]public List<short> UnlockedTechList = new List<short>();//科技点
        [ProtoMember(5, IsRequired = true)]public short UnlockPoint;//解锁点
        [ProtoMember(6, IsRequired = true)]public List<int> MonsterList = new List<int>();
        [ProtoMember(7, IsRequired = true)]public List<int> HadOwnItemList = new List<int>();
        [ProtoMember(8, IsRequired = true)]public List<int> UnSeeOwnItemList = new List<int>();
        [ProtoMember(9, IsRequired = true)]public List<AchievementInfo> AchievementList = new List<AchievementInfo>();//成就
    }

    
    [Message(RBOpcode.C2G_GameStart)]
    [ProtoContract]
    public partial class C2G_GameStart: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
    }

    [Message(RBOpcode.G2C_GameStart)]
    [ProtoContract]
    public partial class G2C_GameStart: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }

    [Message(RBOpcode.C2G_GameOver)]
    [ProtoContract]
    public partial class C2G_GameOver: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public short CrystalReward{ get; set; }
    }

    [Message(RBOpcode.G2C_GameOver)]
    [ProtoContract]
    public partial class G2C_GameOver: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }

    
    [Message(RBOpcode.C2G_ChargeCrystal)]
    [ProtoContract]
    public partial class C2G_ChargeCrystal: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public short ChargeSessionKey { get; set; }//充值校验码  客户端发到第三方 第三方返回此校验码，同时也发给服务端，服务端匹对成功则充值

    }

    [Message(RBOpcode.G2C_ChargeCrystal)]
    [ProtoContract]
    public partial class G2C_ChargeCrystal: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
        
        [ProtoMember(1, IsRequired = true)]public int Crystal { get; set; }
    }
    
    [Message(RBOpcode.C2G_CostCrystal)]
    [ProtoContract]
    public partial class C2G_CostCrystal: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public short Type { get; set; }//花费类型  1:角色 2:科技点 3:老虎机
        [ProtoMember(2, IsRequired = true)]public int DataId { get; set; }//子ID   1:角色Id  2:科技点Id  3:0
    }

    [Message(RBOpcode.G2C_CostCrystal)]
    [ProtoContract]
    public partial class G2C_CostCrystal: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
        [ProtoMember(1, IsRequired = true)]public int Crystal { get; set; }
    }

    
    [Message(RBOpcode.C2G_ChangeRole)]
    [ProtoContract]
    public partial class C2G_ChangeRole: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public short DataId { get; set; }
    }

    [Message(RBOpcode.G2C_ChangeRole)]
    [ProtoContract]
    public partial class G2C_ChangeRole: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }
    
    [Message(RBOpcode.C2G_Unlock)]
    [ProtoContract]
    public partial class C2G_Unlock: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public short DataId { get; set; }
    }

    [Message(RBOpcode.G2C_Unlock)]
    [ProtoContract]
    public partial class G2C_Unlock: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }
    
    [Message(RBOpcode.C2G_UpdateCollection)]
    [ProtoContract]
    public partial class C2G_UpdateCollection: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public short Type { get; set; }
        [ProtoMember(2, IsRequired = true)]public int DataId { get; set; }
    }

    [Message(RBOpcode.G2C_UpdateCollection)]
    [ProtoContract]
    public partial class G2C_UpdateCollection: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }
    
    [Message(RBOpcode.C2G_UpdateAchievement)]
    [ProtoContract]
    public partial class C2G_UpdateAchievement: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(1, IsRequired = true)]public AchievementInfo Info { get; set; }
    }

    [Message(RBOpcode.G2C_UpdateAchievement)]
    [ProtoContract]
    public partial class G2C_UpdateAchievement: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }

    
    [Message(RBOpcode.C2G_HeartTick)]
    [ProtoContract]
    public partial class C2G_HeartTick: IRequest
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
    }

    [Message(RBOpcode.G2C_HeartTick)]
    [ProtoContract]
    public partial class G2C_HeartTick: IResponse
    {
        [ProtoMember(90, IsRequired = true)]public int RpcId { get; set; }
        [ProtoMember(91, IsRequired = true)]public int Error { get; set; }
        [ProtoMember(92, IsRequired = true)]public string Message { get; set; }
    }

    #region DataType
    
    
    [Message(RBOpcode.AchievementInfo)]
    [ProtoContract]
    public partial class AchievementInfo
    {
        [ProtoMember(1, IsRequired = true)] public short DataId { get; set; }
        [ProtoMember(2, IsRequired = true)] public short Progress { get; set; }
    }
    #endregion
}
