$(function () {

    $('#side-menu').metisMenu();

});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
$(function () {
    $(window).bind("load resize", function () {
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.sidebar-collapse').addClass('collapse')
        } else {
            $('div.sidebar-collapse').removeClass('collapse')
        }
    })
})



function validateFileType() {
    var fileName = document.getElementById("fileName").value;
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == "gif") {

        var src = URL.createObjectURL(event.target.files[0]);
        var preview = document.getElementById("imgAvatar");
        preview.srcset = src;
        preview.style.display = "block";

    } else {
        alert("شما فقط مجاز به انتخاب عکس با فرمت .jpg .jepg .gif .png هستید!");
        document.getElementById("fileName").value = "";
    }
}
function validateDemoType() {
    var fileName = document.getElementById("demofile").value;
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (extFile == "mp4" || extFile == "mkv" || extFile == "wmv" || extFile == "mov" || extFile == "flv" || extFile == "avi") {

        var src = URL.createObjectURL(event.target.files[0]);
        var preview = document.getElementById("DemoFilm");
        preview.src = src;
        preview.style.display = "block";

    } else {
        alert("شما فقط مجاز به انتخاب عکس با فرمت .mp4 .mkv .wmv .mov .flv .avi هستید!");
        document.getElementById("demofile").value = "";
    }
}


