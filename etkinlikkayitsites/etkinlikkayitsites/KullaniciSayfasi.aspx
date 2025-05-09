<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KullaniciSayfasi.aspx.cs" Inherits="etkinlikkayitsites.KullaniciSayfasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
              
        .profilim-button {
            padding: 12px 20px;
            font-size: 16px;
            color: #fff;
            background-color: #d6336c;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
            margin-left: 15px;
            transition: background-color 0.3s ease;
        }
.profilim-button,
.cikis-button {
    padding: 8px 15px; 
    font-size: 14px; 
    color: #fff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    text-decoration: none;
    width: auto;
    text-align: center;
    transition: background-color 0.3s ease;
    display: inline-block; 
    margin-left: 10px; 
}

.cikis-button {
    background-color: #ff7300; 
}

.cikis-button:hover {
    background-color: #cc5c00;
}



.nav-item {
    display: flex;
    align-items: center;
    gap: 10px; 
}

        .profilim-button:hover {
            background-color: #b52a56;
        }
        .profilim-button {
            padding: 8px 16px;
            font-size: 16px;
            color: #fff;
            background-color: #d6336c;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
            margin-left: 15px;
            transition: background-color 0.3s ease;
        }

        .profilim-button:hover {
            background-color: #b52a56;
        }

       
        .center-buttons {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
            justify-content: center;
            align-items: center;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            max-width: 800px;
            width: 100%;
        }

        .etkinlik-buton {
            padding: 20px;
            font-size: 18px;
            color: #fff;
            background-color: #d6336c;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            text-align: center;
            transition: background-color 0.3s ease;
        }

        .etkinlik-buton:hover {
            background-color: #b52a56;
        }

       
        @media (max-width: 768px) {
            .center-buttons {
                grid-template-columns: 1fr;
                gap: 15px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
    
    <li class="nav-item">
        <asp:LinkButton ID="btnProfilim" runat="server" Text="Profilim" OnClick="btnProfilim_Click" CssClass="nav-link profilim-button"></asp:LinkButton>
         <asp:LinkButton ID="btnCikisYap" runat="server" Text="Çıkış Yap" OnClick="btnCikisYap_Click" CssClass="cikis-button"></asp:LinkButton>
    </li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="center-buttons">
        <asp:Button ID="btnTumEtkinlikler" runat="server" Text="Tüm Etkinlikleri Görüntüle" CssClass="etkinlik-buton" OnClick="TumEtkinlikleriGoruntule_Click" />
        <asp:Button ID="btnOnerilenEtkinlikler" runat="server" Text="Önerilen Etkinlikleri Görüntüle" CssClass="etkinlik-buton" OnClick="OnerilenEtkinlikleriGoruntule_Click" />
        <asp:Button ID="btnEtkinlikOlustur" runat="server" Text="Etkinlik Oluştur" CssClass="etkinlik-buton" OnClick="EtkinlikOlustur_Click" />
        <asp:Button ID="btnKendiEtkinlikleri" runat="server" Text="Oluşturduğun Etkinlikleri Görüntüle" CssClass="etkinlik-buton" OnClick="KendiEtkinlikleriniGoruntule_Click" />
        <asp:Button ID="btnKatildiginEtkinlikleriGoruntule" runat="server" Text="Katıldığın Etkinlikleri Görüntüle" CssClass="etkinlik-buton" OnClick="KatildiginEtkinlikleriGoruntule_Click" />
        <asp:Button ID="btnHaritaGoruntuleme" runat="server" Text=" Etkinlikleri Haritada Görüntüleme" CssClass="etkinlik-buton" OnClick="HaritaGoruntuleme_Click" />
    </div>
</asp:Content>
