using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using Model.RageBoy.DB;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_UpdateAchievementHandler : AMRpcHandler<C2G_UpdateAchievement, G2C_UpdateAchievement>
	{
		protected override async void Run(Session session, C2G_UpdateAchievement message, Action<G2C_UpdateAchievement> reply)
		{
		    G2C_UpdateAchievement response = new G2C_UpdateAchievement();
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

			    if (message.Info.Progress == 0)
			    {
			        response.Error = ProtocolErrorCode.ERR_InputParams;
			        reply(response);
			        return;
			    }
                
			    DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
			    DB_UserInfo userInfo = await dbProxyComponent.Query<DB_UserInfo>(user.UserID, false);
			    UpdateAchievement(userInfo, message.Info);
			    await dbProxyComponent.Save(userInfo, false);
			    reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}

	    private void UpdateAchievement(DB_UserInfo userInfo, AchievementInfo info)
	    {
	        for (int i = 0; i < userInfo.AchievementList.Count; i++)
	        {
	            DB_AchievementInfo dbAchievementInfo = userInfo.AchievementList[i];
	            if (dbAchievementInfo.DataId == info.DataId)
	            {
	                dbAchievementInfo.Progress = info.Progress;
                    return;
	            }
	        }
	        userInfo.AchievementList.Add(new DB_AchievementInfo(){DataId = info.DataId, Progress = info.Progress});
	    }
	}
}