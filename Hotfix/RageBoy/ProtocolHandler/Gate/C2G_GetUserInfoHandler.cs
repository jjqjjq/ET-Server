using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_GetUserInfoHandler : AMRpcHandler<C2G_GetUserInfo, G2C_GetUserInfo>
	{
		protected override async void Run(Session session, C2G_GetUserInfo message, Action<G2C_GetUserInfo> reply)
		{
		    G2C_GetUserInfo response = new G2C_GetUserInfo();
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
			    response.Crystal = userInfo.Crystal;
			    response.CurrRoleDataId = userInfo.CurrRoleDataId;
			    response.RoleDataList = userInfo.RoleDataList;
			    response.UnlockedTechList = userInfo.UnlockedTechList;
			    response.UnlockPoint = userInfo.UnlockPoint;
			    response.MonsterList = userInfo.MonsterList;
			    response.HadOwnItemList = userInfo.HadOwnItemList;
			    response.UnSeeOwnItemList = userInfo.UnSeeOwnItemList;
			    reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}