<%@ Page Title="Etkinlik Detayları" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="EtkinlikDetay.aspx.cs" Inherits="etkinlikkayitsites.EtkinlikDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: ghostwhite; 
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .event-container {
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            max-width: 500px;
            width: 90%;
            padding: 20px;
            text-align: center;
            color: #333;
        }

        .event-header {
            color: #ff4d88;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .event-detail {
            margin: 10px 0;
            font-size: 16px;
        }

        .event-detail strong {
            color: #ff4d88;
        }

        .error-message {
            color: #ff4d88; 
            margin-top: 15px;
        }

        .navbar{display:none}
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="event-container">
        <div class="event-header">Etkinlik Detayları</div>
        <asp:Panel ID="pnlEventDetails" runat="server" Visible="false">
            <p class="event-detail"><strong>Etkinlik Adı:</strong> <asp:Label ID="lblEtkinlikAdi" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Açıklama:</strong> <asp:Label ID="lblAciklama" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Tarih:</strong> <asp:Label ID="lblTarih" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Başlangıç Saati:</strong> <asp:Label ID="lblSaat" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Bitiş Saati:</strong> <asp:Label ID="lblBitisSaati" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Kategori:</strong> <asp:Label ID="lblKategori" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Konum:</strong> <asp:Label ID="lblKonum" runat="server"></asp:Label></p>
            <p class="event-detail"><strong>Onay Durumu:</strong> <asp:Label ID="lblOnayDurumu" runat="server"></asp:Label></p>
        </asp:Panel>

        <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error-message"></asp:Label>
    </div>
</asp:Content>

