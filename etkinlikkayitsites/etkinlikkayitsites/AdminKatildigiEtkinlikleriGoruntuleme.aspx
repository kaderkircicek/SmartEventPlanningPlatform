<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminKatildigiEtkinlikleriGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.AdminKatildigiEtkinlikleriGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .navbar{display:none;}
  
        .header-container {
            background-color: black;
            color: white;
            padding: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }

        .header-container h2 {
            margin: 0;
            font-size: 24px;
        }

      
        .btn-anasayfa, .btn-cikis {
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            color: white;
            font-weight: bold;
        }

        .btn-anasayfa {
            background-color: orange;
        }

        .btn-anasayfa:hover {
            background-color: #ff7f32; 
        }

        .btn-cikis {
            background-color: red; 
        }

        .btn-cikis:hover {
            background-color: #ff4d4d; 
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
        function KatilmaktanVazgec(etkinlikId) {
            if (confirm("Bu etkinlikten ayrılmak istediğinize emin misiniz?")) {
                fetch('AdminKatildigiEtkinlikleriGoruntuleme.aspx/KatilmaktanVazgec', {
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
 
    <div class="header-container">
        <h2>KATILDIĞIN ETKİNLİKLERİ GÖRÜNTÜLEME</h2>
        <div>
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>


    <div class="etkinlikler-container" id="etkinliklerContainer" runat="server">
    </div>
</asp:Content>
