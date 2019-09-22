using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class recommendme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BeatsEntities db = new BeatsEntities();
            
            user usr = db.users.Where(u => u.username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (usr == null)
            {
                Response.Redirect("index.aspx");
            }
        
            var p = from c in db.correlations
                    where c.user1 == usr.user_id//&&c.factor>0
                    select new { User = c.user2, Correlation = c.factor };
            var q = from c in db.correlations
                    where c.user2 == usr.user_id//&&c.factor>0
                    //orderby c.factor descending
                    select new { User = c.user1, Correlation = c.factor };
            var alluserswithfactormorethanzero= p.Union(q);
            List<sg> ls = new List<sg>();
            //getting history of loggged in user
            var historyloggeduser = from g in db.histories
                                    where g.user_id == usr.user_id
                                    select new { Id = g.song_id ,Ratings=g.ratings};
            //getting songlist,userid,ratings as given by the user,and correlation with everyuser of song i haven't heard
            foreach (var s in alluserswithfactormorethanzero)
            {
                var historyuser = from g in db.histories
                                  where g.user_id == s.User
                                  select new { Id = g.song_id,Ratings=g.ratings };
                foreach (var hist in historyuser)
                {
                    //checking if the song is heard by the logged in user
                    var s1 = db.histories.Where(h => h.song_id == hist.Id&&h.user_id==usr.user_id).FirstOrDefault();
                    if (s1 == null)
                    {
                        sg sg1 = new sg();
                        sg1.sgid = hist.Id;//this is songid
                        sg1.correlid = s.Correlation;//this is correl factor
                        sg1.usrratings = (int)hist.Ratings;
                        ls.Add(sg1);
                    }
                }
                double totalratings=0;
                double finalratings = 0;
                var songidwithcount = from l in ls
                                      group l by l.sgid into songID
                                      select new { Count = songID.Count(), identity = songID.Key };
                //getting list of songs with their songid
                List<sgg> finalsongs = new List<sgg>();
                foreach (var z in songidwithcount)
                {
                    totalratings = 0;
                    var n = from l in ls
                            where l.sgid == z.identity
                            select new { rate = l.usrratings, fact = l.correlid };
                    foreach (var g in n)
                    {
                        totalratings =totalratings+ g.rate * g.fact;
                        
                    }
                    finalratings = totalratings / z.Count;
                    if (finalratings > 0)
                    {
                        sgg s1 = new sgg();
                        s1.songid = z.identity;
                        s1.songrate = finalratings;
                       
                        finalsongs.Add(s1);
                    }
                }
                List<track> tnew = new List<track> ();
                foreach (var fs in finalsongs)
                {
                    song ss = db.songs.Where(sn => sn.song_id == fs.songid).FirstOrDefault();
                    track tr = new track();
                    if (ss != null)
                    {
                        tr.SongId = fs.songid;
                        tr.Trackname = ss.track;
                        tr.Artist = ss.artist;
                        tr.RecommendedRating = fs.songrate;
                    }
                    tnew.Add(tr);
                    
                }
                GridView1.DataSource = tnew;
                
                GridView1.DataBind();
            }
           
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow r1 = GridView1.SelectedRow;
            int id = Int32.Parse(r1.Cells[1].Text);
            string path = "play.aspx?id=";
            path += id;
            Response.Redirect(path);
        }
    }
    public class sg
    {
        public int sgid { get; set; }
        public double correlid { get; set; }
        
        public int usrratings { get; set; }
    }
    public class track
    {
        public int SongId { get; set; }
        public string Trackname { get; set; }
        public string Artist { get; set; }
        public double RecommendedRating { get; set; }
       
    }
    public class sgg
    {
        public int songid { get; set; }
        public double songrate { get; set; }
    }
    
}
