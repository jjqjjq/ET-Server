using System;
using ETHotfix.RageBoy;
using ETModel;
using RageBoy;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_RBLoginHandler : AMRpcHandler<C2R_RBLogin, R2C_RBLogin>
	{
		protected override async void Run(Session session, C2R_RBLogin message, Action<R2C_RBLogin> reply)
		{
            
		    R2C_RBLogin response = new R2C_RBLogin();

		    try
		    {
		        //数据库操作对象
		        DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

		        Log.Info($"登录请求：{{Account:'{message.Account}',Password:'{message.Password}'}}");
		        //验证账号密码是否正确
		        var result = await dbProxy.Query<DB_Account>((p) => p.Account == message.Account && p.Password==message.Password);
		        if (result.Count == 0)
		        {
		            response.Error = ProtocolErrorCode.ERR_LoginError;
		            reply(response);
		            return;
		        }

		        DB_Account account = result[0] as DB_Account;
		        Log.Info($"账号登录成功{MongoHelper.ToJson(account)}");

		        //将已在线玩家踢下线
		        await RealmHelper.KickOutPlayer(account.Id);

		        //随机分配网关服务器
		        StartConfig gateConfig = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
		        Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(gateConfig.GetComponent<InnerConfig>().IPEndPoint);

		        //请求登录Gate服务器密匙
		        G2R_RBGetLoginKey getLoginKey = await gateSession.Call(new R2G_RBGetLoginKey() { userId = account.Id }) as G2R_RBGetLoginKey;

		        response.Key = getLoginKey.Key;
		        response.Address = gateConfig.GetComponent<OuterConfig>().IPEndPoint2.ToString();
		        reply(response);
		    }
		    catch (Exception e)
		    {
		        ReplyError(response, e, reply);
		    }
		}
	}
}