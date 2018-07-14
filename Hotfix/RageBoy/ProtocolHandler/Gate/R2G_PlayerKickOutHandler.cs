﻿using System;
using System.Net;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_PlayerKickOutHandler : AMRpcHandler<R2G_PlayerKickOut, G2R_PlayerKickOut>
    {
        protected override async void Run(Session session, R2G_PlayerKickOut message, Action<G2R_PlayerKickOut> reply)
        {
            G2R_PlayerKickOut response = new G2R_PlayerKickOut();
            try
            {
                
                User user = Game.Scene.GetComponent<UserComponent>().Get(message.UserID);

                //服务端主动断开客户端连接
                long userSessionId = user.GetComponent<UnitGateComponent>().GateSessionActorId;
                Game.Scene.GetComponent<NetOuterComponent>().Remove(userSessionId);
                Log.Info($"将玩家{message.UserID}连接断开");

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
