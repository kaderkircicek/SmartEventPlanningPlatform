<%@ Page Title="Rapor Görüntüleme" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="RaporGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.RaporGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .navbar {
            display: none;
        }
        .report-container {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            align-items: center;
            margin-top: 50px;
        }

        .report-header {
            margin-bottom: 20px;
            text-align: center;
        }

        .report-table {
            width: 80%;
            border-collapse: collapse;
            margin-top: 20px;
        }

       .report-table th,
        .report-table td {
         border: 1px solid #ddd;
          padding: 8px;
          text-align: center;
         width: 200px;
            }

        .report-table th {
            background-color: #4CAF50;
            color: white;
        }

        .report-table td {
            background-color: #f2f2f2;
        }

        .report-table-container {
            display: flex;
            justify-content: center;
            align-items: flex-start;
            width: 100%;
            margin-bottom: 30px;
        }

        .header-shadow {
            background-color: black;
            color: white;
            padding: 10px;
            width: 100%;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header-shadow h1 {
            margin: 0;
            font-size: 24px; 
        }

        .btn {
            padding: 8px 16px;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            font-size: 14px;
            font-weight: bold;
        }

        .btn-anasayfa {
            background-color: #FFA500; 
        }

        .btn-cikis {
            background-color: #FF6347; 
        }
        .report-header h2 {
    text-align: left; 
    margin-left: 20px; 
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header-shadow">
        <h1>SAYFA RAPORU GÖRÜNTÜLEME</h1>
        <div>
            <a href="AdminSayfasi.aspx" class="btn btn-anasayfa">Ana Sayfa</a>
            <a href="AnaSayfa.aspx" class="btn btn-cikis">Çıkış</a>
        </div>
    </div>

    <div class="report-container">
        <div class="report-header">
            <h2>Etkinlik Raporları</h2>
            <p><strong>Toplam Etkinlik Sayısı:</strong> <asp:Label ID="lblTotalEvents" runat="server" Text="0"></asp:Label></p>
        </div>

        <div class="report-table-container">
            <asp:GridView ID="gvEventReports" runat="server" AutoGenerateColumns="False" GridLines="None" BorderWidth="1px" CssClass="report-table">
                <Columns>
                    <asp:BoundField DataField="EtkinlikAdi" HeaderText="Etkinlik Adı" SortExpression="EtkinlikAdi" />
                    <asp:BoundField DataField="Tarih" HeaderText="Tarih" SortExpression="Tarih" />
                    <asp:BoundField DataField="Saat" HeaderText="Saat" SortExpression="Saat" />
                    <asp:BoundField DataField="EtkinlikBitis" HeaderText="Etkinlik Bitiş" SortExpression="EtkinlikBitis" />
                    <asp:BoundField DataField="Konum" HeaderText="Konum" SortExpression="Konum" />
                    <asp:BoundField DataField="KatilimciSayisi" HeaderText="Katılımcı Sayısı" SortExpression="KatilimciSayisi" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="report-header">
            <h2>Kullanıcı Raporları</h2>
            <p><strong>Kayıtlı Kullanıcı Sayısı:</strong> <asp:Label ID="lblTotalUsers" runat="server" Text="0"></asp:Label></p>
        </div>

        <div class="report-table-container">
            <asp:GridView ID="gvUserReports" runat="server" AutoGenerateColumns="False" GridLines="None" BorderWidth="1px" CssClass="report-table">
                <Columns>
                    <asp:BoundField DataField="KullaniciAdi" HeaderText="Kullanıcı Adı" SortExpression="KullaniciAdi" />
                    <asp:BoundField DataField="Eposta" HeaderText="Eposta" SortExpression="Eposta" />
                    <asp:BoundField DataField="Ad" HeaderText="Ad" SortExpression="Ad" />
                    <asp:BoundField DataField="Soyad" HeaderText="Soyad" SortExpression="Soyad" />
                    <asp:BoundField DataField="OlusturduguEtkinlikSayisi" HeaderText="Oluşturduğu Etkinlik Sayısı" SortExpression="OlusturduguEtkinlikSayisi" />
                    <asp:BoundField DataField="KatildigiEtkinlikSayisi" HeaderText="Katıldığı Etkinlik Sayısı" SortExpression="KatildigiEtkinlikSayisi" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
