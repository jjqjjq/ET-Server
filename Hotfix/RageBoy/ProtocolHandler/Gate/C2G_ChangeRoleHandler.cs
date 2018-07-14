using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_ChangeRoleHandler : AMRpcHandler<C2G_ChangeRole, G2C_ChangeRole>
	{
		protected override async void Run(Session session, C2G_ChangeRole message, Action<G2C_ChangeRole> reply)
		{
		    G2C_ChangeRole response = new G2C_ChangeRole();
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
			    //查询用户信息
			    DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                
			    DB_UserInfo userInfo = await dbProxyComponent.Query<DB_UserInfo>(user.UserID, false);
                //暂不验证，角色那里目前是成就验证
			    /*
                if (!userInfo.RoleDataList.Contains(message.DataId))
                {
                    response.Error = ProtocolErrorCode.ERR_DataNotExsit;
                    reply(response);
                    return;
                }*/
			    userInfo.CurrRoleDataId = message.DataId;
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