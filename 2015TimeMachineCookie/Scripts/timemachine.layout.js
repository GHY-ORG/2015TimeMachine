MyModules.Layout = function () {

    //成员定义
    $bg = $(".login-bg");
    var toggleLogin = function () {
        $bg.toggleClass("on");
    }
    //静态方法
    MyModules.Layout.prototype.toggleLogin = toggleLogin;



    //隐藏显示登陆框
    $(".login-btn").click(function () {
        toggleLogin();
    });
    $bg.find("#login-close").click(function () {
        toggleLogin();
    });


    //用户登陆ajax 引用自己的extend
    $("#login-btn-ok").click(function () {
        console.log("btn-ok click");
        var sendData = {
            username: $("#login-name").val(),
            password: $("#login-pass").val()
        };
        $.ajaxJsonPost({
            url: "/User/Login",
            data: JSON.stringify(sendData),
            statusCode: {
                200: function () {
                    alert("Login Success");
                },
                401: function () {
                    alert("unAuth");
                }
            }
        });
    });
}
