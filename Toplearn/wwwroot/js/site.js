// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const headerLower = document.querySelector("#header");

    if (window.location.pathname === "/" || window.location.pathname.toLowerCase().includes("home")) {
        headerLower.style.position = "absolute";
    } else {
        headerLower.style.position = "relative";
    }
});
