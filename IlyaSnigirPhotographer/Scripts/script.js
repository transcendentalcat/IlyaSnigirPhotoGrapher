//Slider Index

window.onload = function () {

    var i = 0;
    var images = document.getElementsByClassName("img");

    var oNext = document.getElementById("next");
    oNext.onclick = Next;
    var oPrev = document.getElementById("prev");
    oPrev.onclick = Prev;

    var interval = setInterval(LeafSlider, 8000);  

    function Next() {
        images[i].classList.remove("showed");
        i++;
        if (i >= images.length) {
            i = 0;
        }

        images[i].classList.add("showed");
        clearInterval(interval);
        //setTimeout(function () { interval = setInterval(LeafSlider, 8000) }, 8000);
        console.log(interval);
    }
    function Prev() {
        images[i].classList.remove("showed");
        i--;
        if (i < 0) {
            i = images.length - 1;
        }
        images[i].classList.add("showed");
        clearInterval(interval);
        //setTimeout(function () { interval = setInterval(LeafSlider, 8000) }, 8000);
        console.log(interval);
    }

    function LeafSlider() {
        images[i].classList.remove("showed");
        i++;
        if (i >= images.length) {
            i = 0;
        }

        images[i].classList.add("showed");
        
    }
  
}


//Before_After

$(document).ready(function() {
    var mouse_down = false;
    $('#ba_box').mousemove(function(event) {
        var leftpos = event.pageX - $("#ba_box").offset().left;
        var rightpos = 700 - leftpos;
        if (mouse_down && leftpos >= 0 && leftpos <= 700) {
            console.log($("#ba_box").offset().left);
            $('#before').css('width', leftpos);
            $('#after').css('width', rightpos);
            $('#ba_cursor').css('left', leftpos - 10);
        }
    });
    $("#ba_cursor").mousedown(function() { mouse_down = true; });
    $(document).mouseup(function() { mouse_down = false; });
});

//Lightbox

$(document).ready(function () {
    $(".album .album-item img").on("click", function () {
        $(".lightbox").css("display", "block");
        var cur_img = $(this).attr('src');
        $("img.slide_lightbox").attr('src', cur_img);

        var next_img = $(this).parent().next(".album-item").children("img").attr('src');
        $(".next-l").on("click", function () {
            $("img.slide_lightbox").attr('src', next_img);
        });

    });

    $(".close p").on("click", function () {
        $(".lightbox").css("display", "none");
    });
});

var allImages = document.querySelectorAll(".album .album-item img");
var currentImage;

// Up-button
$(window).on("scroll",
    function () {
        if ($(window).scrollTop() > 200) {
            $(".btn-up").fadeIn();
        } else {
            $(".btn-up").fadeOut();
        }
    });

$(".btn-up").on("click", function () {
    $("html, body").animate({ scrollTop: 0 }, 600);
});

/*PopUp window for delete*/
$(document).ready(function () {
    $(".no-btn").click(PopUpHide);
    $(".delete-btn").click(PopUpShow);
});

function PopUpShow() {
    $("#popup1").fadeIn();
}

function PopUpHide() {
    $("#popup1").fadeOut();
}

/*Show pop-up with news*/


$(function () {
    if (showNews)
        $(".news-wrap").fadeIn('slow');

    $(".news-wrap").click(NewsHide);
});

function NewsHide() {
    $(".news-wrap").fadeOut('slow');
}




/*$(window).on('scroll', function(){
	if($(window).scrollTop()) {
		$('nav').addClass('black');
	} 
	else {
		$('nav').removeClass('black');
	}
})


$(window).scroll(function (){ 
		 	$(' .mov1').each(function (){ 
		 		var imagePos = $(this).offset().top; 
		 		var topOfWindow = $(window).scrollTop(); 
		 		if (imagePos < topOfWindow+550) { 
		 			$(this).addClass('slideInUp'); 
		 		} 
		 	}); 
		 });﻿
*/

