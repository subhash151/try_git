﻿$(document).ready(function () {
    debugger;
    $("#ddlCity").width("200");
    $("#ddlCategory").width("200");
    $("#txtSearch").width("300");
    fillCityCombo();
    fillCategoryCombo();
    fillAdData();

    $('#postAd').click(function () {
        window.location.href = "/AdData/Create";
        return false;
    });

    $("#lstCategories").on('click', 'li', function () {
        var catVal = ($(this).attr("value"));
        alert(catVal);
        $('#ddlCategory').val(catVal);
        $("#adCategories").hide();
    });

    $("#lstCity").on('click', 'li', function () {
        var cityVal = ($(this).attr("value"));
        alert(cityVal);
        $('#ddlCity').val(cityVal);
        $("#popularCities").hide();
    });

    $("#ddlCategory").change(function () {
        var option = $(this).find('option:selected');
        alert($(option).attr("value"));
    });

    $("#ddlCity").change(function () {
        var option = $(this).find('option:selected');
        alert($(option).attr("value"));
    });
});

function fillCityCombo() {
    if (!$('#ddlCity').val()) {
        $.get("/Home/GetCity", function (data) {
            $("#ddlCity").empty();
            //array to collect cities.
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

function fillAdData() {
    //if (!$('#lstAdData').val()) {
        $.get("/Home/GetAdData", function (data) {
            //$("#lstAdData").empty();
            var items = [];
            $.each(data, function (index, item) {
                items.push('<li><a href="#">' + item.Ad_Title + '</a></li>');
            });

            //alert(JSON.stringify(data));
            //alert(JSON.stringify(items));

            //$('#latestAdContainer').append(items);
            //$.tmpl("tmplTopThree", items).appendTo("#latestAdContainer");
        });
    //};
};



$(function () {
    var selection;
    $(".nav nav-pills pull-left").lstCategories({

        select: function (event, ui) {

            $('.selected', this).removeClass('selected');

            // add the css class as well as get the text value of the selection

            selection = ui.item.addClass('selected').text();

        } // closes select function

    });

    $((".nav nav-pills pull-left").lstCategories).on('click', function () {

        alert(selection);

    }); //closes click()
});


