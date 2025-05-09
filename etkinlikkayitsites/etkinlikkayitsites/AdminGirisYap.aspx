<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminGirisYap.aspx.cs" Inherits="etkinlikkayitsites.AdminGirisYap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-container {
            max-width: 500px; 
            margin: 100px auto; 
            padding: 40px; 
            background-color: #fff;
            border-radius: 15px; 
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1); 
        }
        .login-container h2 {
            font-size: 2.5rem;
            text-align: center;
            margin-bottom: 30px; 
        }
        .login-container .form-control {
            border-radius: 10px; 
            font-size: 1.25rem; 
            padding: 15px; 
        }
        .login-container .btn {
            width: 100%;
            border-radius: 10px;
            font-size: 1.25rem;
            padding: 15px; 
        }
     
        .btn-secondary {
            padding: 10px; 
        }
        .form-label {
            font-size: 1.25rem; 
        }
        .invalid-feedback {
            color: #dc3545; 
            display: none; 
        }
        .was-validated .form-control:invalid {
            border-color: #dc3545; 
        }
        .was-validated .form-control:invalid ~ .invalid-feedback {
            display: block; 
        }
       
        .btn-spacing {
            margin-top: 15px; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <h2>Yönetici Girişi</h2>
        <asp:PlaceHolder runat="server" ID="PlaceHolder1">
            <div class="mb-3">
                <label for="username" class="form-label">Kullanıcı Adı</label>
                <asp:TextBox ID="username" CssClass="form-control" runat="server" Placeholder="Kullanıcı adınızı girin"></asp:TextBox>
                <div class="invalid-feedback">Lütfen kullanıcı adınızı girin.</div>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Şifre</label>
                <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server" Placeholder="Şifrenizi girin"></asp:TextBox>
                <div class="invalid-feedback">Lütfen şifrenizi girin.</div>
            </div>
            <div class="mb-3">
                <asp:CheckBox ID="chkRememberMe" runat="server" Text="Beni Hatırla" />
            </div>
            <div class="d-grid btn-spacing">
                <asp:Button ID="btnSifreGuncelleme" runat="server" Text="Şifremi Unuttum" OnClick="btnSifreGuncelleme_Click" CssClass="btn btn-secondary" />
            </div>
            <div class="d-grid">
                <asp:Button ID="btnRegister" runat="server" Text="Giriş Yap" OnClick="btnRegister_Click" CssClass="btn btn-primary" />
            </div>
        </asp:PlaceHolder>
    </div>
    <script>

        (function () {
            'use strict';
            var forms = document.querySelectorAll('.needs-validation');
            Array.prototype.slice.call(forms).forEach(function (form) {
                form.addEventListener('submit', function (event) {
                    if (!form.checkValidity()) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        })();
    </script>
</asp:Content>
