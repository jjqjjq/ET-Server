﻿using System;
using ETModel;
using Model.RageBoy.Component.Gate.Session;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class R2G_GetLoginKeyHandler : AMRpcHandler<R2G_GetLoginKey, G2R_GetLoginKey>
	{
		protected override void Run(Session session, R2G_GetLoginKey message, Action<G2R_GetLoginKey> reply)
		{
			G2R_GetLoginKey response = new G2R_GetLoginKey();
			try
			{
				long key = RandomHelper.RandInt64();
				Game.Scene.GetComponent<GateSessionKeyComponent>().Add(key, message.userId);
				response.Key = key;

			    session.AddComponent<HeartTickComponent>().CurrentTime = TimeHelper.ClientNowSeconds();

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}