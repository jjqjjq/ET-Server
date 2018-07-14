namespace ETModel.RageBoy
{
    [ObjectSystem]
    public class UserAwakeSystem : AwakeSystem<User,long>
    {
        public override void Awake(User self, long userID)
        {
            self.Awake(userID);
        }
    }

    /// <summary>
    /// 玩家对象
    /// </summary>
    public sealed class User : Entity
    {
        //用户ID（唯一）
        public long UserID { get; private set; }

        //Gate转发ActorID
        public long ActorID { get; set; }

        public void Awake(long userID)
        {
            this.UserID = userID;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
            this.ActorID = 0;
        }
    }
}