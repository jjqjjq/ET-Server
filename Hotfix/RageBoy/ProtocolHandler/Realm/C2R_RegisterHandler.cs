using System;
using System.Text.RegularExpressions;
using ETHotfix.RageBoy;
using ETModel;
using Model.RageBoy.DB;
using RageBoy;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_RegisterHandler : AMRpcHandler<C2R_Register, R2C_Register>
    {
        protected override async void Run(Session session, C2R_Register message, Action<R2C_Register> reply)
        {
            R2C_Register response = new R2C_Register();
            try
            {
                //验证账号格式
                if (!Regex.IsMatch(message.Account, "^[A-Za-z0-9]*$"))
                {
                    response.Error = ProtocolErrorCode.ERR_IllegalCharacter;
                    reply(response);
                    return;
                }
                
                //验证密码格式
                if (!Regex.IsMatch(message.Password, "^[A-Za-z0-9]*$"))
                {
                    response.Error = ProtocolErrorCode.ERR_IllegalCharacter;
                    reply(response);
                    return;
                }
                
                //数据库操作对象
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                //查询账号是否存在
                var  result = await dbProxy.Query<DB_Account>((p) => p.Account == message.Account);
                if (result.Count > 0)
                {
                    response.Error = ProtocolErrorCode.ERR_AccountAlreadyRegister;
                    reply(response);
                    return;
                }

                //新建账号
                DB_Account newAccount = ComponentFactory.CreateWithId<DB_Account>(IdGenerater.GenerateId());
                newAccount.Account = message.Account;
                newAccount.Password = message.Password;

                Log.Info($"注册新账号：{MongoHelper.ToJson(newAccount)}");

                //新建用户信息
                DB_UserInfo newUser = ComponentFactory.CreateWithId<DB_UserInfo>(newAccount.Id);
                newUser.Account = $"用户{message.Account}";
                newUser.RoleDataList.Add(1001);
                //保存到数据库
                await dbProxy.Save(newAccount);
                await dbProxy.Save(newUser, false);

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
