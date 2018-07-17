using System;
using System.Net;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_RBLoginGateHandler : AMRpcHandler<C2G_RBLoginGate, G2C_RBLoginGate>
	{
		protected override async void Run(Session session, C2G_RBLoginGate message, Action<G2C_RBLoginGate> reply)
		{
		    G2C_RBLoginGate response = new G2C_RBLoginGate();
			try
			{
			    Log.Debug("[C2G_LoginGate]key:"+message.Key);
			    GateSessionKeyComponent gateSessionKeyComponent = Game.Scene.GetComponent<GateSessionKeyComponent>();
				long userId = gateSessionKeyComponent.Get(message.Key);
				if (userId == 0)
				{
					response.Error = ErrorCode.ERR_ConnectGateKeyError;
					reply(response);
					return;
				}
                
			    //Key过期
			    gateSessionKeyComponent.Remove(message.Key);

                
			    //创建User对象
				User user = ComponentFactory.Create<User, long>(userId);
			    user.AddComponent<UnitGateComponent, long>(session.Id);
			    Game.Scene.GetComponent<UserComponent>().Add(user);
			    await user.AddComponent<MailBoxComponent>().AddLocation();
                
			    //添加User对象关联到Session上
				session.AddComponent<SessionUserComponent>().User = user;
			    //添加消息转发组件
				await session.AddComponent<MailBoxComponent, string>(ActorType.GateSession).AddLocation();
                
			    //向登录服务器发送玩家上线消息
			    StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
			    IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
			    Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
			    await realmSession.Call(new G2R_PlayerOnline() { UserID = userId, GateAppID = config.StartConfig.AppId });

				response.PlayerId = user.Id;
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}