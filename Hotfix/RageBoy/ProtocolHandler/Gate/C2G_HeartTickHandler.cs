using System;
using ETHotfix.RageBoy;
using ETModel;
using ETModel.RageBoy;
using Model.RageBoy.Component.Gate.Session;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_HeartTickHandler : AMRpcHandler<C2G_HeartTick, G2C_HeartTick>
	{
		protected override void Run(Session session, C2G_HeartTick message, Action<G2C_HeartTick> reply)
		{
		    if (session.GetComponent<HeartTickComponent>() != null)
		    {
		        session.GetComponent<HeartTickComponent>().CurrentTime = TimeHelper.ClientNowSeconds();
		    }
		    G2C_HeartTick response = new G2C_HeartTick();
		    reply(response);
		}
	}
}