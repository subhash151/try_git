$(document).ready(function () {
    debugger;
    //$("#ddlCity").width("200");
    //$("#ddlCategory").width("200");
    //$("#txtSearch").width("300");
    fillCityCombo();
    fillCategoryCombo();

    $('#postAd').click(function () {
        window.location.href = "/AdData/Create";
        return false;
    });

    $("#lstCategories").on('click', 'li', function () {
        var catVal = ($(this).attr("value"));
        $('#ddlCategory').val(catVal);
        fethData();
        $("#adCategories").hide();
    });

    $("#lstCity").on('click', 'li', function () {
        var cityVal = ($(this).attr("value"));
        $('#ddlCity').val(cityVal);
        fethData();
        $("#popularCities").hide();
    });

    $("#ddlCategory").change(function () {
        fethData();
    });

    $("#ddlCity").change(function () {
        fethData();
    });

    $("#btnSearch").button().click(function () {
        fethData();
    });

    //alert("yes loading");
    fethData();
});

$("#ulPagination").click(function () {
});

function fethData()
{
    var valCat = $('#ddlCategory').val();
    var valCity = $('#ddlCity').val();
    var search = $('#txtSearch').val();
   // alert("cat :" + valCat + "cty :" + valCity + "search:" + search);

    $("#adData").empty();
    $.get("/Home/fetchData?cityId=" + valCity + "&catId=" +  valCat + "&search=" +search, function (data) {
        $("#adData").append(data);
    });

    return false;
};

function fillCityCombo() {
    if (!$('#ddlCity').val()) {
        $.get("/Home/GetCity", function (data) {
            $("#ddlCity").empty();
            //array to collect cities.
            $('#ddlCity').append('<option value="0">All</option>');
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
            $('#ddlCategory').append('<option value="0">All</option>');
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

//$(function () {
//    var selection;
//    $(".nav nav-pills pull-left").lstCategories({
//        select: function (event, ui) {
//            $('.selected', this).removeClass('selected');
//            // add the css class as well as get the text value of the selection
//            selection = ui.item.addClass('selected').text();
//        } // closes select function

//    });

//    $((".nav nav-pills pull-left").lstCategories).on('click', function () {
//        alert(selection);
//    }); //closes click()
//});


