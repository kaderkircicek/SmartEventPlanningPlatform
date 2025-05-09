<%@ Page Title="Sohbeti Görüntüle" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="SohbetiGoruntule.aspx.cs" Inherits="etkinlikkayitsites.SohbetiGoruntule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       
        body {
            background-color: #f4f4f4;
            font-family: Arial, sans-serif;
        }

       
        .chat-panel {
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            width: 70%;
            margin: 50px auto;
            max-width: 800px;
        }

        .chat-header {
            text-align: center;
            margin-bottom: 20px;
        }

       
        .message-list {
            list-style: none;
            padding-left: 0;
            max-height: 300px;
            overflow-y: auto;
            margin-bottom: 20px;
            border-bottom: 1px solid #ddd;
        }

        .message-item {
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
            background-color: #f9f9f9;
        }

        .message-item strong {
            color: #007bff;
        }

      
        .message-box {
            width: 100%;
            padding: 10px;
            border-radius: 5px;
            border: 1px solid #ddd;
            margin-bottom: 15px;
        }

      
        .send-button {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
        }

        .send-button:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="chat-panel">
        <div class="chat-header">
            <h2>Sohbeti Görüntüle</h2>
        </div>

      
        <ul class="message-list">
    <asp:Repeater ID="rptMesajlar" runat="server">
        <ItemTemplate>
            <li class="message-item">
                <strong><%# Eval("KullaniciAdi") %>:</strong> <%# Eval("MesajMetni") %>
                <br />
                <small style="color: gray;"><%# Eval("GönderimZamani", "{0:dd.MM.yyyy HH:mm:ss}") %></small>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>

        
        <asp:TextBox ID="txtMesaj" runat="server" TextMode="MultiLine" Rows="3" CssClass="message-box"></asp:TextBox>
        <asp:Button ID="btnGonder" runat="server" Text="Mesaj Gönder" OnClick="btnGonder_Click" CssClass="send-button" />
    </div>
</asp:Content>
