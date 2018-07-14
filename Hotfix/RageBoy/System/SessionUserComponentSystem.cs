using ETModel;
using System.Net;
using ETHotfix.RageBoy;
using ETModel.RageBoy;

namespace ETHotfix
{
    [ObjectSystem]
    public class SessionUserComponentDestroySystem : DestroySystem<SessionUserComponent>
    {
        public override async void Destroy(SessionUserComponent self)
        {
            try
            {
                //释放User对象时将User对象从管理组件中移除
                Game.Scene.GetComponent<UserComponent>()?.Remove(self.User.UserID);

                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();

                //向登录服务器发送玩家下线消息
                IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
                await realmSession.Call(new G2R_PlayerOffline() { UserID = self.User.UserID });

                self.User.Dispose();
                self.User = null;
            }
            catch (System.Exception e)
            {
                Log.Trace(e.ToString());
            }
        }
    }
}
