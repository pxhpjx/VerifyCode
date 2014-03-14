using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VerifyCodeService
{
    [ServiceContract]
    public interface VerifyCodeOperate
    {
        [OperationContract]
        CodeCheckResult CheckVerifyCode(string Code, string Key, string GPS, string Validate);
    }


    [DataContract]
    public class CodeCheckResult
    {
        [DataMember]
        public bool IsPass { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
