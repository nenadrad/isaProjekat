using RestBiz.Ajax;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class MojProfil : System.Web.UI.Page
    {

        private string UserEmail
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
            set
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ((Site)this.Master).MarkAsSelected("MojProfilLink");

                LoadData();
            }

        }

        private void LoadData()
        {
            KorisnikService kService = new KorisnikService();

            Korisnik currentUser = kService.FindUserByEmail(UserEmail);
            ProfileHeader.Text = "<h2>"+currentUser.ImePrezime+"</h2>";

            PodaciKorisnika.DataSource = new List<Korisnik>() { currentUser };
            PodaciKorisnika.DataBind();

            List<Korisnik> prijatelji = kService.GetFriends(currentUser.KorisnikId);

            if (prijatelji.Count == 0)
            {
                ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                HtmlGenericControl friendsTable = (HtmlGenericControl)ContentPlaceHolder1.FindControl("friendsTableDiv");
                friendsTable.Attributes.Add("style", "display: none");
            }
            else
            {
                PrijateljiKorisnika.DataSource = prijatelji;
                PrijateljiKorisnika.DataBind();
            }

            
        }

        protected void PrijateljiKorisnika_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Korisnik prijatelj = (Korisnik)e.Item.DataItem;
            string Id = prijatelj.KorisnikId.ToString();
            HtmlAnchor buttonCell = (HtmlAnchor)e.Item.FindControl("buttonCell");

            buttonCell.Attributes.Add("class", "pure-button");
            buttonCell.InnerText = "Ukloni iz prijatelja";
            string _params = Id + ", " + e.Item.ItemIndex.ToString();
            buttonCell.HRef = "javascript:removeFriend(" + _params + ")";
        }

        #region WebMethods

        [WebMethod]
        public static string UpdateUser(int id, string ime, string prezime, string adresa)
        {
            string retVal = "";

            using (var ctx = new RestBizContext())
            {
                var korisnik = ctx.Korisnici.Find(id);
                if(korisnik !=null)
                {
                    korisnik.Ime = ime;
                    korisnik.Prezime = prezime;
                    korisnik.Adresa = adresa;

                    ctx.SaveChanges();

                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, "Podaci uspešno izmenjeni."));
                }
                else
                {
                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "Greška pri izmeni podataka"));
                }
            }


            return retVal;
        }

        #endregion

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchInput = SearchInput.Text;
            List<Korisnik> results = new List<Korisnik>();
            using (var ctx = new RestBizContext())
            {
                results = (from k in ctx.Korisnici where (k.Ime.Contains(searchInput) || k.Prezime.Contains(searchInput)) select k).OrderBy(k => k.Ime).ThenBy(k => k.Prezime).ToList<Korisnik>();
            }

            if (results != null && results.Count > 0)
            {
                ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                HtmlGenericControl friendsTable = (HtmlGenericControl)ContentPlaceHolder1.FindControl("friendsResults");
                friendsTable.Attributes.Remove("style");
                KorisniciRepeater.DataSource = results;
                KorisniciRepeater.DataBind();
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
                if (korisnik.Prijatelji.Select(k => k.KorisnikId).Contains(prijatelj.KorisnikId))
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
    }
}