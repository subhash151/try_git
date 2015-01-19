var recordPerPage = 10;
var currentPage = 1;

$(document).ready(function () {
    debugger;
    fillCityCombo();
    fillCategoryCombo();

    //main page when clicking on the logo of the website.
    $('#postAd').click(function () {
        window.location.href = "/AdData/Create";
        return false;
    });

    $("#lstCategories").on('click', 'li', function () {
        var catVal = ($(this).attr("value"));
        $('#ddlCategory').val(catVal);
        fethData(currentPage, recordPerPage);
        $("#adCategories").hide();
    });

    $("#lstCity").on('click', 'li', function () {
        var cityVal = ($(this).attr("value"));
        $('#ddlCity').val(cityVal);
        fethData(currentPage, recordPerPage);
        $("#popularCities").hide();
    });

    $("#topLink li").click(function () {
        checkHome();
        var lnkVal = $(this).attr("value");
        $('#ddlCategory').val(lnkVal);
        fethData(currentPage, recordPerPage);
        $("#adCategories").hide();
    });

    $("#ddlCategory").change(function () {
        fethData(currentPage, recordPerPage);
    });

    $("#ddlCity").change(function () {
        fethData(currentPage, recordPerPage);
    });

    $("#btnSearch").button().click(function () {
        fethData(currentPage, recordPerPage);
    });
});

function checkHome()
{
    var url = window.location.href;
    if (url.split("/").length > 3) {
        if (url.split("/")[3] != "") {
            if (url.split("/")[3] != "#") {
                window.location.href = "/";
            }
        }
    }
};

function fethData(currentPage, recordPerPage)
{
    var valCat = $('#ddlCategory').val();
    var valCity = $('#ddlCity').val();
    var search = $('#txtSearch').val();    

    $("#pageData").empty();
    $.get("/Home/fetchData?cityId=" + valCity + "&catId=" + valCat + "&currentPage=" + currentPage + "&recordPerPage=" + recordPerPage + "&search=" + search, function (data) {
        $("#pageData").append(data);
    });

    setFilterCaption();

    $("#ulPagination li").click(function () {
        var pgVal = $(this).text();
        $("#ulPagination li.active").removeClass('active');
        $(this).addClass('active');
    });
};

function setFilterCaption()
{
    var catTxt = $("#ddlCategory option:selected").text();
    var ctyTxt = $("#ddlCity option:selected").text();
    var filterCity = "All India";
    var filterCategory = "All Category";
   
    if (ctyTxt != "")
    {
        filterCity = ctyTxt;
    }

    if (catTxt != "")
    {
        filterCategory = catTxt;
    }

    $("#freeAdFilders").empty();
    $("#freeAdFilders").text(filterCity + " - " + filterCategory);
}

function fillCityCombo() {
    if (!$('#ddlCity').val()) {
        $.get("/Home/GetCity", function (data) {
            $("#ddlCity").empty();
            //array to collect cities.
            $('#ddlCity').append('<option value="0">All India</option>');
            var items = [];
            $.each(data, function (index, item) {
                $('#ddlCity').append($('<option>', {
                    value: item.ID,
                    text: item.CityName
                }));
                //collecting cities for list view
                items.push('<li value='+ item.ID + '><a href="#">' + item.CityName + '</a></li>');
                //items.push('<li value=' + item.ID + '>' + item.CityName + '</li>');
            });
            //populating list of cities.
            $('#lstCity').append(items);
        });
    };
};

function fillCategoryCombo() {
    if (!$('#ddlCategory').val()) {
        $.get("/Home/GetCategory", function (data) {
            $("#ddlCategory").empty();
            //array to collect categories.
            $('#ddlCategory').append('<option value="0">All Category</option>');
            var items = [];
            $.each(data, function (index, item) {
                $('#ddlCategory').append($('<option>', {
                    value: item.ID,
                    text: item.CategoryName
                }));
                //collecting categories for list view
                items.push('<li value='+ item.ID + '><a href="#">' + item.CategoryName + '</a></li>');
            });
            //populating list of cities.
            $('#lstCategories').append(items);
        });
    };
};
