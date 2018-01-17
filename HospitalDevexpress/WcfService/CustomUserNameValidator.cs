using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfService
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }
            if (!(userName == "test1" && password == "1tset") && !(userName == "test2" && password == "2tset"))
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}