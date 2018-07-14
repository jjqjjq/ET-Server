using ETModel;
using ETModel.RageBoy;

namespace ETHotfix
{
    public static class GateHelper
    {
        /// <summary>
        /// 验证Session是否合法
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static User GetUserBySession(Session session)
        {
            SessionUserComponent sessionUser = session.GetComponent<SessionUserComponent>();
            if (sessionUser == null) return null;
            User user = Game.Scene.GetComponent<UserComponent>().Get(sessionUser.User.UserID);
            return user;
        }

        public static bool CheckSession(Session session)
        {
            User user = GetUserBySession(session);
            return user != null;
        }

    }
}
