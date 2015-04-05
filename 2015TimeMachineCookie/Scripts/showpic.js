$(function () {
    //分页
    $('.pageli').click(function () {
        showpicPost($(this).text());
    });
    //下拉菜单
    $("[role='menuitem']").click(function () {
        var select = $(this).attr('id');//时间顺序、时间倒序、热度最高
    });
    $('#index-search-btn').click(function () {
        searchPost($('#index-search-input')[0].value);
    });
});

function showpicPost(index) {
    var sendData = { blockno: index };
    if (sendData.blockno == "") {
        return;
    }
    $.ajaxJsonPost({
        url: "/Picture/Block",
        data: JSON.stringify(sendData),
        success: function () {
        },
        error: function () {
            console.log("err.");
        }
    });
}

function orderPic(select,type) {
    $.ajaxJsonPost({
        url: "/Picture/Block",
        data: JSON.stringify(sendData),
        success: function () {
        },
        error: function () {
            console.log("err.");
        }
    });
}

function searchPost(input) {
    var sendData = { input: input };
    if (sendData.input == "") {
        return;
    }
    $.ajaxJsonPost({
        url: "/Picture/Search",
        data: JSON.stringify(sendData),
        success: function () {
        },
        error: function () {
            console.log("err.");
        }
    });
}