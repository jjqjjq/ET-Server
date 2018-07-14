using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_GameOverHandler : AMRpcHandler<C2G_GameOver, G2C_GameOver>
	{
		protected override async void Run(Session session, C2G_GameOver message, Action<G2C_GameOver> reply)
		{
		    G2C_GameOver response = new G2C_GameOver();
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
			    if (message.CrystalReward<=0)
			    {
			        response.Error = ProtocolErrorCode.ERR_InputParams;
			        reply(response);
			        return;
			    }
			    DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
			    DB_UserInfo userInfo = await dbProxyComponent.Query<DB_UserInfo>(user.UserID, false);
			    long gameUseMin = (DateTime.UtcNow.Ticks - userInfo.LastGameStartTime)/1000/60; //花费分钟数
			    long average = message.CrystalReward / gameUseMin;
			    if (average > 20)//每分钟允许获得至多20个海钻
			    {
			        response.Error = ProtocolErrorCode.ERR_GameOverError;
			        reply(response);
			        return;
			    }
			    userInfo.Crystal += message.CrystalReward;
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