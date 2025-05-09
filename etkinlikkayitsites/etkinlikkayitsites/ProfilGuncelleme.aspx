<%@ Page Title="Profil Güncelleme" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="ProfilGuncelleme.aspx.cs" Inherits="etkinlikkayitsites.ProfilGuncelleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Profil Güncelleme</title>
    <style>
       
        .navbar{display:none;}
        .form-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

      
        .header-container {
            background-color: black;
            color: white;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px 20px;
        }

        .header-container .header-title {
            font-size: 24px;
            font-weight: bold;
        }

        .header-container .btn {
            color: white;
            padding: 8px 16px;
            border-radius: 4px;
            text-decoration: none;
            font-size: 16px;
        }

        .btn-anasayfa {
            background-color: orange;
        }

        .btn-anasayfa:hover {
            background-color: #e65100;
        }

        .btn-cikis {
            background-color: red;
        }

        .btn-cikis:hover {
            background-color: #c62828;
        }

        .form-header {
            text-align: center;
            font-size: 24px;
            font-weight: bold;
            color: #333;
            margin-bottom: 20px;
        }

        .form-content {
            display: flex;
            width: 100%;
            flex-wrap: wrap;
        }

        .form-left, .form-right {
            width: 100%;
            padding: 10px;
            margin-bottom: 20px;
        }

        label {
            color: #e91e63;
            font-weight: bold;
            margin-top: 10px;
            display: block;
            text-align: left;
        }

        input[type="text"],
        input[type="password"],
        input[type="date"],
        input[type="tel"],
        select,
        textarea {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
        }

        .button-container {
            text-align: center;
            margin-top: 20px;
        }

        .button-container button {
            background-color: #e91e63;
            color: white;
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .button-container button:hover {
            background-color: #d81b60;
        }

       
        .interest-container {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            justify-content: flex-start;
        }

        .interest-container .checkbox-item {
            width: 30%; 
        }

       
        .checkbox-list-item {
            width: 100%;
            display: inline-block;
            margin-bottom: 10px;
        }

        
        .profil-image-container {
            text-align: center;
            margin-bottom: 20px;
        }

        
        .no-space-between {
            margin-top: 0;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="header-container">
        <div class="header-title">PROFİL GÜNCELLEME</div>
        <div>
            <a href="KullaniciSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

    <div class="form-container">
        <div class="form-header">Profil Güncelleme</div>

       
        <div class="profil-image-container">
            <asp:Image ID="ProfilImage" runat="server" Width="150px" Height="150px" />
            <asp:Label ID="lblResim" Text="Profil Resmi:" AssociatedControlID="fuResim" runat="server" />
            <asp:FileUpload ID="fuResim" runat="server" />
        </div>

        <div class="form-content">
            
            <div class="form-left">
                <asp:Label ID="lblIlgiAlanlari" Text="İlgi Alanları:" runat="server" />
                <div class="interest-container">
                    <asp:CheckBoxList ID="chkIlgiAlanlari" runat="server" CssClass="checkbox-list">
                    </asp:CheckBoxList>
                </div>
            </div>

           
            <div class="form-right">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <asp:Label ID="lblKullaniciAdi" Text="Kullanıcı Adı:" AssociatedControlID="txtKullaniciAdi" runat="server" />
                <asp:TextBox ID="txtKullaniciAdi" runat="server" Enabled="false" />

                <asp:Label ID="lblPuanDurumu" Text="Puan Durumu:" AssociatedControlID="txtPuanDurumu" runat="server" />
                <asp:TextBox ID="txtPuanDurumu" runat="server" Enabled="false" />

                <asp:Label ID="lblPuanTarihi" Text="Puanın Kazanıldığı Tarih:" AssociatedControlID="txtPuanTarihi" runat="server" />
                <asp:TextBox ID="txtPuanTarihi" runat="server" Enabled="false" />

                <asp:Label ID="lblKonum" Text="Konum:" AssociatedControlID="txtKonum" runat="server" />
                <asp:TextBox ID="txtKonum" runat="server" />

                <asp:Label ID="lblSifre" Text="Şifre:" AssociatedControlID="txtSifre" runat="server" />
                <asp:TextBox ID="txtSifre" TextMode="Password" runat="server" />

                <asp:Label ID="lblEposta" Text="E-posta:" AssociatedControlID="txtEposta" runat="server" />
                <asp:TextBox ID="txtEposta" runat="server" />

                <asp:Label ID="lblAd" Text="Ad:" AssociatedControlID="txtAd" runat="server" />
                <asp:TextBox ID="txtAd" runat="server" />

                <asp:Label ID="lblSoyad" Text="Soyad:" AssociatedControlID="txtSoyad" runat="server" />
                <asp:TextBox ID="txtSoyad" runat="server" />

                <asp:Label ID="lblDogumTarihi" Text="Doğum Tarihi:" AssociatedControlID="txtDogumTarihi" runat="server" />
                <asp:TextBox ID="txtDogumTarihi" runat="server" TextMode="Date" />

                <asp:Label ID="lblCinsiyet" Text="Cinsiyet:" AssociatedControlID="ddlCinsiyet" runat="server" />
                <asp:DropDownList ID="ddlCinsiyet" runat="server">
                    <asp:ListItem Text="Seçiniz" Value="" />
                    <asp:ListItem Text="Erkek" Value="Erkek" />
                    <asp:ListItem Text="Kadın" Value="Kadın" />
                </asp:DropDownList>

                <asp:Label ID="lblTelefonNumarasi" Text="Telefon Numarası:" AssociatedControlID="txtTelefonNumarasi" runat="server" />
                <asp:TextBox ID="txtTelefonNumarasi" runat="server" />
            </div>
        </div>

       
        <div class="button-container">
            <asp:Button ID="btnKaydet" Text="Kaydet" OnClick="btnKaydet_Click" runat="server" />
        </div>
    </div>
</asp:Content>
