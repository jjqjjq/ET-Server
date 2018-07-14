using System;
using System.Collections.Generic;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_UpdateCollectionHandler : AMRpcHandler<C2G_UpdateCollection, G2C_UpdateCollection>
	{
		protected override async void Run(Session session, C2G_UpdateCollection message, Action<G2C_UpdateCollection> reply)
		{
		    G2C_UpdateCollection response = new G2C_UpdateCollection();
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
			    bool change = false;
			    switch (message.Type)
			    {
			        case 1:
			            if (addToList(userInfo.MonsterList, message.DataId))
			            {
			                change = true;
			            }
			            break;
			        case 2:
			            if (addToList(userInfo.HadOwnItemList, message.DataId))
			            {
			                change = true;
			            }
			            break;
			        case 3:
			            if (addToList(userInfo.UnSeeOwnItemList, message.DataId))
			            {
			                change = true;
			            }
			            break;
			    }

			    if (change)
			    {
			        await dbProxyComponent.Save(userInfo, false);
			    }
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}

	    private bool addToList(List<int> list, int addOne)
	    {
	        if (!list.Contains(addOne))
	        {
                list.Add(addOne);
	            return true;
	        }

	        return false;
	    }
	}
}