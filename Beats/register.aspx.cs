using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class egister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Request.QueryString["res"]);
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BeatsEntities db = new BeatsEntities();
            string msg = "Please try another name";
                user u2 = db.users.Where(u1 => u1.username == TextBox1.Text).FirstOrDefault();
                admin ad = db.admins.Where(a => a.username == TextBox1.Text).FirstOrDefault();
                if (u2 != null || ad != null)
                {
                    Response.Redirect("register.aspx?res="+msg);
                }
               
                user u = new user();
                u.username = TextBox1.Text;
                u.password = TextBox2.Text;
                u.security_ques = TextBox4.Text;
                u.security_ans = TextBox5.Text;
                db.users.Add(u);
                db.SaveChanges();

                Response.Redirect("index.aspx?Login="+true);
            }
        }
    }
