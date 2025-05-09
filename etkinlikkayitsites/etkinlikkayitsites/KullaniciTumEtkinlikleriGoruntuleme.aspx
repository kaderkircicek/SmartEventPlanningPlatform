<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KullaniciTumEtkinlikleriGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.KullaniciTumEtkinlikleriGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        .navbar {
            display: none
        }

        .header-bar {
            background-color: #2f2f2f; 
            color: #fff;
            padding: 10px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header-title {
            font-size: 18px;
            color: #fff;
            margin-right: auto;
            text-transform: uppercase;
            font-weight: bold;
        }

        .header-bar .btn {
            padding: 8px 16px;
            border: none;
            cursor: pointer;
            border-radius: 4px;
            text-decoration: none;
            color: #fff;
        }

        .btn-anasayfa {
            background-color: #ff8c00; 
        }

        .btn-anasayfa:hover {
            background-color: #ffa733; 
        }

        .btn-cikis {
            background-color: #dc143c; 
        }

        .btn-cikis:hover {
            background-color: #e3425d; 
        }

         .etkinlikler-container {
            margin-top: 20px;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }

        .etkinlikler-container h1 {
            color: white;
            text-align: center;
            margin-bottom: 20px;
        }

        .etkinlik-kutusu {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 15px;
            margin: 10px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            background-color: #fff;
            transition: transform 0.2s;
        }

        .etkinlik-kutusu:hover {
            transform: translateY(-5px);
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
        }

        .etkinlik-baslik {
            font-size: 20px;
            font-weight: bold;
            color: #333;
        }

        .etkinlik-detay {
            font-size: 14px;
            color: #666;
            margin-top: 8px;
        }

        .etkinlik-butonlar {
            margin-top: 10px;
        }

        .etkinlik-butonlar button {
            margin-right: 10px;
        }

        .etkinlikler-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }

        .etkinlik-kutu-wrapper {
            flex: 1 1 calc(33.333% - 20px);
            margin: 10px;
            max-width: calc(33.333% - 20px);
        }

        @media (max-width: 768px) {
            .etkinlik-kutu-wrapper {
                flex: 1 1 calc(50% - 20px);
                max-width: calc(50% - 20px);
            }
        }

        @media (max-width: 576px) {
            .etkinlik-kutu-wrapper {
                flex: 1 1 100%;
                max-width: 100%;
            }
        }
    </style>
    <script>
        function Katila(etkinlikId) {
            fetch('KullaniciTumEtkinlikleriGoruntuleme.aspx/Katil', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ etkinlikId: etkinlikId })
            })
                .then(response => response.json())
                .then(data => {
                    alert(data.d);
                })
                .catch(error => {
                    console.error('Hata:', error);
                });
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="header-bar">
        <span class="header-title">Yönetici Tarafından Onaylanmış Etkinlikler</span>
        <a href="KullaniciSayfasi.aspx" class="btn btn-anasayfa">AnaSayfa</a>
        <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
    </div>

    <div class="etkinlikler-container" id="etkinliklerContainer" runat="server">
    </div>
</asp:Content>