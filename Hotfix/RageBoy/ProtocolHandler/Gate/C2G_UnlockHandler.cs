using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_UnlockHandler : AMRpcHandler<C2G_Unlock, G2C_Unlock>
	{
		protected override async void Run(Session session, C2G_Unlock message, Action<G2C_Unlock> reply)
		{
		    G2C_Unlock response = new G2C_Unlock();
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
                
			    DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
			    DB_UserInfo userInfo = await dbProxyComponent.Query<DB_UserInfo>(user.UserID, false);
			    userInfo.UnlockPoint = message.DataId;
			    await dbProxyComponent.Save(userInfo, false);
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}