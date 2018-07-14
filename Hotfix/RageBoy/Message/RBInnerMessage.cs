using ETModel;
using ProtoBuf;

namespace ETHotfix.RageBoy
{
    #region Gate-Realm

    [Message(RBInnerOpcode.G2R_PlayerOnline)]
    [ProtoContract]
    public partial class G2R_PlayerOnline: IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long UserID;

        [ProtoMember(2, IsRequired = true)]
        public int GateAppID;

    }

    [Message(RBInnerOpcode.R2G_PlayerOnline)]
    [ProtoContract]
    public partial class R2G_PlayerOnline: IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

    }

    [Message(RBInnerOpcode.G2R_PlayerOffline)]
    [ProtoContract]
    public partial class G2R_PlayerOffline: IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long UserID;

    }

    [Message(RBInnerOpcode.R2G_PlayerOffline)]
    [ProtoContract]
    public partial class R2G_PlayerOffline: IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

    }

    #endregion
    
    #region Realm-Gate

    [Message(RBInnerOpcode.R2G_RBGetLoginKey)]
    [ProtoContract]
    public partial class R2G_RBGetLoginKey: IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long userId { get; set; }

    }

    [Message(RBInnerOpcode.G2R_RBGetLoginKey)]
    [ProtoContract]
    public partial class G2R_RBGetLoginKey: IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long Key;

    }

    [Message(RBInnerOpcode.R2G_PlayerKickOut)]
    [ProtoContract]
    public partial class R2G_PlayerKickOut: IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public long UserID;

    }

    [Message(RBInnerOpcode.G2R_PlayerKickOut)]
    [ProtoContract]
    public partial class G2R_PlayerKickOut: IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

    }

    #endregion
}
