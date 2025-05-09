<%@ Page Title="Etkinlik Haritası" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="HaritadaEtkinlikGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.HaritadaEtkinlikGoruntuleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDL0UrNYNAON-a4yBZRiegXByzuHMu0A4g"></script>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            haritayiBaslat();
        });

        function haritayiBaslat() {
            try {
                // Harita ayarları
                var haritaAyarlar = {
                    center: { lat: 39.92077, lng: 32.85411 }, // Türkiye'nin merkez noktası
                    zoom: 6
                };

                var harita = new google.maps.Map(document.getElementById('harita-alani'), haritaAyarlar);

                // Konumları JSON formatında al
                var konumlarElemani = document.getElementById('<%= gizliAlanKonumlar.ClientID %>');
                if (!konumlarElemani) {
                    mesajGoster('Harita yüklenirken bir hata oluştu: Konum verileri bulunamadı.', 'error');
                    return;
                }

                var konumlar = JSON.parse(konumlarElemani.value);
                if (!konumlar || konumlar.length === 0) {
                    mesajGoster('Haritada gösterilecek etkinlik bulunamadı.', 'info');
                    return;
                }

                konumlar.forEach(function (konum) {
                    var cozumleyici = new google.maps.Geocoder();
                    cozumleyici.geocode({ 'address': konum.Konum }, function (sonuclar, durum) {
                        if (durum === 'OK') {
                            var isaretci = new google.maps.Marker({
                                position: sonuclar[0].geometry.location,
                                map: harita
                            });

                            // İşaretçiye etkinlik ID'sini ata
                            isaretci.ozelId = konum.ID;

                            
                            google.maps.event.addListener(isaretci, 'click', function () {
                                // EtkinlikDetay.aspx'e yönlendir
                                window.location.href = `EtkinlikDetay.aspx?ID=${this.ozelId}`;
                            });
                        } else {
                            mesajGoster('Konum çözümlenemedi: ' + konum.Konum + ' (' + durum + ')', 'error');
                        }
                    });
                });

                mesajGoster('Harita başarıyla yüklendi.', 'success');
            } catch (e) {
                mesajGoster('Harita yüklenirken bir hata oluştu: ' + e.message, 'error');
            }
        }

      
        function mesajGoster(mesaj, tur) {
            var mesajKutusu = document.getElementById('mesajKutusu');
            mesajKutusu.innerHTML = mesaj;
            mesajKutusu.className = tur;
            mesajKutusu.style.display = 'block';
        }
    </script>
    <style>
        .navbar {
            display: none;
        }

        #harita-alani {
            width: 100%;
            height: 100vh; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Etkinlik Haritası</h2>

   
    <div id="mesajKutusu" style="display: none; padding: 10px; border: 1px solid; margin-bottom: 10px;"></div>

  
    <div id="harita-alani" style="width: 100%; height: 500px; border: 1px solid #ddd;"></div>

    
    <asp:HiddenField ID="gizliAlanKonumlar" runat="server" />
</asp:Content>
