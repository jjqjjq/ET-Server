using System.Collections.Generic;
using System.Linq;
using ETModel.RageBoy;

namespace ETModel.RageBoy
{
	[ObjectSystem]
	public class UserComponentSystem : AwakeSystem<UserComponent>
	{
		public override void Awake(UserComponent self)
		{
			self.Awake();
		}
	}
	
	public class UserComponent : Component
	{
		public static UserComponent Instance { get; private set; }

		public User MyPlayer;
		
		private readonly Dictionary<long, User> idUsers = new Dictionary<long, User>();

		public void Awake()
		{
			Instance = this;
		}
		
		public void Add(User user)
		{
			this.idUsers.Add(user.UserID, user);
		}

		public User Get(long userId)
		{
			this.idUsers.TryGetValue(userId, out User gamer);
			return gamer;
		}

		public void Remove(long userId)
		{
			this.idUsers.Remove(userId);
		}

		public int Count
		{
			get
			{
				return this.idUsers.Count;
			}
		}

		public User[] GetAll()
		{
			return this.idUsers.Values.ToArray();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			foreach (User user in this.idUsers.Values)
			{
				user.Dispose();
			}

			Instance = null;
		}
	}
}