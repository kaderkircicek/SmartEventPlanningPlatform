<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KullaniciOlusturduguEtkinlikleriGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.KullaniciOlusturduguEtkinlikleriGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
     

          .navbar {
                display: none
            }

        .header-bar {
            background-color: rgba(50, 50, 50, 0.8); 
            color: white;
            padding: 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header-bar h1 {
            font-size: 24px;
            margin: 0;
        }

        .header-bar .buttons {
            display: flex;
            gap: 10px;
        }

        .header-bar button {
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .header-bar .btn-anasayfa {
            background-color: #FFA500; 
            color: white;
        }

        .header-bar .btn-cikis {
            background-color: #FF0000;
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
        function Sil(etkinlikId) {
            if (confirm("Bu etkinliği silmek istediğinize emin misiniz?")) {
              
                fetch('KullaniciOlusturduguEtkinlikleriGoruntuleme.aspx/SilEtkinlik', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ etkinlikId: etkinlikId })
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.d === true) {
                            alert("Etkinlik başarıyla silindi.");
                            location.reload(true);
                        } else {
                            alert("Etkinlik silinirken bir hata oluştu.");
                        }
                    })
                    .catch(error => {
                        console.error('Hata:', error);
                        alert("Bir hata oluştu.");
                    });
            }
        }
    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="header-bar">
        <h1>OLUŞTURDUĞUN ETKİNLİKLER</h1>
        <div class="buttons">
            <a href="KullaniciSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
           <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

   
    <div class="etkinlikler-container" id="etkinliklerContainer" runat="server">
       
    </div>
</asp:Content>
