// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    var canvas = document.getElementById("footer-canvas");
    var ctx = canvas.getContext("2d");
    ctx.font = "15px Segoe UI Semilight";
    ctx.textAlign = "center";
    ctx.fillText("\u00A9 2021 - FlightInfo", 75, 20);
});