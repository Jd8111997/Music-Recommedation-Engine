using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Beats1
{
    public partial class index : System.Web.UI.Page
    {

       
        BeatsEntities db = new BeatsEntities();
        int mode = 0;
        // if mode =0 that means play the song else if  is 1 means download the song
        protected void Page_Load(object sender, EventArgs e)
        {
            //beacuse when a user is sucessfully created it is redirected and we need to display the message
            if (Request.QueryString["Login"] != null)
            {
                Response.Write("Sucesssfully created");
            }
            //checking if user is authenticated and then displaying the options
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HyperLink5.Visible = false;
                HyperLink2.Visible = false;
                LoginStatus1.Visible = false;
                HyperLink1.Visible = false;
                HyperLink3.Visible = false;
            }
            //checking if the authenticated user is admin and if he is admin then redirect to admin.aspx
            string usr = System.Web.HttpContext.Current.User.Identity.Name;
            admin ad = db.admins.Where(a => a.username == usr).FirstOrDefault();
            if (ad != null)
            {
                Response.Redirect("admin.aspx");
            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            LoginStatus1.LoginText = "";
            if (Request.QueryString["search"] != null)
            {
                DropDownList2.Visible = false;
                string query = Request.QueryString["search"];
                string mode = Request.QueryString["mode"];

                if (mode == "" + 1)
                {

                    var result = (from st in db.songs
                                  where st.artist.Contains(query)
                                  select new { ID = st.song_id, Artist = st.artist, Track = st.track, Album = st.album, Duration = st.duration, Rating = st.song_rating }).ToList();
                    GridView2.DataSource = result;
                }
                if (mode == "" + 3)
                {

                    var result = (from st in db.songs
                                  where st.track.Contains(query)
                                  select new { ID = st.song_id, Artist = st.artist, Track = st.track, Album = st.album, Duration = st.duration, Rating = st.song_rating }).ToList();
                    GridView2.DataSource = result;
                }
                if (mode == "" + 2)
                {

                    var result = (from st in db.songs
                                  join sg in db.songgenres on st.song_id equals sg.song_id
                                  join g in db.genres on sg.genre_id equals g.Id

                                  where g.name.Contains(query)
                                  select new { ID = st.song_id, Artist = st.artist, Track = st.track, Album = st.album, Duration = st.duration, Rating = st.song_rating, Genre = g.name }).ToList();
                    GridView2.DataSource = result;
                }

                GridView1.Visible = false;

                GridView2.DataBind();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue != "" + 0)
            {
                string url = "index.aspx?";
                url += "search=" + TextBox1.Text + "&";
                url += "mode=" + DropDownList1.SelectedIndex;
                Response.Redirect(url);
            }
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            GridView1.AllowSorting = true;

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedItem.Value == "" + 0)
            {
                var res = (from st in db.songs
                           join sa in db.song_add on st.song_id equals sa.song_id
                           orderby st.song_rating descending
                           select new
                           {
                               Id = st.song_id,
                               Artist = st.artist,
                               Track = st.track,
                               Album = st.album,
                               Duration = st.duration,
                               Rating = st.song_rating,
                               Date = sa.upload_time
                           }).ToList();
                GridView2.DataSource = res;
                GridView2.DataBind();
                
                GridView1.Visible = false;

            }
            if (DropDownList2.SelectedItem.Value == "" + 1)
            {

                var yesterday = DateTime.Today.AddDays(-1);
                var res = (from st in db.songs
                           join sa in db.song_add on st.song_id equals sa.song_id
                           where sa.upload_time > yesterday
                           orderby sa.upload_time
                           select new
                           {
                               Id = st.song_id,
                               Artist = st.artist,
                               Track = st.track,
                               Album = st.album,
                               Duration = st.duration,
                               Rating = st.song_rating,
                               Date = sa.upload_time
                           }).ToList();
                GridView2.DataSource = res;
                GridView2.DataBind();
                
                GridView1.Visible = false;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView2.SelectedRow;
            int id = Int32.Parse(row.Cells[1].Text);

            song_add songid = db.song_add.Where(s => s.song_id == id).FirstOrDefault();
            song sg = db.songs.Where(s => s.song_id == id).FirstOrDefault();

            if (mode == 1)
            {
                Response.ContentType = "audio/mpeg";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + sg.track);//The third argument represents the name of the file to be downloaded
                Response.WriteFile(songid.path);
                Response.End();
            }
            if (mode == 0)
            {
                string path = "play.aspx?id=";
                path += sg.song_id;
                Response.Redirect(path);
            }

        }
       



        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = GridView1.SelectedRow;
            int id = Int32.Parse(row.Cells[5].Text);

            song_add songid = db.song_add.Where(s => s.song_id == id).FirstOrDefault();
            song sg = db.songs.Where(s => s.song_id == id).FirstOrDefault();

            if (mode == 1)
            {
                Response.ContentType = "audio/mpeg";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + sg.track);//The third argument represents the name of the file to be downloaded
                Response.WriteFile(songid.path);
                Response.End();
            }
            if (mode == 0)
            {
                string path = "play.aspx?id=";
                path += sg.song_id;
                Response.Redirect(path);
            }
            
        }

        protected void LoginView2_ViewChanged(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            user us = db.users.Where(u => u.username == Login1.UserName && u.password == Login1.Password).FirstOrDefault();
            admin ad = db.admins.Where(a => a.username == Login1.UserName && a.password == Login1.Password).FirstOrDefault();
            if (us != null||ad!=null)
            {
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, true);
            }
          
              
            
            
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = Int32.Parse(RadioButtonList1.SelectedValue);
           
        }
    }
}