using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace etkinlikkayitsites
{
    public partial class AnaSayfa1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EtkinlikBitisSaatiniHesapla hesapla = new EtkinlikBitisSaatiniHesapla();
            hesapla.HesaplaVeGuncelle();
        }
    }
}