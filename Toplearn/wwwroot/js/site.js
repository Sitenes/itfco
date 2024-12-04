document.addEventListener("DOMContentLoaded", function () {
    const headerLower = document.querySelector("#header");

    if (window.location.pathname === "/" || window.location.pathname.toLowerCase().includes("home")) {
        headerLower.style.position = "absolute";
    } else {
        headerLower.style.position = "relative";
    }
});
