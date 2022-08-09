// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//student detail getiren bootstrap modal
$(function () {
    var modalPlaceHolder = $('#ModalPlaceHolder');
    $('a[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        var id = $(this).data('id');
        $.get(url, {id: id}).done(function (data) {
            modalPlaceHolder.html(data);
            modalPlaceHolder.find('.modal').modal('show');
        })
    })
})

