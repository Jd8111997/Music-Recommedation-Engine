using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        BeatsEntities db = new BeatsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] == null)
            {
                Label1.Text = "Username";

            }
            if (Request.QueryString["user"] != null)
            {
                string username1 = Request.QueryString["user"];
                user us = db.users.Where(u => u.username == username1).FirstOrDefault();
                Label1.Text = us.security_ques;

            }


            }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] == null)
            {
               
                user us = db.users.Where(u => u.username == TextBox1.Text).FirstOrDefault();
                if (us == null)
                {
                    Response.Write("Invalid User");
                }
                if (us != null)
                {
                    Label1.Text = us.security_ques;
                    Response.Redirect("forgotpassword.aspx?user=" + us.username);

                }
            }
            if (Request.QueryString["user"] != null)
            {
                
                string user1 = Request.QueryString["user"];

                user us = db.users.Where(u => u.username == user1&&u.security_ans==TextBox1.Text).FirstOrDefault();
                string ans = us.security_ans;
                
                
                    if (us!=null)
                    {
                        FormsAuthentication.RedirectFromLoginPage(us.username, true);

                    }
                    if (us==null)
                    {
                        Response.Write("Not correct answer");
                    }
                
                
            }
            }
        }
    }
