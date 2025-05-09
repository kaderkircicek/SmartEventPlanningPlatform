<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminKullaniciGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.AdminKullaniciGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .navbar{display:none}
        
        .center-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 80vh;
            flex-direction: column;
            padding: 20px;
        }

       
        .page-title {
            font-size: 24px;
            font-weight: bold;
            color: #333;
            margin-bottom: 20px; 
        }

       
        .user-table {
            width: 100%;
            max-width: 1200px;
            border-collapse: collapse;
            background-color: #f9f9f9;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .user-table th, .user-table td {
            padding: 12px;
            text-align: left;
            border: 1px solid #ddd;
        }

        .user-table th {
            background-color: #ff69b4; 
            color: white;
        }

        .user-table tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .btn-guncelle, .btn-sil {
            padding: 8px 16px;
            margin: 4px;
            cursor: pointer;
            border: none;
            border-radius: 4px;
            font-size: 14px;
        }

        .btn-guncelle {
            background-color: #ff69b4; 
            color: white;
        }

        .btn-guncelle:hover {
            background-color: #ff1493;
        }

        .btn-sil {
            background-color: #dc3545;
            color: white;
        }

        .btn-sil:hover {
            background-color: #c82333;
        }

        
        .top-bar {
            background-color: black; 
            padding: 15px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .top-bar .page-title-bar {
            color: white;
            font-size: 20px;
            font-weight: bold;
        }

        .top-bar .btn-anasayfa, .top-bar .btn-cikis {
            padding: 10px 20px;
            font-size: 14px;
            color: white;
            text-decoration: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .top-bar .btn-anasayfa {
            background-color: #f39c12; 
        }

        .top-bar .btn-anasayfa:hover {
            background-color: #e67e22; 
        }

        .top-bar .btn-cikis {
            background-color: #e74c3c; 
        }

        .top-bar .btn-cikis:hover {
            background-color: #c0392b; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div class="top-bar">
        <div class="page-title-bar">KULLANICILARI GÖRÜNTÜLE</div>
        <div>
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

    <div class="center-container">
    
        <h2 class="page-title">Kullanıcıları Görüntüle</h2>
        
        <asp:GridView ID="gvKullanicilar" runat="server" AutoGenerateColumns="False" OnRowCommand="gvKullanicilar_RowCommand" CssClass="user-table">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="KullaniciAdi" HeaderText="Kullanıcı Adı" SortExpression="KullaniciAdi" />
                <asp:BoundField DataField="Eposta" HeaderText="E-posta" SortExpression="Eposta" />
                <asp:BoundField DataField="Ad" HeaderText="Ad" SortExpression="Ad" />
                <asp:BoundField DataField="Soyad" HeaderText="Soyad" SortExpression="Soyad" />
                <asp:BoundField DataField="TelefonNumarası" HeaderText="Telefon Numarası" SortExpression="TelefonNumarasi" />
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Guncelle" CommandArgument='<%# Eval("ID") %>' Text="Güncelle" OnClick="btnGuncelle_Click" CssClass="btn-guncelle" />
                        <asp:Button runat="server" CommandName="Sil" CommandArgument='<%# Eval("ID") %>' Text="Sil" OnClick="btnSil_Click" CssClass="btn-sil" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
