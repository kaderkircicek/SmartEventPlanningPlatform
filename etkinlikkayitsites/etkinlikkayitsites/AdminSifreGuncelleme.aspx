<%@ Page Title="" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="AdminSifreGuncelleme.aspx.cs" Inherits="etkinlikkayitsites.AdminSifreGuncelleme" EnableEventValidation="false"%>
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
        .login-container .btn-primary {
            width: 100%;
            border-radius: 10px;
            font-size: 1.25rem;
            padding: 15px; 
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <h2>ŞİFRE GÜNCELLEME</h2>
        
       
        <form class="needs-validation" method="post" novalidate>
            <asp:PlaceHolder runat="server" ID="PlaceHolder1">
              
                <div class="mb-3">
                    <label for="username" class="form-label">Kullanıcı Adı</label>
                    <asp:TextBox ID="username" CssClass="form-control" runat="server" Placeholder="Kullanıcı adını giriniz" required></asp:TextBox>
                     <div class="invalid-feedback">Lütfen kullanıcı adınızı girin.</div>
                </div>

                <div class="mb-3">
                    <label for="emal" class="form-label">Mail Adresi</label>
                    <asp:TextBox ID="email" CssClass="form-control" runat="server" Placeholder="Mail adresini girin" required></asp:TextBox>
                    <div class="invalid-feedback">Lütfen geçerli mail adresini girin.</div>
                </div>
                
                <div class="mb-3">
                    <label for="newPassword" class="form-label">Yeni Şifre</label>
                    <asp:TextBox ID="newPassword" TextMode="Password" CssClass="form-control" runat="server" Placeholder="Yeni şifrenizi girin"></asp:TextBox>
                   <div class="invalid-feedback">Lütfen yeni şifrenizi girin.</div>
                      </div>

               
                <asp:Button ID="btnSifreGuncelleme" CssClass="btn btn-primary" runat="server" Text="Şifreyi Güncelle" OnClick="btnSifreGuncelleme_Click" />
            </asp:PlaceHolder>
        </form>
   

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
    </div>
</asp:Content>