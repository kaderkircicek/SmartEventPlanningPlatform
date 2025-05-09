<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="KayıtOl.aspx.cs" Inherits="etkinlikkayitsites.KayıtOl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .register-container {
        max-width: 600px;
        margin: 50px auto;
        padding: 20px;
        background-color: #fff;
        border-radius: 10px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }
    .register-container h2 {
        text-align: center;
        margin-bottom: 20px;
    }
    .register-container .form-control {
        border-radius: 5px;
    }
    .register-container .btn-primary {
        width: 100%;
        border-radius: 5px;
    }
    .invalid-feedback {
        display: none;
        color: red;
    }
    .invalid-feedback.show {
        display: block;
    }
     .success-message {
            color: green;
            font-weight: bold;
            text-align: center;
            margin-top: 20px;
        }
       
        .btn-register {
            width: 100%;
            border-radius: 5px;
            background-color: #ff66b2;
            color: white;
            border: none;
            padding: 10px;
        }
        .btn-home {
            width: 100%;
            border-radius: 5px;
            background-color: #ff8c00; 
            color: white;
            border: none;
            padding: 10px;
            margin-top: 10px;
        }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="register-container">
        <h2>Kayıt Ol</h2>
        <asp:PlaceHolder runat="server" ID="PlaceHolder1">
            <div class="mb-3">
                <label for="firstName" class="form-label">Ad</label>
                <asp:TextBox ID="firstName" CssClass="form-control" runat="server" Placeholder="Adınızı girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen adınızı girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="lastName" class="form-label">Soyad</label>
                <asp:TextBox ID="lastName" CssClass="form-control" runat="server" Placeholder="Soyadınızı girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen soyadınızı girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <asp:TextBox ID="email" CssClass="form-control" runat="server" Placeholder="E-posta adresinizi girin"></asp:TextBox>
                <div class="invalid-feedback" id="emailFeedback">
                    Lütfen geçerli bir email adresi girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="username" class="form-label">Kullanıcı Adı</label>
                <asp:TextBox ID="username" CssClass="form-control" runat="server" Placeholder="Kullanıcı adınızı girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen kullanıcı adınızı girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Şifre</label>
                <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server" Placeholder="Şifrenizi girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen şifrenizi girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="Konum" class="form-label">Konum</label>
                <asp:TextBox ID="konum" CssClass="form-control" runat="server" Placeholder="Konum"></asp:TextBox>
                 <div class="invalid-feedback">
                    Lütfen konumunuzu giriniz.
                 </div>
                </div>
            <div class="mb-3">
            <label for="birthDate" class="form-label">Doğum Tarihi</label>
              <asp:TextBox ID="birthDate" CssClass="form-control" runat="server" Placeholder="GG/AA/YYYY"></asp:TextBox>
              <div class="invalid-feedback">Lütfen doğum tarihinizi girin.</div>
                </div>
            <div class="mb-3">
                <label for="gender" class="form-label">Cinsiyet</label>
                <asp:DropDownList ID="gender" CssClass="form-control" runat="server">
                    <asp:ListItem Text="Seçiniz" Value=""></asp:ListItem>
                    <asp:ListItem Text="Erkek" Value="Erkek"></asp:ListItem>
                    <asp:ListItem Text="Kadın" Value="Kadın"></asp:ListItem>
                </asp:DropDownList>
                <div class="invalid-feedback">Lütfen cinsiyetinizi seçin.</div>
            </div>
            <div class="mb-3">
                <label for="phoneNumber" class="form-label">Telefon Numarası</label>
                <asp:TextBox ID="phoneNumber" CssClass="form-control" runat="server" Placeholder="Telefon numaranızı girin"></asp:TextBox>
                <div class="invalid-feedback">Lütfen geçerli bir telefon numarası girin.</div>
           
            <div class="d-grid">
                <asp:Button ID="btnRegister" runat="server" Text="Kayıt Ol" OnClick="btnRegister_Click" OnClientClick="return validateForm();" />
            </div>
                <div class="d-grid">
                <asp:Button ID="btnHome" runat="server" Text="Ana Sayfa" CssClass="btn-home" OnClick="RedirectHome" />
            </div>
            <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <script>
        function validateForm() {
            var email = document.getElementById('<%= email.ClientID %>');
            var emailFeedback = document.getElementById('emailFeedback');

            if (!email.value.includes('@')) {
                emailFeedback.classList.add('show');
                return false;
            } else {
                emailFeedback.classList.remove('show');
                return true;
            }
        }
    </script>
</asp:Content>
