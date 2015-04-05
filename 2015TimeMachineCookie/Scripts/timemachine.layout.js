

MyModules.Layout = function () {

    //成员定义
    $bg = $(".box-bg");
    var showbg = function () {
        $bg.addClass("on");
    }
    var hiddenbg = function () {
        $bg.removeClass("on");
    }
    var togglebg = function () {
        $bg.toggleClass("on");
    }

    //静态方法.外部调用
    //box-bg show hidden
    MyModules.Layout.prototype.togglebg = togglebg;
    MyModules.Layout.prototype.showbg = showbg;
    MyModules.Layout.prototype.hiddenbg = hiddenbg;



    //=============================================
    //main()

    //隐藏显示登陆框
    $(".login-btn").click(function () {
        togglebg();
    });
    $bg.find("#login-close").click(function () {
        togglebg();
    });
    //注册用户框
    $bg.find("#login-btn-register").click(function () {
        togglebg();
        toggleRegister();
    });

    //用户登陆ajax 引用自己定义的ajaxJsonPost
    $("#login-btn-ok").click(function () {

        var sendData = {
            username: $("#login-name").val(),
            password: $("#login-pass").val()
        };

        if (sendData.username == "" || sendData.password == "") {
            return;
        }
        $temp = $(this);
        $(this).attr("disabled", "disabled");
        $.ajaxJsonPost({
            url: "/User/Login",
            data: JSON.stringify(sendData),
            success: function (user) {
                togglebg();
                $(".user-state").html("欢迎，" + user.username + " <a class='user-exit'></a>");
            },
            error: function () {
                console.log("err.");
                $temp.removeAttr("disabled");
                $("#login-pass").parent().removeClass("has-error");
                $("#login-pass").parent().addClass("has-error");
            }
        });
    });
    var doExit = function () {
        $.ajaxJsonGet({
            url: "/User/Exit",
            complete: function () {
                self.location = self.location;
            }
        });
    }
    $(".user-state").on("click", function (e) {
        console.log(e);
        if (e.target == document.getElementsByClassName("user-exit")[0]) {
            doExit();
        }
    });
}
