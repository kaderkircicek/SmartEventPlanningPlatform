<%@ Page Title="Katıldığım Etkinlikler" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KullaniciKatildigiEtkinlikleriGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.KullaniciKatildigiEtkinlikleriGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        .navbar {
       display: none;
          }
       
        .header-column {
            background-color: #444; 
            color: white;
            padding: 15px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .header-column h1 {
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .button-container {
            display: flex;
            align-items: center;
        }

        .button-container button {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-left: 10px;
        }

        .button-container .anasayfa-btn {
            background-color: #ff7f00; 
            color: white;
        }

        .button-container .cikis-btn {
            background-color: #ff0000; 
            color: white;
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
        function KatilmaktanVazgec(etkinlikId) {
            if (confirm("Bu etkinlikten ayrılmak istediğinize emin misiniz?")) {
                fetch('KullaniciKatildigiEtkinlikleriGoruntuleme.aspx/KatilmaktanVazgec', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ etkinlikId: etkinlikId })
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.d === true) {
                            alert("Etkinlikten başarıyla ayrıldınız.");
                            location.reload();
                        } else {
                            alert("Bir hata oluştu. Lütfen tekrar deneyin.");
                        }
                    })
                    .catch(err => {
                        alert("Bağlantı sırasında bir hata oluştu.");
                    });
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header-column">
        <h1>KATILDIĞIN ETKİNLİKLER</h1>
        <div class="button-container">

            <a href="KullaniciSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>


    <div class="etkinlikler-container" id="etkinliklerContainer" runat="server">
    </div>
</asp:Content>
