// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// show modal function
function showModal(modalId) {
    // select modal all modals that start with id defaultModal_
    let modal = $('#' + modalId);
    // change aria-hidden attribute to false
    modal.attr('aria-hidden', 'false');
    // remove hidden from class attribute
    modal.removeClass('hidden');
    // add fade show to class attribute
    // modal.addClass('fade show');
}
// hide modal function
function hideModal(modalId) {
    // select modal
    let modal = $('#' + modalId);
    // change aria-hidden attribute to true
    modal.attr('aria-hidden', 'true');
    // add hidden from class attribute
    modal.addClass('hidden');
    // add fade hide to class attribute
    // modal.addClass('fade show');
}
