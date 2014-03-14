using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using VerifyCodeBase;

namespace VerifyCodeService
{

    public class VerifyCode : VerifyCodeOperate
    {
        public CodeCheckResult CheckVerifyCode(string Code, string Key, string GPS, string Validate)
        {
            CodeCheckResult Result = new CodeCheckResult();
            Key = Key.Replace(" ", "+");
            GPS = GPS.Replace(" ", "+");
            try
            {
                if (String.Compare(SimpleEncrypt.Md5(SimpleEncrypt.DecryptString(Key, "yzm"), SimpleEncrypt.DecryptString(GPS, "yzm")), Validate, true) != 0)
                {
                    Result.Message = "安全校验失败";
                    Result.IsPass = false;
                    return Result;
                }

                string realCode = "";
                bool re = Check.ValidateValue(SimpleEncrypt.DecryptString(Key, "yzm"), Code, ref realCode);
                if (re == false)
                {
                    Result.Message = "验证码输入错误";
                    Result.IsPass = false;
                }
                else
                    Result.IsPass = true;
                return Result;
            }
            catch (Exception ex)
            {
                Result.IsPass = false;
                Result.Message = "验证异常" + ex.Message;
            }
            return Result;
        }
    }
}
