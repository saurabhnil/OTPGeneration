using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OTPResetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPResetAPIController : ControllerBase
    {
        //private readonly ILogger<OTPResetAPIController> _logger;

        //public OTPResetAPIController(ILogger<OTPResetAPIController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        [Route("[action]")]
        public int GetIntOtp()
        {
            //OTP logic start
            Random rand = new Random();
            // 6 digits number returned.
            int p = rand.Next(100000, 999999);

            string cus_mob = "+919545527332";
            #region "send sms"
            string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=test@gmail.com:3698521478&senderID=TEST SMS&receipientno=";
            string bal_strUrl = "&msgtxt=Bill Amount:'" + "1003" + "' and Balance Amount:'" + "15000" + "' and Discount:'" + "27" + "'&state=4";
            string new_strUrl = strUrl + cus_mob + bal_strUrl;
            WebRequest request = HttpWebRequest.Create(new_strUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();

            //using (var wb = new WebClient())
            //{
            //    byte[] response1 = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
            //    {
            //    {"apikey" , "9777ae21b16f5a6febc8b9f4407e04b994fb7293f56f110a"},
            //    {"numbers" , "919545527332"},
            //    {"message" , "message to send as sms"},
            //    {"sender" , "TXTLCL"}
            //    });
            //    string result = System.Text.Encoding.UTF8.GetString(response1);
            //    //return result;
            //}

            
            #endregion

            return p;
        }

        [HttpGet]
        [Route("[action]/{empid}")]
        public string GetStrOtp(string empid)
        {
            string p = string.Empty;
            if (empid == "1001")
            {
                p = Convert.ToString(this.GetIntOtp());
                //return "Returning OTP for emp id " + empid +": " + p;
                return "Returning OTP for emp id " + empid + ": " + p;
            }
            return "Invalid employee";
        }

        public Boolean validateRequest(string input)
        {
            var empid = "1001"; //session value
            var genotp = "999999"; //otp retrieved from database 

            if (!String.IsNullOrEmpty(input))
            {
                string[] inReq = input.Split("#");
                if(inReq[0] == empid && inReq[1] == genotp)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
