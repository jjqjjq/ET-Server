using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_CostCrystalHandler : AMRpcHandler<C2G_CostCrystal, G2C_CostCrystal>
	{
		protected override async void Run(Session session, C2G_CostCrystal message, Action<G2C_CostCrystal> reply)
		{
		    G2C_CostCrystal response = new G2C_CostCrystal();
			try
			{
			    User user = GateHelper.GetUserBySession(session);
			    //验证Session
			    if (user==null)
			    {
			        response.Error = ProtocolErrorCode.ERR_SignError;
			        reply(response);
			        return;
			    }

			    short crystal = getCost(message.Type, message.DataId);
			    if (crystal <= 0)
			    {
			        response.Error = ProtocolErrorCode.ERR_InputParams;
			        reply(response);
			        return;
			    }

			    DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
			    DB_UserInfo userInfo = await dbProxyComponent.Query<DB_UserInfo>(user.UserID, false);
			    if (userInfo.Crystal < crystal)
			    {
			        response.Error = ProtocolErrorCode.ERR_CostCrystalFail;
			        reply(response);
			        return;
			    }
			    userInfo.Crystal -= crystal;
			    await dbProxyComponent.Save(userInfo, false);
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}

	    private short getCost(short type, int dataId)
	    {
            return 15;
	    }
	}
}