using ID.Biz.Contract;
using ID.Common;
using ID.ValueObjects;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ID.Biz.Implementation
{
    public class EmailBizManager : IEmailBizManager
    {


        public bool SendEmailToAdmin(EmailVO objEmail)
        {
            string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string AdminName = ConfigurationManager.AppSettings["AdminName"].ToString();
            string ApiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();
            string ApiDomain = ConfigurationManager.AppSettings["ApiDomain"].ToString();
            string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
            string FromName = ConfigurationManager.AppSettings["FromName"].ToString();


            objEmail.FromName = FromName;
            objEmail.FromEmail = FromEmail;
            objEmail.Body = "Name : " + objEmail.UserName + " Email : " + objEmail.UserEmail + " Message : " + objEmail.Body;
            objEmail.UserEmail = AdminEmail;
            objEmail.UserName = AdminName;

            switch (objEmail.ContactFor)
            {
                case ContactForEnum.General:
                    objEmail.Subject = "You have new contact.";

                    break;
                case ContactForEnum.SurveyRequest:
                    objEmail.Subject = "You have new survey request.";
                    break;
                case ContactForEnum.Adverstisement:
                    objEmail.Subject = "You have new advertise request.";
                    break;
            }

            IRestResponse res = SendSimpleMessage(objEmail, ApiKey, ApiDomain);
            return true;
        }


        private IRestResponse SendSimpleMessage(EmailVO objEmail, String ApiKey, String ApiDomain)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                   new HttpBasicAuthenticator("api", ApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", ApiDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", objEmail.From);
            request.AddParameter("to", objEmail.To);
            request.AddParameter("subject", objEmail.Subject);
            request.AddParameter("text", objEmail.Body);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
