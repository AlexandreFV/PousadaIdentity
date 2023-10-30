// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Script do menu lateral

var BodyPrincipal = document.getElementById('body_principal');
var MenuLateral = document.getElementById('menu_lateral');
var MainPrincipal = document.getElementById('main_principal');
var ShowMenuLateral = false;

function toggleMenuLateral() {
    ShowMenuLateral = !ShowMenuLateral;
    if (ShowMenuLateral) {
        MenuLateral.style.overflow = 'visible';
        MenuLateral.style.marginRight = '0'
        MenuLateral.style.animationName = 'ShowMenuLateral';
        MainPrincipal.style.filter = 'blur(2px)';
        
    }
    else {
        MenuLateral.style.overflow = 'hidden';
        MenuLateral.style.marginRight = '-80vw';
        MenuLateral.style.animationName = '';
        MainPrincipal.style.filter = '';
    }
} 

function closeMenuLateral(){
    if (ShowMenuLateral) {
        toggleMenuLateral();
    }
}

window.addEventListener('resize', function (event) {
    if(window.innerWidth > 1200 && ShowMenuLateral){
        toggleMenuLateral();
    }
});