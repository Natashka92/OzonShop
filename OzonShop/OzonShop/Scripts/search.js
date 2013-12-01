$(function () {
    $("#SortByField").change(function () {
        $("#CurrentPage").val(0);
        SubmitForm();
    });
    $("#PagingSize").change(function () {
        $("#CurrentPage").val(0);
        SubmitForm();
    });
    $("#Previous").click(function () {
        var currentPage = $("#CurrentPage").val();
        if (currentPage != null && currentPage > 0) {
            currentPage--;
            $("#CurrentPage").val(currentPage);
        }
        SubmitForm();
    });
    $("#Next").click(function () {
        var currentPage = $("#CurrentPage").val();
        if (currentPage) {
            currentPage++;
            $("#CurrentPage").val(currentPage);
        }
        SubmitForm();
    });
});

function SubmitForm() {
    document.forms["SearchForm"].submit();
}