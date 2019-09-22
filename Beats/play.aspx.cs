using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Beats1
{
    public partial class play : System.Web.UI.Page
    {
        public int id;
        public string user1 = System.Web.HttpContext.Current.User.Identity.Name;

        protected void Page_Load(object sender, EventArgs e)
        {

            using (BeatsEntities db = new BeatsEntities())
            {
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("index.aspx");
                }
                    if (Request.QueryString["id"] != null)
                {

                     id = Int32.Parse(Request.QueryString["id"]);
                    var path = from pt in db.song_add
                               join sg in db.songs
                               on pt.song_id equals sg.song_id
                               where pt.song_id == id
                               select new { album = sg.album, songpath = pt.path, name = sg.track, artist = sg.artist, ratings = sg.song_rating };
                    foreach (var q in path)
                    {
                        string str = "<audio controls onended ='callme()'>";
                        str += "<source src='" + q.songpath + "'type='audio/mpeg'";
                        str += "</audio>";
                        Response.Write("Track Name:" + q.name + "<br>");
                        Response.Write("Artist Name:" + q.artist + "<br>");
                        Response.Write("Album Name:" + q.album + "<br>");
                        Response.Write("Ratings:" + q.ratings + "<br>");
                        Response.Write(str);
                        //checking the rating and inserting into database
                        

                    }
                }

            }
        }
    }
}
