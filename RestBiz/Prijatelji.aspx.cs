using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestBiz.DataLayer;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;

namespace RestBiz
{
    public partial class Prijatelji : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Site)this.Master).MarkAsSelected("PrijateljiLink");

                using (var ctx = new RestBizContext())
                {
                    string userEmail = HttpContext.Current.User.Identity.Name;
                    KorisniciRepeater.DataSource = (from k in ctx.Korisnici where k.Email != userEmail select k).OrderBy(o => o.Ime).ThenBy(o => o.Prezime).ToList<Korisnik>();
                    KorisniciRepeater.DataBind();
                }
            }


        }

        protected void KorisniciRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Korisnik prijatelj = (Korisnik)e.Item.DataItem;
            string Id = prijatelj.KorisnikId.ToString();
            HtmlAnchor buttonCell = (HtmlAnchor)e.Item.FindControl("buttonCell");
            bool friends = false;
            string userEmail = System.Web.HttpContext.Current.User.Identity.Name;
            
            using (var ctx = new RestBizContext())
            {
                var korisnik = (from k in ctx.Korisnici where k.Email == userEmail select k).FirstOrDefault<Korisnik>();
                if(korisnik.Prijatelji.Select(k => k.KorisnikId).Contains(prijatelj.KorisnikId))
                    friends = true;
                else
                    friends = false;
            }

            if (!friends)
            {
                buttonCell.Attributes.Add("class", "pure-button pure-button-primary");
                buttonCell.InnerText = "Dodaj za prijatelja";
                string _params = Id + ", " + e.Item.ItemIndex.ToString();
                buttonCell.HRef = "javascript:addFriend(" + _params + ")";
            }
            else
            {
                buttonCell.Attributes.Add("class", "pure-button");
                buttonCell.InnerText = "Ukloni iz prijatelja";
                string _params = Id + ", " + e.Item.ItemIndex.ToString();
                buttonCell.HRef = "javascript:removeFriend(" + _params + ")";
            }
        }

        #region WebMethods

        [WebMethod]
        public static string AddFriend(int id)
        {
            string retVal = "nista";
            Korisnik prijateljClone = null;
            using (var ctx = new RestBizContext())
            {
                var prijatelj = ctx.Korisnici.Find(id);
                if (prijatelj == null)
                    retVal = "null je";
                else
                {
                    retVal = prijatelj.ImePrezime;
                    string userEmail = System.Web.HttpContext.Current.User.Identity.Name;
                    var korisnik = (from k in ctx.Korisnici where k.Email == userEmail select k).FirstOrDefault<Korisnik>();
                    prijateljClone = new Korisnik()
                    {
                        Ime=prijatelj.Ime,
                        Prezime=prijatelj.Prezime
                    };
                    korisnik.Prijatelji.Add(prijatelj);
                    ctx.SaveChanges(); 
                }
                retVal = new JavaScriptSerializer().Serialize(prijateljClone);
                
            }

            return retVal;
        }

        [WebMethod]
        public static string RemoveFriend(int id)
        {
            string retVal = "";
            Korisnik prijateljClone = null;
            using (var ctx = new RestBizContext())
            {
                string userEmail = System.Web.HttpContext.Current.User.Identity.Name;
                var korisnik = (from k in ctx.Korisnici where k.Email == userEmail select k).FirstOrDefault<Korisnik>();
                var prijatelj = ctx.Korisnici.Find(id);
                prijateljClone = new Korisnik()
                {
                    Ime = prijatelj.Ime,
                    Prezime = prijatelj.Prezime
                };
                korisnik.Prijatelji.Remove(prijatelj);
                ctx.SaveChanges();
                
            }
            retVal = new JavaScriptSerializer().Serialize(prijateljClone);
            return retVal;
        }

        #endregion

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchInput = SearchInput.Text;
            List<Korisnik> results = new List<Korisnik>();
            using(var ctx = new RestBizContext())
            {
                results = (from k in ctx.Korisnici where (k.Ime.Contains(searchInput) || k.Prezime.Contains(searchInput))  select k).OrderBy(k => k.Ime).ThenBy(k => k.Prezime).ToList<Korisnik>();
            }

            if (results == null || results.Count == 0)
            {
                ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                HtmlGenericControl friendsTable = (HtmlGenericControl)ContentPlaceHolder1.FindControl("friendsTableDiv");
                friendsTable.Attributes.Add("style", "display: none");
            }
            else {
                KorisniciRepeater.DataSource = results;
                KorisniciRepeater.DataBind();
            }
         }
    }

}