using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class morechannels : System.Web.UI.Page
    {
        BeatsEntities db = new BeatsEntities();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("index.aspx");
            }
            
            BeatsEntities db = new BeatsEntities();
            GridView1.Visible = true;
            GridView2.Visible = false;
            if (Request.QueryString["mode"] == null)
            {
                var res=(from ch in db.channels
                        select new { Name = ch.channel_name, ID = ch.channel_id, Description = ch.channel_description , Followers = ch.follow }).ToList();
                GridView1.DataSource = res;
                GridView1.DataBind();
            }
            if (Request.QueryString["mode"] != null)
            {
                string query = Request.QueryString["mode"];
                var res = (from ch in db.channels
                           where ch.channel_name.Contains(query)
                           select new { Name = ch.channel_name, ID = ch.channel_id, Description = ch.channel_description ,Followers=ch.follow}).ToList();
                GridView2.DataSource = res;
                GridView2.DataBind();
                GridView2.Visible = true;
                GridView1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("morechannels.aspx?mode="+TextBox1.Text);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rw = GridView1.SelectedRow;
            int channelid = Int32.Parse(rw.Cells[2].Text);
            channel ch = db.channels.Where(c => c.channel_id == channelid).FirstOrDefault();
            ch.follow = ch.follow + 1;
            
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            user us = db.users.Where(u => u.username == username).FirstOrDefault();
            userfollowschannel uf = db.userfollowschannels.Where(u => u.userid == us.user_id && u.channelid == ch.channel_id).FirstOrDefault();
            if (uf == null)
            {
                userfollowschannel ufc = new userfollowschannel();
                ufc.channelid = ch.channel_id;
                ufc.userid = us.user_id;
                db.userfollowschannels.Add(ufc);
                db.SaveChanges();
                Response.Write("<script>alert('You have started following this channel')</script>");
            }
            else
            {
                Response.Write("<script>alert('You already follow this channel')</script>");
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rw = GridView2.SelectedRow;
            int channelid = Int32.Parse(rw.Cells[2].Text);
            channel ch = db.channels.Where(c => c.channel_id == channelid).FirstOrDefault();
            ch.follow = ch.follow + 1;
            db.SaveChanges();
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            user us = db.users.Where(u => u.username == username).FirstOrDefault();
            userfollowschannel uf = db.userfollowschannels.Where(u => u.userid == us.user_id && u.channelid == ch.channel_id).FirstOrDefault();
            if (uf == null)
            {
                userfollowschannel ufc = new userfollowschannel();
                ufc.channelid = ch.channel_id;
                ufc.userid = us.user_id;
                db.userfollowschannels.Add(ufc);
                db.SaveChanges();
                Response.Write("<script>alert('You have started following this channel')</script>");
            }
            else {
                Response.Write("<script>alert('You already follow this channel')</script>");
            }
        }
    }
}