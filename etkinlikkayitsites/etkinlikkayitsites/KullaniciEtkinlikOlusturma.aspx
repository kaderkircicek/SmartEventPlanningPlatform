<%@ Page Title="Etkinlik Oluştur" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KullaniciEtkinlikOlusturma.aspx.cs" Inherits="etkinlikkayitsites.KullaniciEtkinlikOlusturma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
      
        .header {
            background-color: rgba(0, 0, 0, 0.6); 
            color: white;
            padding: 20px;
            text-align: center;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header h1 {
            margin: 0;
            font-size: 24px;
        }

        
        .header .button-container {
            display: flex;
            gap: 10px;
        }

        .header .btn-home, .header .btn-logout {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            color: white;
            font-size: 16px;
            cursor: pointer;
        }

        .header .btn-home {
            background-color: #ff8c00; 
        }

        .header .btn-logout {
            background-color: #f44336;
        }

        .navbar {
            display: none;
        }

        
        .container1 {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding-top: 0px; 
        }

       
        .form-container {
            display: flex;
            width: 100%;
            max-width: 800px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
            overflow: hidden;
            background-color: #ffffff;
        }

        
        .form-section {
            padding: 30px;
            width: 50%;
            background-color: #f9f9f9;
        }

       
        label {
            color: #e91e63;
            font-weight: bold;
            display: block;
            margin: 15px 0 5px;
        }

        input[type="text"],
        input[type="date"],
        input[type="time"],
        input[type="number"],
        textarea,
        select {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }

        input:focus, textarea:focus, select:focus {
            border-color: #e91e63;
            outline: none;
        }

        .btn {
            display: block;
            width: 100%;
            background-color: #e91e63;
            color: #ffffff;
            padding: 12px;
            font-size: 16px;
            font-weight: bold;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #d81b60;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnCakismaOnayi" runat="server" Value="0" />
    <script type="text/javascript">
        function ConfirmKaydet(mesaj) {
            if (confirm(mesaj)) {
                document.getElementById('<%= hdnCakismaOnayi.ClientID %>').value = "1";
                __doPostBack('<%= btnEtkinlikOlustur.UniqueID %>', '');
            }
        }
    </script>

  
    <div class="header">
        <h1>ETKİNLİK OLUŞTUR</h1>
        <div class="button-container">
          
             <a href="KullaniciSayfasi.aspx" class="btn btn-anasayfa">AnaSayfa</a>
             <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

  
    <div class="container1">
        <div class="form-container">
          
            <div class="form-section">
                <asp:Label ID="lblUyari" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblEtkinlikAdi" runat="server" Text="Etkinlik Adı:" AssociatedControlID="txtEtkinlikAdi" />
                <asp:TextBox ID="txtEtkinlikAdi" runat="server" MaxLength="50" />

                <asp:Label ID="lblAciklama" runat="server" Text="Açıklama:" AssociatedControlID="txtAciklama" />
                <asp:TextBox ID="txtAciklama" runat="server" TextMode="MultiLine" Rows="5" />

                <asp:Label ID="lblTarih" runat="server" Text="Tarih:" AssociatedControlID="txtTarih" />
                <asp:TextBox ID="txtTarih" runat="server" TextMode="Date" />
            </div>

            
            <div class="form-section">
                <asp:Label ID="lblSaat" runat="server" Text="Saat:" AssociatedControlID="txtSaat" />
                <asp:TextBox ID="txtSaat" runat="server" TextMode="Time" />

                <asp:Label ID="lblEtkinlikSuresi" runat="server" Text="Etkinlik Süresi (dakika):" AssociatedControlID="txtEtkinlikSuresi" />
                <asp:TextBox ID="txtEtkinlikSuresi" runat="server" TextMode="Number" />

                <asp:Label ID="lblKonum" runat="server" Text="Konum:" AssociatedControlID="txtKonum" />
                <asp:TextBox ID="txtKonum" runat="server" MaxLength="100" />

                <asp:Label ID="lblKategori" runat="server" Text="Kategori:" AssociatedControlID="ddlKategori" />
                <asp:DropDownList ID="ddlKategori" runat="server">
                </asp:DropDownList>

                <asp:Button ID="btnEtkinlikOlustur" runat="server" Text="Etkinlik Oluştur" CssClass="btn" OnClick="btnEtkinlikOlustur_Click" />
                <asp:Label ID="lblSonuc" runat="server" CssClass="success-message"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
