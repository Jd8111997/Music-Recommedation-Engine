using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class channels : System.Web.UI.Page
    {
        BeatsEntities db = new BeatsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Request.QueryString["msg"]);
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("index.aspx");
            }
            else if(System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //getting user
                string user1 = System.Web.HttpContext.Current.User.Identity.Name;
                user us1 = db.users.Where(u => u.username == user1).FirstOrDefault();
                //getting channels followed by the user
                var userchannels = (from usf in db.userfollowschannels
                                    join ch in db.channels
                                    on usf.channelid equals ch.channel_id
                                    join up in db.uploads
                                    on usf.channelid equals up.channel_id
                                    join tr in db.trailers
                                    on up.trailer_id equals tr.trailer_id
                                    where usf.userid == us1.user_id
                                    select new
                                    {
                                        ID = tr.trailer_id,
                                        Description = tr.title,
                                        Channel = ch.channel_name
                                    }).ToList();
                GridView1.DataSource = userchannels;
                GridView1.DataBind();

            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rw = GridView1.SelectedRow;
            string source = "tr" + rw.Cells[1].Text + ".mp3";
            string str = "<audio controls onended ='callme()'>";
            str += "<source src='" +source + "'type='audio/mpeg'";
            str += "</audio>";
            

            Response.Write(str);
        }
    }
}