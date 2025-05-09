<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AnaSayfa.aspx.cs" Inherits="etkinlikkayitsites.AnaSayfa1" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Header-->
    <header class="masthead text-center text-white">
        <div class="masthead-content">
            <div class="container px-5">
                <h1 class="masthead-heading mb-0">Etkinliklerinize Katılın ve Yeni Etkinlikler Düzenleyin</h1>
                <h2 class="masthead-subheading mb-0">İlginizi Çeken Etkinliklere Kolayca Erişin</h2>
                <a class="btn btn-primary btn-xl rounded-pill mt-5" href="#scroll">Detaylı Bilgi</a>
            </div>
        </div>
        <div class="bg-circle-1 bg-circle"></div>
        <div class="bg-circle-2 bg-circle"></div>
        <div class="bg-circle-3 bg-circle"></div>
        <div class="bg-circle-4 bg-circle"></div>
    </header>

    <!-- Content section 1-->
    <section id="scroll">
        <div class="container px-5">
            <div class="row gx-5 align-items-center">
                <div class="col-lg-6 order-lg-2">
                    <div class="p-5"><img class="img-fluid rounded-circle" src="assets/img/04.jpeg" alt="..." /></div>
                </div>
                <div class="col-lg-6 order-lg-1">
                    <div class="p-5">
                        <h2 class="display-4">Etkinliklerinizi Kendi İhtiyaçlarınıza Göre Düzenleyin</h2>
                        <p>Platformumuz, kullanıcıların kendilerine özel etkinlikler düzenlemesine olanak tanır. Yeni bir etkinlik oluşturabilir veya mevcut etkinlikler üzerinde değişiklikler yapabilirsiniz. Böylece etkinliklerinizi, katılımcı sayısından içerik detaylarına kadar tamamen ihtiyaçlarınıza göre şekillendirebilirsiniz.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Content section 2-->
    <section>
        <div class="container px-5">
            <div class="row gx-5 align-items-center">
                <div class="col-lg-6">
                    <div class="p-5"><img class="img-fluid rounded-circle" src="assets/img/07.jpg" alt="..." /></div>
                </div>
                <div class="col-lg-6">
                    <div class="p-5">
                        <h2 class="display-4">Kolay Etkinlik Katılımı</h2>
                        <p>Kullanıcılarımız, ilgi alanlarına göre etkinlikleri kolayca bulup katılabilirler. Sistemimiz, kullanıcının geçmiş katılımlarını dikkate alarak öneriler sunar ve etkinliklere hızlıca erişim sağlar. Yeni deneyimler keşfetmek ve ilginizi çeken etkinliklere katılmak artık çok daha kolay.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Content section 3-->
    <section>
        <div class="container px-5">
            <div class="row gx-5 align-items-center">
                <div class="col-lg-6 order-lg-2">
                    <div class="p-5"><img class="img-fluid rounded-circle" src="assets/img/06.jpeg" alt="..." /></div>
                </div>
                <div class="col-lg-6 order-lg-1">
                    <div class="p-5">
                        <h2 class="display-4">Öneri Sistemi ile Yeni Etkinlikler Keşfedin</h2>
                        <p>Sistemimiz, katıldığınız etkinlikler ve ilgi alanlarınız doğrultusunda size yeni etkinlik önerileri sunar. Bu sayede, ilgilendiğiniz alanlarda yeni deneyimler edinebilir, sosyal çevrenizi genişletebilirsiniz. Platformumuz ile her zaman size uygun etkinliklere erişim sağlayabilirsiniz.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
