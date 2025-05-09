<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminEtkinlikOnaylama.aspx.cs" Inherits="etkinlikkayitsites.AdminEtkinlikOnaylama" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        .navbar{display:none}
     
        .header-bar {
            background-color: #333; 
            color: white;
            padding: 10px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .header-bar h1 {
            font-size: 24px;
            margin: 0;
        }

        .header-bar .btn {
            padding: 8px 16px;
            margin-left: 10px;
            border: none;
            border-radius: 4px;
            text-decoration: none;
            font-size: 14px;
            font-weight: bold;
            color: white;
            cursor: pointer;
        }

        .btn-anasayfa {
            background-color: orange;
        }

        .btn-anasayfa:hover {
            background-color: darkorange;
        }

        .btn-cikis {
            background-color: red;
        }

        .btn-cikis:hover {
            background-color: darkred;
        }

        .etkinlikler-wrapper {
            margin: 30px auto;
            padding: 20px;
            max-width: 1200px;
            background-color: #f9f9f9;
            border-radius: 12px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .etkinlikler-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            justify-content: flex-start;
        }

        .etkinlik-kutusu {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 15px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            background-color: #fff;
            transition: transform 0.2s;
            width: calc(33.333% - 20px);
        }

        .etkinlik-kutusu:hover {
            transform: translateY(-5px);
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
        }

        .etkinlik-baslik {
            font-size: 20px;
            font-weight: bold;
            color: #333;
            margin-bottom: 8px;
        }

        .etkinlik-detay {
            font-size: 14px;
            color: #666;
            margin-top: 8px;
        }

        .etkinlik-butonlar {
            margin-top: 10px;
            display: flex;
            justify-content: space-between;
        }

        .btn-onayla {
            background-color: orangered;
            color: white;
            padding: 8px 16px;
            border: none;
            border-radius: 4px;
            font-size: 14px;
            cursor: pointer;
        }

        .btn-onayla:hover {
            background-color: hotpink;
        }

        .btn-iptal {
            background-color: grey;
            color: white;
            padding: 8px 16px;
            border: none;
            border-radius: 4px;
            font-size: 14px;
            cursor: pointer;
        }

        .btn-iptal:hover {
            background-color: darkgrey;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header-bar">
        <h1>ONAY BEKLEYEN ETKİNLİKLER</h1>
        <div>
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

    <div class="etkinlikler-wrapper">
        <h2>Onay Bekleyen Etkinlikler</h2>
        <div class="etkinlikler-container" id="OnaysizEtkinlikContainer" runat="server">
            <asp:Panel ID="EventsContainer" runat="server">
                <div class="etkinlik-kutusu">
                    <div class="etkinlik-baslik">Etkinlik Başlık</div>
                    <div class="etkinlik-detay">Etkinlik Detayı</div>
                    <div class="etkinlik-butonlar">
                        <button class="btn-onayla">Onayla</button>
                        <button class="btn-iptal">İptal</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
