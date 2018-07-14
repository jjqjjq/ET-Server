using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_ChargeCrystalHandler : AMRpcHandler<C2G_ChargeCrystal, G2C_ChargeCrystal>
	{
		protected override async void Run(Session session, C2G_ChargeCrystal message, Action<G2C_ChargeCrystal> reply)
		{
		    G2C_ChargeCrystal response = new G2C_ChargeCrystal();
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

			    short crystal = getChargeVal(message.ChargeSessionKey);
			    if (crystal <= 0)
			    {
			        response.Error = ProtocolErrorCode.ERR_ChargeCrystalFail;
			        reply(response);
			        return;
			    }
                
			    DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                
			    DB_UserInfo userInfo = await dbProxyComponent.Query<DB_UserInfo>(user.UserID, false);
			    userInfo.Crystal += crystal;
			    await dbProxyComponent.Save(userInfo, false);
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}

	    private short getChargeVal(short key)
	    {
	        return 10;
	    }
	}
}