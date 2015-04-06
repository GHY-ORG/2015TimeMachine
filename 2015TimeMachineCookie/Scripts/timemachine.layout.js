MyModules.Layout = function () {

    //成员定义
    $bg = $(".box-bg");

    $loginform = $("#login-form");
    $registerform = $("#register-form");

    $btn_login = $loginform.find("#login-btn-ok");
    $btn_toregister = $loginform.find("#login-btn-register");

    $btn_register = $registerform.find("#register-btn-ok");
    $btn_backlogin = $registerform.find("#register-btn-login");

    $close_login = $loginform.find("#login-close");
    $close_register = $registerform.find("#register-close");

    //静态方法.外部调用
    //box-bg show hidden
    MyModules.Layout.prototype.showlogin = function () {
        $bg.addClass("on");
        $loginform.addClass("on");
    };


    //=============================================
    //main()

    //显示登陆框
    $(".login-btn").click(function () {
        $bg.addClass("on");
        $loginform.addClass("on");
    });
    //关闭登陆框
    $close_login.click(function () {
        $loginform.removeClass("on");
        $bg.removeClass("on");
    });
    //关闭注册框
    $close_register.click(function () {
        $registerform.removeClass("on");
        $bg.removeClass("on");
    });
    //登陆框到注册框
    $btn_toregister.click(function () {
        $loginform.removeClass("on");
        $registerform.addClass("on");
    });
    //注册框到登录框
    $btn_backlogin.click(function () {
        $registerform.removeClass("on");
        $loginform.addClass("on");
    });

    //用户登陆ajax 引用自己定义的ajaxJsonPost
    $btn_login.click(function () {

        var sendData = {
            username: $("#login-name").val(),
            password: $("#login-pass").val()
        };
        if (!document.getElementById("login-form").checkValidity())
            return;
        $temp = $(this);
        $(this).attr("disabled", "disabled");
        $.ajaxJsonPost({
            url: "/User/Login",
            data: JSON.stringify(sendData),
            success: function (user) {
                $loginform.removeClass("on"); $bg.removeClass("on");
                $(".user-state").html("欢迎， " + user.username + " <a class='user-exit'></a>");
            },
            error: function () {
                console.log("err.");
                $temp.removeAttr("disabled");
                $("#login-pass").parent().removeClass("has-error");
                $("#login-pass").parent().addClass("has-error");
            }
        });
    });
    if (document.getElementById("register-rpwd")) {
        document.getElementById("register-rpwd").checkValidity = function () {
            if (this.value === document.getElementById("register-pwd").value) return true;
            else false;
        }
    }

    $bg.find("input").blur(function () {
        var dom = $(this).get(0);
        if (!dom.checkValidity()) {
            $(this).parent().removeClass("has-success");
            $(this).parent().addClass("has-error");
        } else {
            $(this).parent().removeClass("has-error");
            $(this).parent().addClass("has-success");
        }
    });

    //用户注册
    $btn_register.click(function () {
        var sendData = {
            StudentNo: $("#register-sno").val(),
            SPassword: $("#register-spwd").val(),
            Phone: $("#register-tel").val(),
            UserName: $("#register-uname").val(),
            Password1: $("#register-pwd").val(),
            Password2: $("#register-rpwd").val()
        };
        if (!document.getElementById("register-form").checkValidity())
            return;
        $temp = $(this);
        $temp.attr("disabled", "disabled");
        $.ajaxJsonPost({
            url: "/User/Register",
            data: JSON.stringify(sendData),
            success: function (data) {
                console.log("success:"+data.message);
                document.location = document.location;
            },
            error: function (err) {
                console.log("err:" + err.message);
                alert(err.message);
                $temp.removeAttr("disabled");
            }
        });
    });
    //用户退出
    var doExit = function () {
        $.ajaxJsonGet({
            url: "/User/Exit",
            complete: function () {
                self.location = self.location;
            }
        });
    }
    //退出按钮监听
    $(".user-state").on("click", function (e) {
        console.log(e);
        if (e.target == document.getElementsByClassName("user-exit")[0]) {
            doExit();
        }
    });
}
