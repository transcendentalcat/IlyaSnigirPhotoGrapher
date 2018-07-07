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
        setTimeout(function () { interval = setInterval(LeafSlider, 8000) }, 8000);
    }
    function Prev() {
        images[i].classList.remove("showed");
        i--;
        if (i < 0) {
            i = images.length - 1;
        }
        images[i].classList.add("showed");
        clearInterval(interval);
        setTimeout(function () { interval = setInterval(LeafSlider, 8000) }, 8000);
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

$(document).ready(function () {
    var mouse_down = false;
    $('#ba_box').mousemove(function (event) {
        var leftpos = event.pageX - $("#ba_box").offset().left;
        var rightpos = 700 - leftpos;
        if (mouse_down && leftpos >= 0 && leftpos <= 700) {
            console.log($("#ba_box").offset().left);
            $('#before').css('width', leftpos);
            $('#after').css('width', rightpos);
            $('#ba_cursor').css('left', leftpos - 10);
        }
    });
    $("#ba_cursor").mousedown(function () { mouse_down = true; });
    $(document).mouseup(function () { mouse_down = false; });
}
)

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

