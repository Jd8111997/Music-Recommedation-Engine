using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class account : System.Web.UI.Page
    {
        BeatsEntities db = new BeatsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                Label3.Visible = false;
                GridView1.Visible = false;
                Label1.Visible = false;
                Label2.Visible = false;
                Button1.Visible = false;
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator4.Enabled = false;
                RegularExpressionValidator1.Enabled = false;
                CompareValidator1.Enabled = false;
                ValidationSummary1.Enabled = false;
            }
            
            //check if user is logged in and if it is not logged in then redirect the anonymous user
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("index.aspx");
            }
            else if(System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                string mode = Request.QueryString["mode"];
                if (mode == "history")
                {
                    Label3.Visible = true;

                    string username1 = System.Web.HttpContext.Current.User.Identity.Name;
                    //get user history from username based on username and taking the join with the song id in song database with history table
                    var id = (from us in db.users
                              join hs in db.histories
                              on us.user_id equals hs.user_id
                              join sg in db.songs
                              on hs.song_id equals sg.song_id
                              where us.username == username1
                              select new { Track = sg.track, Album = sg.album, Time = hs.time, Rated = hs.ratings, Count = hs.count }).ToList();
                    GridView1.DataSource = id;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
                else if (mode == "update")
                {
                    Label1.Visible = true;
                    Label2.Visible = true;
                    Button1.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    RequiredFieldValidator3.Enabled = true;
                    RequiredFieldValidator4.Enabled = true;
                    CompareValidator1.Enabled = true;
                    RegularExpressionValidator1.Enabled = true;
                    ValidationSummary1.Enabled = true;
                }


            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //upadating the user with new password
            string username1 = System.Web.HttpContext.Current.User.Identity.Name;
            user us = db.users.Where(s => s.username == username1).FirstOrDefault();
            us.password = TextBox1.Text;
           
            db.SaveChanges();
          
            Response.Write("Password successfully changed");

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}