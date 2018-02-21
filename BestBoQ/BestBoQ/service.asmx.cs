using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BestBoQ
{
    /// <summary>
    /// Summary description for service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class service : System.Web.Services.WebService
    {

        [WebMethod]
        public void checkUser(string username)
        {
            string sql = "SELECT * FROM [BESTBoQ].[dbo].[userinfo] WHERE [username] = '"+ username + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql);
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Write("Yes");
            }
            else
            {
                HttpContext.Current.Response.Write("No");
            }
            
        }
    }
}
