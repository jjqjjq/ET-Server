using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_GetGameStartHandler : AMRpcHandler<C2G_GameStart, G2C_GameStart>
	{
		protected override async void Run(Session session, C2G_GameStart message, Action<G2C_GameStart> reply)
		{
		    G2C_GameStart response = new G2C_GameStart();
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
			    userInfo.LastGameStartTime = DateTime.UtcNow.Ticks;
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