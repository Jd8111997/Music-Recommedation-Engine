using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Beats1
{
    public partial class mychannels : System.Web.UI.Page
    {
        BeatsEntities db = new BeatsEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
           
                if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("index.aspx");
                }
                else if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Label1.Visible = false;
                    TextBox1.Visible = false;
                    Label2.Visible = false;
                    TextBox2.Visible = false;
                    Button3.Visible = false;
                    Button2.Visible = false;
                    FileUpload1.Visible = false;
                    Label3.Visible = false;
                    TextBox3.Visible = false;

                    //getting all the channles created by the user 
                    string username = System.Web.HttpContext.Current.User.Identity.Name;
                    user us = db.users.Where(u => u.username == username).FirstOrDefault();
                    usercreatedchannel ucc1 = db.usercreatedchannels.Where(u => u.userid == us.user_id).FirstOrDefault();
                    if (ucc1 == null)
                    {
                        Label1.Visible = true;
                        TextBox1.Visible = true;
                        Label2.Visible = true;
                        TextBox2.Visible = true;
                        Button2.Visible = true;
                    }
                    if (ucc1 != null)
                    {
                        Button3.Visible = true;
                        FileUpload1.Visible = true;
                        Label3.Visible = true;
                        TextBox3.Visible = true;
                        
                        var ucc = (from uc in db.usercreatedchannels
                                   join ch in db.channels
                                   on uc.channelid equals ch.channel_id
                                   join up in db.uploads
                                   on uc.channelid equals up.channel_id
                                   join tr in db.trailers
                                   on up.trailer_id equals tr.trailer_id
                                   where uc.userid == us.user_id
                                   select new
                                   {
                                       ID = tr.trailer_id,
                                       Description = tr.title,
                                       Channel = ch.channel_name
                                   }).ToList();
                        GridView1.DataSource = ucc;
                        GridView1.DataBind();
                    }
                }
            }
        
        protected void Button2_Click(object sender, EventArgs e)
        {
            //adding channel and checking whether current channel name is available or not
            channel ch = new channel();
            channel ch2 = db.channels.Where(c => c.channel_name == TextBox1.Text).FirstOrDefault();
            if (ch2 == null)
            {

                ch.channel_name = TextBox1.Text;

                ch.channel_description = TextBox2.Text;
                db.channels.Add(ch);
                db.SaveChanges();
                usercreatedchannel ucc2 = new usercreatedchannel();
                string username = System.Web.HttpContext.Current.User.Identity.Name;
                user us = db.users.Where(u => u.username == username).FirstOrDefault();
                ucc2.userid = us.user_id;
                channel ch1 = db.channels.Where(c => c.channel_name == TextBox1.Text).FirstOrDefault();
                
                ucc2.channelid = ch1.channel_id;
                db.usercreatedchannels.Add(ucc2);
                db.SaveChanges();
                Response.Redirect("mychannels.aspx");
            }
            if (ch2 != null)
            {
                Response.Redirect("channels.aspx?msg=The channel name already exists.Please try another name");
            }  
                
           
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string username = System.Web.HttpContext.Current.User.Identity.Name;
                user us = db.users.Where(u => u.username == username).FirstOrDefault();
                //adding trailer
                trailer tr = new trailer();
                trailer tr2 = db.trailers.Where(t => t.title == TextBox3.Text).FirstOrDefault();
                if (tr2 != null)
                {
                    Response.Redirect("channels.aspx?msg=The trailer name already exists");
                }
                if (tr2 == null)
                {
                    if (TextBox3.Text == "")
                    {
                        tr.title = "No text";
                    }
                    else if (TextBox3.Text != "")
                    {
                        tr.title = TextBox3.Text;
                    }

                    db.trailers.Add(tr);
                    db.SaveChanges();
                    trailer tr1 = db.trailers.Where(d => d.title == TextBox3.Text).FirstOrDefault();
                    upload uc = new upload();
                    uc.trailer_id = tr1.trailer_id;
                    usercreatedchannel cc = db.usercreatedchannels.Where(u => u.userid == us.user_id).FirstOrDefault();
                    uc.channel_id = cc.channelid;

                    //maximum size set to 20mb
                    //look for size on webconfig

                    string fextension = Path.GetExtension(FileUpload1.FileName);
                    if (fextension == ".mp3")
                    {
                        try
                        {
                            FileUpload1.SaveAs(Server.MapPath("~/tr") + tr1.trailer_id + ".mp3");
                            uc.path = "/" + tr1.trailer_id + ".mp3";
                           
                            db.uploads.Add(uc);
                            db.SaveChanges();
                            Response.Write("<script>alert('Upload sucess!!')</script>");
                        }
                        catch (Exception ae)
                        {
                        }
                    }
                    else
                    {
                        Response.Write("Use proper mp3 extension");
                    }
                }
                if (!FileUpload1.HasFile)
                {
                    //to display proper message that file has not been uploaded yet
                    //Response.Write("<script>alert('Upload the file')</script>");
                    Response.Redirect("mychannels.aspx");
                }

            }

        }
          
            

        }
    }

     