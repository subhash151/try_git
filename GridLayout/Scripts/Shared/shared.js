$(document).ready(function () {
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

    
    //create top three template
    var markup = "<h4>${Ad_Title}</h4>";
    $.template("tmplTopThree", markup);
    
    alert("ALL DONE");
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
                items.push('<li><a href="#">' + item.CityName + '</a></li>');
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
                items.push('<li><a href="#">' + item.CategoryName + '</a></li>');
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


