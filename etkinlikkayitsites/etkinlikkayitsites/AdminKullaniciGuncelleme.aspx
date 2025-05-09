<%@ Page Title="Kullanıcı Güncelleme" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminKullaniciGuncelleme.aspx.cs" Inherits="etkinlikkayitsites.AdminKullaniciGuncelleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Kullanıcı Güncelleme</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
    <style>

        .navbar{display:none;}
      
        .header-column {
            background-color: #444;
            color: white;
            padding: 15px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-size: 18px;
        }

        .header-column h1 {
            margin: 0;
            font-size: 22px;
        }

        .header-buttons {
            display: flex;
            gap: 10px;
        }

        .btn-anasayfa {
            background-color: #ff7f00;
            color: white;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 5px;
        }

        .btn-anasayfa:hover {
            background-color: #e67300;
        }

        .btn-cikis {
            background-color: #d9534f;
            color: white;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 5px;
        }

        .btn-cikis:hover {
            background-color: #c9302c;
        }

       
        .form-container {
            max-width: 500px;
            margin: 50px auto;
            padding: 20px;
            border-radius: 10px;
            background-color: #ffffff;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        .form-container h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        .form-container label {
            font-weight: bold;
            color: #555;
        }

        .form-container .btn-pink {
            background-color: #ff69b4;
            color: #fff;
            border: none;
        }

        .form-container .btn-pink:hover {
            background-color: #ff85c3;
        }

        .image-preview {
            width: 100px;
            height: 100px;
            object-fit: cover;
            border-radius: 50%;
            margin-bottom: 10px;
            border: 1px solid #ddd;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <div class="header-column">
        <h1>KULLANICI BİLGİSİ GÜNCELLEME</h1>
        <div class="header-buttons">
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

  
    <div class="form-container">
        <h2>Kullanıcı Güncelleme</h2>

        <div class="mb-3">
            <label for="txtKullaniciAdi">Kullanıcı Adı:</label>
            <asp:TextBox ID="txtKullaniciAdi" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="txtSifre">Şifre:</label>
            <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="txtEposta">E-posta:</label>
            <asp:TextBox ID="txtEposta" runat="server" CssClass="form-control"></asp:TextBox>
        </div>


        <div class="mb-3">
            <label for="txtAd">Ad:</label>
            <asp:TextBox ID="txtAd" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="txtSoyad">Soyad:</label>
            <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="txtDogumTarihi">Doğum Tarihi:</label>
            <asp:TextBox ID="txtDogumTarihi" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label for="ddlCinsiyet">Cinsiyet:</label>
            <asp:DropDownList ID="ddlCinsiyet" runat="server" CssClass="form-select">
                <asp:ListItem Text="Erkek" Value="Erkek"></asp:ListItem>
                <asp:ListItem Text="Kadın" Value="Kadın"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="mb-3">
          <label for="txtKonum">Konum:</label>
          <asp:TextBox ID="txtKonum" runat="server" CssClass="form-control"></asp:TextBox>
           </div>

        <div class="mb-3">
            <label for="txtTelefon">Telefon:</label>
            <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3 text-center">
            <asp:Image ID="imgResim" runat="server" CssClass="image-preview" />
            <asp:FileUpload ID="fileUploadResim" runat="server" CssClass="form-control mt-2" />
        </div>

        <div class="mb-3">
            <label for="chkIlgiAlanlari">İlgi Alanları:</label>
            <asp:CheckBoxList ID="chkIlgiAlanlari" runat="server" CssClass="form-check">
            </asp:CheckBoxList>
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-success d-block mb-3"></asp:Label>

        <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn btn-pink w-100" OnClick="btnKaydet_Click" />
    </div>
</asp:Content>
