<%@ Page Title="Etkinlik Haritası" Language="C#" MasterPageFile="~/AnaSayfa.Master" AutoEventWireup="true" CodeBehind="HaritadaGoruntuleme.aspx.cs" Inherits="etkinlikkayitsites.HaritadaGoruntuleme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDL0UrNYNAON-a4yBZRiegXByzuHMu0A4g&libraries=places,directions"></script>
    <script type="text/javascript">
        var harita, isaretci, rotaServisi, yuruRotaCizici, surusRotaCizici, bisikletRotaCizici, mevcutKonum;

        function haritayiBaslat(lat, lng) {
            var haritaSecenekleri = {
                center: { lat: lat, lng: lng },
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            harita = new google.maps.Map(document.getElementById('map-canvas'), haritaSecenekleri);

            
            isaretci = new google.maps.Marker({
                position: { lat: lat, lng: lng },
                map: harita
            });

           
            google.maps.event.addListener(harita, 'click', function (event) {
                var tiklananKonum = event.latLng;
                isaretci.setPosition(tiklananKonum);

            
                rotaHesapla(tiklananKonum, mevcutKonum);
            });

            rotaServisi = new google.maps.DirectionsService();

          
            yuruRotaCizici = new google.maps.DirectionsRenderer({ polylineOptions: { strokeColor: "orange" } });
            surusRotaCizici = new google.maps.DirectionsRenderer({ polylineOptions: { strokeColor: "blue" } });
            bisikletRotaCizici = new google.maps.DirectionsRenderer({ polylineOptions: { strokeColor: "yellow" } });

            yuruRotaCizici.setMap(harita);
            surusRotaCizici.setMap(harita);
            bisikletRotaCizici.setMap(harita);
        }

        function adresiKodla(adres) {
            var kodlayici = new google.maps.Geocoder();
            kodlayici.geocode({ 'address': adres }, function (sonuclar, durum) {
                if (durum === 'OK') {
                    var konum = sonuclar[0].geometry.location;
                    mevcutKonum = konum;
                    haritayiBaslat(konum.lat(), konum.lng());
                } else {
                    alert('Konum bulunamadı: ' + durum);
                }
            });
        }

        function rotaHesapla(varisNoktasi, baslangicNoktasi) {
         
            var yuruIstek = {
                origin: baslangicNoktasi,
                destination: varisNoktasi,
                travelMode: google.maps.TravelMode.WALKING,
                unitSystem: google.maps.UnitSystem.METRIC
            };

          
            var surusIstek = {
                origin: baslangicNoktasi,
                destination: varisNoktasi,
                travelMode: google.maps.TravelMode.DRIVING,
                unitSystem: google.maps.UnitSystem.METRIC
            };

           
            var bisikletIstek = {
                origin: baslangicNoktasi,
                destination: varisNoktasi,
                travelMode: google.maps.TravelMode.BICYCLING,
                unitSystem: google.maps.UnitSystem.METRIC
            };

           
            rotaServisi.route(yuruIstek, function (cevap, durum) {
                if (durum === 'OK') {
                    yuruRotaCizici.setDirections(cevap);
                } else {
                    alert('Yürüyüş rotası hesaplanamadı: ' + durum);
                }
            });

            rotaServisi.route(surusIstek, function (cevap, durum) {
                if (durum === 'OK') {
                    surusRotaCizici.setDirections(cevap);
                } else {
                    alert('Araba rotası hesaplanamadı: ' + durum);
                }
            });

            rotaServisi.route(bisikletIstek, function (cevap, durum) {
                if (durum === 'OK') {
                    bisikletRotaCizici.setDirections(cevap);
                } else {
                    alert('Bisiklet rotası hesaplanamadı: ' + durum);
                }
            });
        }
    </script>
    <style>
        .navbar {
            display: none;
        }

        #map-canvas {
            width: 100%;
            height: 100vh;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileButtonContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Etkinlik Haritası</h2>
    <div id="map-canvas"></div>

    <asp:Literal ID="ltHataMesaji" runat="server" Visible="false"></asp:Literal>
</asp:Content>
