<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KullaniciEtkinlikOneri.aspx.cs" Inherits="etkinlikkayitsites.KullaniciEtkinlikOneri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Etkinlik Önerileri</title>
   
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;600&display=swap" rel="stylesheet">
    
    <style>
        body {
            font-family: 'Poppins', sans-serif;
        }

        .navbar {
            display: none;
        }

        .content-header {
            text-align: center;
            margin-bottom: 30px;
            font-size: 28px;
            font-weight: 600;
            color: #3a3a3a;
        }

        .table thead {
            background-color: #007bff;
            color: white;
        }

        .table-striped tbody tr:nth-child(odd) {
            background-color: #f2f2f2;
        }

        .table-bordered {
            border: 1px solid #dee2e6;
        }

        .table td, .table th {
            padding: 15px;
            text-align: left;
        }

        .text-danger {
            font-size: 1.2rem;
            text-align: center;
        }

        .grid-wrapper {
            margin: 30px;
        }

        .card {
            border: 0;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .no-data-message {
            background-color: #ffcccb;
            padding: 20px;
            border-radius: 5px;
            margin-top: 20px;
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="content-header">Size Önerilen Etkinlikler</h2>

       
        <div class="grid-wrapper">
            <asp:GridView ID="gvOneriler" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped shadow-sm">
                <Columns>
                    <asp:BoundField DataField="EtkinlikAdi" HeaderText="Etkinlik Adı" SortExpression="EtkinlikAdi" />
                    <asp:BoundField DataField="KategoriAdi" HeaderText="Kategori" SortExpression="KategoriAdi" />
                    <asp:BoundField DataField="Konum" HeaderText="Konum" SortExpression="Konum" />
                    <asp:BoundField DataField="OnerilmeNedeni" HeaderText="Önerilme Nedeni" SortExpression="OnerilmeNedeni" />
                </Columns>
            </asp:GridView>
            
            
            <div id="noDataMessage" class="no-data-message">
                <p><strong>Üzgünüz!</strong> Şu an önerilen etkinlikler bulunmamaktadır.</p>
            </div>
        </div>
    </div>

    <asp:Label ID="lblNoData" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
</asp:Content>

