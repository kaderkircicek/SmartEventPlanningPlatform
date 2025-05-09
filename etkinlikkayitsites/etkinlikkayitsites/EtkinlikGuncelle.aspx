<%@ Page Title="Etkinlik Güncelle" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="EtkinlikGuncelle.aspx.cs" Inherits="etkinlikkayitsites.EtkinlikGuncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        .navbar{display:none;}
      
        .header-container {
            background-color: black;
            color: white;
            padding: 15px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header-container .header-title {
            font-size: 24px;
            font-weight: bold;
        }

        .header-container .btn {
            padding: 10px 15px;
            border-radius: 8px;
            font-size: 16px;
            color: white;
            text-decoration: none;
            display: inline-block;
            margin-left: 10px;
        }

        .btn-anasayfa {
            background-color: #ff8c00;
        }

        .btn-cikis {
            background-color: #e57373;
        }

        body {
            font-family: Arial, sans-serif;
        }

        .form-container {
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #f3c6e8;
            border-radius: 12px;
            background-color: #fff;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
        }

        .form-container label {
            font-weight: bold;
            color: #a64d79;
            display: block;
            margin-bottom: 5px;
        }

        .form-container input,
        .form-container select,
        .form-container textarea {
            width: 100%;
            padding: 12px;
            margin-bottom: 15px;
            border: 1px solid #e0b3d3;
            border-radius: 8px;
            background-color: #fdf2f7;
            font-size: 14px;
        }

        .form-container input:focus,
        .form-container select:focus,
        .form-container textarea:focus {
            border-color: #d480ac;
            outline: none;
            box-shadow: 0 0 4px rgba(214, 128, 172, 0.5);
        }

        .form-container button {
            width: 100%;
            padding: 12px;
            background-color: #e57399;
            color: #fff;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .form-container button:hover {
            background-color: #cc5279;
        }

        .form-container .message {
            color: #cc5279;
            font-weight: bold;
            margin-bottom: 15px;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="header-container">
        <div class="header-title">ETKİNLİK GÜNCELLEME</div>
        <div>
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

   
    <div class="form-container">
        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
        
        <label for="txtEtkinlikAdi">Etkinlik Adı</label>
        <asp:TextBox ID="txtEtkinlikAdi" runat="server" CssClass="form-control"></asp:TextBox>
        
        <label for="txtAciklama">Açıklama</label>
        <asp:TextBox ID="txtAciklama" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
        
        <label for="txtTarih">Tarih</label>
        <asp:TextBox ID="txtTarih" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        
        <label for="txtSaat">Saat</label>
        <asp:TextBox ID="txtSaat" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
        
        <label for="txtSure">Etkinlik Süresi (dakika)</label>
        <asp:TextBox ID="txtSure" runat="server" CssClass="form-control"></asp:TextBox>
        
        <label for="txtKonum">Konum</label>
        <asp:TextBox ID="txtKonum" runat="server" CssClass="form-control"></asp:TextBox>
        
        <label for="ddlKategori">Kategori</label>
        <asp:DropDownList ID="ddlKategori" runat="server" CssClass="form-control">
        </asp:DropDownList>
        
        <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn" OnClick="btnKaydet_Click" />
    </div>
</asp:Content>
