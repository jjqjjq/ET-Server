using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public static class ProtocolErrorCode
    {
        //20000以下抛异常
        public const int ERR_SignError = 300001;
        public const int ERR_Disconnect = 300002;
        public const int ERR_AccountAlreadyRegister = 300003;
        public const int ERR_LoginError = 300004;
        public const int ERR_IllegalCharacter = 300005;
        public const int ERR_ChargeCrystalFail = 300006;
        public const int ERR_CostCrystalFail = 300007;
        public const int ERR_InputParams = 300008;//请求参数错误
        public const int ERR_GameOverError = 300009;//涉嫌作弊！
        public const int ERR_DataNotExsit= 300010;//数据不存在
    }
}
