using RestBiz.Ajax;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RestBiz.Utills;

namespace RestBiz
{
    public partial class Rezervacija : System.Web.UI.Page
    {
        private int IdRestorana
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["idRest"]);
            }
            set
            {

            }
        }

        private string NazivRestorana
        {
            get
            {
                using(var ctx = new RestBizContext())
                {
                    return ctx.Restorani.Find(IdRestorana).Naziv;
                }
            }
            set
            {

            }
        }

        private Dictionary<int, int> Stolovi
        {
            get
            {
                Dictionary<int, int> raspored = new Dictionary<int, int>();
                List<PozicijaStola> pozcije = new List<PozicijaStola>();
                using(var ctx = new RestBizContext())
                {
                    pozcije = ctx.Restorani.Find(IdRestorana).KonfiguracijaSedenja.Stolovi.ToList();
                }
                foreach(PozicijaStola p in pozcije)
                {
                    raspored[p.Pozicija] = p.BrojStola;
                }
                return raspored;
            }
            set
            {

            }
        }

        private Dictionary<string, string> Forma
        {
            get
            {
                return (Dictionary<string, string>)ViewState["Forma"];
            }
            set
            {
                ViewState["Forma"] = value;
            }
        }

        private List<int> IzabraniStolovi
        {
            get
            {
                return (List< int>)ViewState["IzabraniStolovi"];
            }
            set
            {
                ViewState["IzabraniStolovi"] = value;
            }
        }

        private HtmlInputText DateInput
        {
            get
            {
                ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                return (HtmlInputText)ContentPlaceHolder1.FindControl("Date");
            }
            set
            {

            }
        }

        private HtmlInputText DateTimeInput
        {
            get
            {
                ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                return (HtmlInputText)ContentPlaceHolder1.FindControl("DateTime");
            }
            set
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Name.Text = NazivRestorana;

                
            }
            if(Forma != null)
            {
                ReloadFormValues();
            }

        }

        protected void NextButton1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> form = new Dictionary<string, string>();
            form["Restoran"] = Name.Text;
            form["Datum"] = DateInput.Value;
            form["Vreme"] = DateTimeInput.Value;
            form["Trajanje"] = Time.Text;

            Forma = form;

            Name.ReadOnly = true;
            DateInput.Attributes.Add("readonly", "readonly");
            DateTime.Attributes.Add("readonly", "readonly");
            Time.ReadOnly = true;

            NextButton1.Visible = false;

            RasporediStolove();
            RasporedStolovaDiv.Attributes.Remove("style");     
        }

        private void RasporediStolove()
        {
            int[] data = new int[5];
            for (int i = 0; i < 5; i++)
                data[i] = i;
            RowsRepeater.DataSource = data;
            RowsRepeater.DataBind();
        }

        protected void RowsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater ColsRepeater = (Repeater)e.Item.FindControl("ColsRepeater");
            int rowCount = (int)e.Item.DataItem;

            ColsRepeater.ItemDataBound += new RepeaterItemEventHandler(ColsRepeater_ItemDataBound);

            int[] data = new int[8];
            for (int i = 0; i < 8; i++)
                data[i] = rowCount*8 + i;

            ColsRepeater.DataSource = data;
            ColsRepeater.DataBind();

            
        }

        protected void ColsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int pozicija = (int)e.Item.DataItem;
            HtmlAnchor sto = (HtmlAnchor)e.Item.FindControl("sto");

            try {
                int brojstola = Stolovi[pozicija];
                sto.InnerHtml = brojstola.ToString();
                sto.Attributes.Remove("class");
                if(!isTaken(brojstola))
                    sto.Attributes.Add("class", "pure-button button-xlarge free-table stolovi");
                else
                    sto.Attributes.Add("class", "pure-button button-xlarge taken-table stolovi");
            } catch (KeyNotFoundException ex)
            {
                sto.Attributes.Remove("class");
                sto.Attributes.Add("class", "pure-button button-xlarge pure-button-disabled stolovi");
            }
        }

        private void ReloadFormValues()
        {
            Dictionary<string, string> form = Forma;

            Name.Text = form["Restoran"];
            DateInput.Value = form["Datum"];
            DateTimeInput.Value = form["Vreme"];
            Time.Text = form["Trajanje"];
        }


        protected void SearchButton_Click(object sender, EventArgs e)
        {
            InviteDiv.Attributes.Remove("style");

            string searchInput = SearchInput.Text;
            List<Korisnik> results = new List<Korisnik>();
            string userEmail = System.Web.HttpContext.Current.User.Identity.Name;
            using (var ctx = new RestBizContext())
            {
                var currentUser = (from k in ctx.Korisnici where k.Email == userEmail select k).FirstOrDefault();
                var friends = currentUser.Prijatelji.ToList<Korisnik>();
                foreach (Korisnik k in friends)
                    if (k.Ime.ToLower().Contains(searchInput.ToLower()) || k.Prezime.ToLower().Contains(searchInput.ToLower()))
                        results.Add(k);
                
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

            List<int> InvitedIds = (List<int>)HttpContext.Current.Session["InvitedIds"];

            if (InvitedIds != null && InvitedIds.Contains(prijatelj.KorisnikId))
            {
                buttonCell.Attributes.Add("class", "pure-button");
                buttonCell.InnerText = "Ukloni iz poziva";
                buttonCell.ID = "btnCell" + Id;
                string _params = Id;
                buttonCell.HRef = "javascript:removeInvite(" + _params + ")";
            }
            else
            {
                buttonCell.Attributes.Add("class", "pure-button pure-button-primary");
                buttonCell.InnerText = "Pozovi";
                buttonCell.ID = "btnCell" + Id;
                string _params = Id;
                buttonCell.HRef = "javascript:inviteFriend(" + _params + ")";
            }
        }

        #region WebMethods

        [WebMethod(EnableSession = true)]
        public static string SaveTables(List<int> stolovi)
        {
            string retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "Greska"));

            HttpContext.Current.Session["Stolovi"] = stolovi;

            retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, HttpContext.Current.Session["Stolovi"]));

            return retVal;
        }

        [WebMethod(EnableSession = true)]
        public static string InviteFriend(int id)
        {
            string retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "Greska"));

            if (HttpContext.Current.Session["InvitedIds"] == null)
            {
                List<int> InvitedIds = new List<int>();
                InvitedIds.Add(id);
                HttpContext.Current.Session["InvitedIds"] = InvitedIds;
                retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, InvitedIds));
            }
            else
            {
                List<int> InvitedIds = (List<int>)HttpContext.Current.Session["InvitedIds"];
                InvitedIds.Add(id);
                HttpContext.Current.Session["InvitedIds"] = InvitedIds;
                retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, InvitedIds));
            }

            return retVal; 
        }

        [WebMethod(EnableSession = true)]
        public static string RemoveInvite(int id)
        {
            string retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "Greska"));

            if (HttpContext.Current.Session["InvitedIds"] != null)
            {
                List<int> InvitedIds = (List<int>)HttpContext.Current.Session["InvitedIds"];
                InvitedIds.Remove(id);
                HttpContext.Current.Session["InvitedIds"] = InvitedIds;
                retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, InvitedIds));
            }
            

            return retVal;
        }

        #endregion


        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            using(var ctx = new RestBizContext())
            {
                RestBiz.DataLayer.Entities.Rezervacija rezervacija = new DataLayer.Entities.Rezervacija();
                rezervacija.Stolovi = new List<PozicijaStola>();
                rezervacija.Prijatelji = new List<Korisnik>();

                List<int> stolovi = (List<int>)HttpContext.Current.Session["Stolovi"];
                List<int> prijateljiIds = (List<int>)HttpContext.Current.Session["InvitedIds"];

                foreach(int brojStola in stolovi)
                {
                    var pozicijaStola = (from p in ctx.Stolovi where p.KonfiguracijaSedenja.IdRestorana == IdRestorana && p.BrojStola == brojStola select p).FirstOrDefault();
                    rezervacija.Stolovi.Add(pozicijaStola);
                }

                if (prijateljiIds != null)
                {
                    foreach (int prId in prijateljiIds)
                    {
                        var prijatelj = ctx.Korisnici.Find(prId);
                        rezervacija.Prijatelji.Add(prijatelj);
                    }
                }

                rezervacija.Restoran = ctx.Restorani.Find(IdRestorana);

                string[] datumInputs = Forma["Datum"].Split('/');

                string[] vremeInputs = Forma["Vreme"].Split(':');

                DateTime Pocetak = new DateTime(Convert.ToInt32(datumInputs[2]), Convert.ToInt32(datumInputs[0]), Convert.ToInt32(datumInputs[1]), Convert.ToInt32(vremeInputs[0]), Convert.ToInt32(vremeInputs[1]), 0);
                DateTime Zavrsetak = Pocetak.AddHours(Convert.ToInt32(Forma["Trajanje"]));

                rezervacija.Pocetak = Pocetak;
                rezervacija.Zavrsetak = Zavrsetak;

                ctx.Rezervacije.Add(rezervacija);

                ctx.SaveChanges();

                new Invitation().SendInvitation(ctx, rezervacija);
            }

            HttpContext.Current.Session["Stolovi"] = null;
            HttpContext.Current.Session["InvitedIds"] = null;

            RasporedStolovaDiv.Visible = false;
            InviteDiv.Visible = false;
            FormDiv.Visible = false;
            MessageDiv.Visible = true;

         }

        private bool isTaken(int brojStola)
        {
            string[] datumInputs = Forma["Datum"].Split('/');
            string[] vremeInputs = Forma["Vreme"].Split(':');

            DateTime Pocetak = new DateTime(Convert.ToInt32(datumInputs[2]), Convert.ToInt32(datumInputs[0]), Convert.ToInt32(datumInputs[1]), Convert.ToInt32(vremeInputs[0]), Convert.ToInt32(vremeInputs[1]), 0);
            DateTime Zavrsetak = Pocetak.AddHours(Convert.ToInt32(Forma["Trajanje"]));

            using (var ctx = new RestBizContext())
            {
                var rezervacije = (from r in ctx.Rezervacije where ((r.Zavrsetak >= Pocetak && r.Zavrsetak <= Zavrsetak && r.Pocetak<=Pocetak) || (r.Pocetak <= Zavrsetak && r.Pocetak >= Pocetak && r.Zavrsetak>=Zavrsetak) || (r.Pocetak <= Pocetak && r.Zavrsetak >= Zavrsetak) || (Pocetak<=r.Pocetak && Zavrsetak>=r.Zavrsetak)) && (r.Restoran.RestoranId == IdRestorana) select r).ToList();
                if (rezervacije != null)
                {
                    foreach (var rez in rezervacije)
                        foreach (var sto in rez.Stolovi)
                            if (sto.BrojStola == brojStola)
                                return true;
                }
            }
            return false;
        }
    }
}