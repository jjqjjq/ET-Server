using ETModel;
using System;
using ETHotfix.RageBoy;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_RBGetLoginKeyHandler : AMRpcHandler<R2G_RBGetLoginKey, G2R_RBGetLoginKey>
    {
        protected override void Run(Session session, R2G_RBGetLoginKey message, Action<G2R_RBGetLoginKey> reply)
        {
            G2R_RBGetLoginKey response = new G2R_RBGetLoginKey();
            try
            {
                long key = RandomHelper.RandInt64();
                Game.Scene.GetComponent<GateSessionKeyComponent>().Add(key, message.userId);
                response.Key = key;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
