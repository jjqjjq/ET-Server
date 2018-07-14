using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace Model.RageBoy.Component.Gate.Session
{
    [ObjectSystem]
    public class HeartTickComponentUpdateSystem : UpdateSystem<HeartTickComponent>
    {
        public override void Update(HeartTickComponent self)
        {
            self.Update();
        }
    }
    public class HeartTickComponent:ETModel.Component
    {/// <summary>
        /// 更新间隔
        /// </summary>
        public long UpdateInterval = 5;
        /// <summary>
        /// 超出时间
        /// </summary>
        /// <remarks>如果跟客户端连接时间间隔大于在服务器上删除该Session</remarks>
        public long OutInterval = 10;
        /// <summary>
        /// 记录时间 
        ///</summary>
        private long _recordDeltaTime = 0;
        /// <summary>
        /// 当前Session连接时间
        /// </summary>
        public long CurrentTime = 0; 

        public void Update()
        {
            long currSecond = TimeHelper.ClientNowSeconds();

            long passTime = currSecond - _recordDeltaTime;
            // 如果没有到达发包时间、直接返回
            if (!(passTime > UpdateInterval) || CurrentTime == 0) return;
            // 记录当前时间
            this._recordDeltaTime =currSecond;
            if ((currSecond - CurrentTime) > OutInterval)
            {
                // 移除Session
                Game.Scene.GetComponent<NetOuterComponent>().Remove(this.Entity.Id);
                Game.Scene.GetComponent<NetInnerComponent>().Remove(this.Entity.Id);
            }
        }
    }
}
