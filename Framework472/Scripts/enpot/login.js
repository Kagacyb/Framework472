$(function () {
    var txtUser = $("#txtAccount");
    var txtPwd = $("#txtPwd");
    var btnLogin = $("#btnLogin");

    var funLogin = function () {
        var user = txtUser.val();

        if ($.isEmptyObject(user)) {
            $.messager.alert('提示', '请填写账号。', 'info');
            txtUser.focus();
            return;
        }

        var pwd = txtPwd.val();
        if ($.isEmptyObject(pwd)) {
            $.messager.alert('提示', '请填写密码。', 'info');
            txtPwd.focus();
            return;
        }


        login({ user: user, pwd: pwd });
        return;

    };


    var login = function (data) {
        axios.post("../api/Login", data)
            .then(res => {
                var result = res.data;
                if (result.Success) {
                    $.cookie('loginID', result.Content.LoginID, { expires: 1 });
                    location.href = result.Content.Url; return;
                }
                else {
                    $.messager.alert('提示', result.Message, 'error');
                }
            })
            .catch(err => {
                $.messager.alert('提示', err, 'error');
            });
        //asyncPost("../api/Login", data, function (result) {
        //    if (result.Success) {
        //        //$.cookie('companyCode', txtCpyCode.val(), { expires: 7 });
        //        $.cookie('loginID', result.Content.LoginID, { expires: 1 });
        //        location.href = result.Content.Url; return;
        //    }
        //    $.messager.alert('提示', result.message, 'error');
        //});

    };

    btnLogin.click(funLogin);

    //$(".login-text").on('change', function (e) { txtKeyup(this); });
    $(".login-text").keyup(function (e) { txtKeyup(this); });

    $(".login-text").keydown(function (e) {
        if (e.keyCode == 13) {
            if ($.isEmptyObject(txtUser.val())) {
                txtUser.focus();
                return;
            }

            if ($.isEmptyObject(txtPwd.val())) {
                txtPwd.focus();
                return;
            }

            btnLogin.click();
        }
    });

    function txtKeyup(el) {
        if ($(el).val().length > 0)
            $(el).prev().hide();
        else
            $(el).prev().show();
    }

    var init = function () {
        txtKeyup(txtUser);
        txtKeyup(txtPwd);
    };

    init();
    //loadCookie();
});