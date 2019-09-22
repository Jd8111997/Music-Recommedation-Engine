using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class admin1 : System.Web.UI.Page
    {
        BeatsEntities db = new BeatsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            admin ad = db.admins.Where(a => a.username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (ad == null)
            {
                Response.Redirect("index.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
               // string fname = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
                string fextension = Path.GetExtension(FileUpload1.FileName);
                if (fextension == ".mp3")
                {
                   
                        

                        
                        string artist_name = TextBox3.Text;
                        string album_name = TextBox2.Text;
                        string genre_name = TextBox4.Text;
                        string duration = TextBox5.Text;
                        string trackname = TextBox1.Text;
                        song s = new song();
                        s.album = album_name;
                        s.artist = artist_name;
                        s.duration = duration;
                        s.count = 0;
                        s.song_rating = 0;
                        s.track = trackname;
                        s.counttoday = 0;
                        db.songs.Add(s);
                        db.SaveChanges();
                        songgenre sg = new songgenre();
                        sg.genre_id = Int32.Parse(genre_name);
                        sg.song_id = s.song_id;
                        db.songgenres.Add(sg);
                    db.SaveChanges();
                    song_add sa = new song_add();
                        sa.song_id = s.song_id;
                    sa.path = s.song_id + ".mp3";
                    sa.upload_time = System.DateTime.Now;
                        sa.admin_id = db.admins.Where(a => a.username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().admin_id;
                        db.song_add.Add(sa);
                        db.SaveChanges();
                    FileUpload1.SaveAs(Server.MapPath("~/")+s.song_id + ".mp3");
                    Response.Write("<script>alert('Upload Successful')</script>");
                        
                    }
                   
                }


            
            if (!FileUpload1.HasFile)
            {
                Response.Write("<script>alert('Select atleast one file')</script>");
            }
        }

        
    }
}