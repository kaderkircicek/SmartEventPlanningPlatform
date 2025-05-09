<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminTumEtkinlikleriGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.AdminTumEtkinlikleriGoruntuleme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .navbar
        {display:none}

        .header-bar {
            background-color: #2d2d2d; 
            color: white;
            padding: 15px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
        }

        .header-bar h1 {
            margin: 0;
            font-size: 24px;
        }

        .header-bar .btn {
            padding: 10px 15px;
            border-radius: 5px;
            text-decoration: none;
            font-size: 14px;
            color: white;
        }

        .btn-anasayfa {
            background-color: orange;
            border: 1px solid orange;
        }

        .btn-anasayfa:hover {
            background-color: #e69500;
            border-color: #e69500;
        }

        .btn-cikis {
            background-color: red;
            border: 1px solid red;
            margin-left: 10px;
        }

        .btn-cikis:hover {
            background-color: #b30000;
            border-color: #b30000;
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

        .etkinlik-butonlar {
         margin-top: 10px;
         display: flex;
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
              
                fetch('AdminTumEtkinlikleriGoruntuleme.aspx/SilEtkinlik', {
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


        function Katila(etkinlikId) {
            fetch('AdminTumEtkinlikleriGoruntuleme.aspx/Katil', {
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
        <h1>TÜM ETKİNLİKLER</h1>
        <div>
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

    <div class="etkinlikler-container" id="etkinliklerContainer" runat="server">
    </div>
</asp:Content>

