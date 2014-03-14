using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VerifyCodeBase
{
    public class Check
    {
        public static bool ValidateValue(string key, string code, ref string realCode)
        {
            DateTime now = DateTime.Now;
            string cvalue = "";
            object value = Couch.Get(key);
            if (value == null)
                return false;
            cvalue = SimpleEncrypt.DecryptString(value.ToString().Trim(), "cache");
            if (cvalue == "")
                return false;
            string[] valueList = cvalue.Split('|');
            int actionSecond = 0;
            if (valueList.Length > 2)
            {
                DateTime lasttime = DateTime.MinValue;
                if (DateTime.TryParse(valueList[2], out lasttime) && lasttime != DateTime.MinValue)
                    actionSecond = (int)(((TimeSpan)(now - lasttime)).TotalMilliseconds);
            }
            else
                return false;

            Couch.Remove(key);
            realCode = valueList[0];

            if (String.Compare(code, valueList[0], true) == 0)
                return true;
            else
                return false;
        }
    }
}
