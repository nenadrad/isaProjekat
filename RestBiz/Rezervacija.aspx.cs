using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
            RasporediStolove();
            RasporedStolova.Visible = true;
            //NextButton1.Visible = false;

            Dictionary<string, string> form = new Dictionary<string, string>();
            form["Restoran"] = Name.Text;
            form["Datum"] = DateInput.Value;
            form["Trajanje"] = Time.Text;

            Forma = form;
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
                sto.Attributes.Add("class", "pure-button button-xlarge free-table stolovi");
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
            Time.Text = form["Trajanje"];
        }

    }
}