using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MathNet.Numerics.Statistics;
namespace Beats1
{
    public partial class saveratings : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            BeatsEntities db = new BeatsEntities();
            string user1 = System.Web.HttpContext.Current.User.Identity.Name;

            if (Request.QueryString["rating"] != null)
            {
                int songid = Int32.Parse(Request.QueryString["songid"]);
                int rating = Int32.Parse(Request.QueryString["rating"]);
                //getting user name
                //to get song s of songid id so that we can update count,counttoday,and ratings of that song
                if (rating <= 10 && rating >= 0)
                {
                    song s1 = db.songs.Where(y => y.song_id == songid).FirstOrDefault();
                    s1.count++;
                    s1.counttoday++;
                    s1.song_rating = (s1.song_rating * (s1.count - 1) + Int32.Parse(Request.QueryString["rating"])) / s1.count;
                    db.SaveChanges();
                    user us = db.users.Where(s => s.username == user1).FirstOrDefault();
                    if (us != null)
                    {
                        history h = db.histories.Where(s => s.user_id == us.user_id && s.song_id == songid).FirstOrDefault();

                        //checking if user has previously listened the song and then updating the count only else updating the database 
                        if (h == null)
                        {

                            history h1 = new history();
                            h1.song_id = songid;
                            h1.user_id = us.user_id;
                            h1.count = 1;
                            h1.ratings = Int32.Parse(Request.QueryString["rating"]);
                            h1.time = DateTime.Now;
                            db.histories.Add(h1);
                            


                        }
                        if (h != null)
                        {
                            h.count = h.count + 1;
                            h.time = DateTime.Now;
                            h.ratings = Int32.Parse(Request.QueryString["rating"]);
                            
                        }
                        db.SaveChanges();
                        List<double> r1 = new List<double>();
                        List<double> r2 = new List<double>();
                        
                        int primary = 0, secondary = 0;
                        foreach (user u in db.users)
                        {
                            if (u.user_id != us.user_id)
                            {
                                if (u.user_id > us.user_id)
                                {
                                    secondary = u.user_id;
                                    primary = us.user_id;
                                }
                                if (u.user_id < us.user_id)
                                {
                                    secondary = us.user_id;
                                    primary = u.user_id;
                                }
                                //getting histories
                                var p1 = (from h1 in db.histories
                                          where h1.user_id == primary
                                          select new { Song = h1.song_id, rating = h1.ratings }).ToList();
                                var p2 = (from h1 in db.histories
                                          where h1.user_id == secondary
                                          select new { Song = h1.song_id, rating = h1.ratings }).ToList();
                                //getting rating of common songs
                                foreach (var p in p1)
                                {
                                    foreach (var q in p2)
                                    {
                                        if (p.Song == q.Song)
                                        {

                                            r1.Add((double)p.rating);
                                            r2.Add((double)q.rating);
                                        }
                                    }
                                }
                                if (!(r1.Count == 0 || r2.Count == 0))
                                {
                                    double correl1 = Correlation.Pearson(r1, r2);
                                    if (!(Double.IsNaN(correl1)))
                                    {
                                        
                                        

                                        r1.Clear();
                                        r2.Clear();

                                        correlation c = db.correlations.Where(c1 => c1.user1 == primary && c1.user2 == secondary).FirstOrDefault();

                                        if (c == null)
                                        {
                                            correlation c2 = new correlation();
                                            c2.user1 = primary;
                                            c2.user2 = secondary;
                                            c2.factor = (float)correl1;
                                            db.correlations.Add(c2);

                                        }
                                        if (c != null)
                                        {
                                            c.factor = (float)correl1;

                                        }


                                    }
                                }
                            }
                        }
                        db.SaveChanges();


                        Response.Redirect("index.aspx");


                    }

                    else
                    {
                        Response.Redirect("index.aspx");
                    }

                }

            }
        }
    }
}

