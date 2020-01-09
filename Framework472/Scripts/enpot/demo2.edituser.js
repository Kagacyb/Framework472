function EditUserInfo() {
    var editUser;
    var dlgTxtUserName = $("#dlgTxtUserName");
    var dlgTxtLoginAccount = $("#dlgTxtLoginAccount");
    var dlgTxtUploadAccount = $("#dlgTxtUploadAccount");
    var dlgTvRole = $("#dlgTvRole");
    var dlgTvLocation = $("#dlgTvLocation");
    var dlgBtnUserStop = $("#dlgBtnUserStop");
    var dlgBtnResetPwd = $("#dlgBtnResetPwd");
    var dlgBtnUserSave = $("#dlgBtnUserSave");
    var dlgUser = $("#dlgUser");

    dlgUser.dialog({
        modal: true,
        onClose: function () {
            editUser = null;

            //$.messager.progress('close');
            dlgTxtUserName.textbox('setValue', '');
            dlgTxtLoginAccount.textbox('setValue', '');
            dlgTvRole.tree('loadData', []);
            dlgTvLocation.tree('loadData', []);
        }
    });
    dlgUser.dialog('close');

    this.setData = function (data) {
        editUser = data;
        dlgUser.dialog('open');
   
        dlgTxtUserName.textbox('setValue', data.Name);
        dlgTxtLoginAccount.textbox('setValue', data.Account);
        dlgTxtUploadAccount.textbox('setValue', data.UploadAccount);
        dlgBtnUserStop.val(data.Effectiveness ? "停用" : "启用");
        //dlgTvRole.tree('loadData', data.Roles);
        //dlgTvLocation.tree('loadData', data.Locations);
    }

    reload = null;

    this.saveData = function (reset) {
        reload = reset;
    }

    var waitLoad = function () {
        $(".datagrid-mask").css("z-index", 10001);
        $(".datagrid-mask-msg").css("z-index", 10002);
    };

    dlgTvLocation.tree({
        onLoadSuccess: function (node, data) {
            $("#dlgTvLocation span[class^='tree-icon tree-folder']").remove();
            $("#dlgTvLocation span[class^='tree-icon tree-file']").remove();
            var root = $(this).tree('getRoot');
            if ($.isEmptyObject(root) == false)
                $(this).tree('scrollTo', root.target);

            var roots = $(this).tree('getRoots');
            for (var i = 0; i < roots.length; i++) {
                var item = $(this).tree('find', roots[i].id);
                $(this).tree('collapseAll', item.target);
            }
        }, onExpand: function (node) {
            $("#dlgTvLocation span[class^='tree-folder-open']").remove();
        }
    });

    dlgTvRole.tree({
        onLoadSuccess: function (node, data) {
            $("#dlgTvRole span[class^='tree-icon tree-folder']").remove();
            $("#dlgTvRole span[class^='tree-icon tree-file']").remove();
            var root = $(this).tree('getRoot');
            if ($.isEmptyObject(root) == false)
                $(this).tree('scrollTo', root.target);
        }, onExpand: function (node) {
            $("#dlgTvRole span[class^='tree-folder-open']").remove();
        }
    });

    dlgBtnResetPwd.click(function () {
        if ($.isEmptyObject(editUser)) return;

        if ($.isEmptyObject(editUser.ID)) return;

        $.messager.confirm('提示', '确认是否重置密码？', function (r) {
            if (r) {
                var json = { ID: editUser.ID };
                var jsonData = JSON.stringify(json);
                var jsonText = { action: "ChangePwd", data: jsonData };
                asyncPost(document.URL, jsonText, function (result) {
                    if (result.success)
                        $.messager.alert('提示', result.data, 'info');
                    else
                        $.messager.alert('错误', result.message, 'info');
                });
            }
        });
    });

    dlgBtnUserStop.click(function () {
        dlgBtnUserStop.val(dlgBtnUserStop.val() == "停用" ? "启用" : "停用");
    });

    dlgBtnUserSave.click(function () {

        $.messager.progress('close');
        dlgUser.dialog('close');
        return;

        var userName = dlgTxtUserName.val();
        if ($.isEmptyObject(userName)) {
            $.messager.alert('提示', '请填写用户名。', 'info');
            dlgTxtUserName.focus();
            return;
        }

        var loginAccount = dlgTxtLoginAccount.val();
        if ($.isEmptyObject(loginAccount)) {
            $.messager.alert('提示', '请填写登录账号。', 'info');
            dlgTxtLoginAccount.focus();
            return;
        }

        var uploadAccount = dlgTxtUploadAccount.val();

        var roleNodes = dlgTvRole.tree('getChecked');
        var roleArray = new Array();
        $.each(roleNodes, function (idx, node) {
            if ($.isEmptyObject(node.attributes) == false)
                roleArray[roleArray.length] = node.attributes;
        });

        var locationNodes = dlgTvLocation.tree('getChecked');
        var locationArray = new Array();

        var pass = false;
        $.each(locationNodes, function (idx, node) {
            if ($.isEmptyObject(node.attributes) == false) {

                var tmpItem = node.attributes;
                locationArray[locationArray.length] = tmpItem;

                if (pass == false && tmpItem.TypeID == 3) {
                    pass = true;
                }
            }
        });

        if (pass == false) {
            $.messager.alert('提示', '请选择一间所属工厂', 'info');
            return;
        }


        if (roleArray.length == 0 && locationArray.length == 0) {
            $.messager.alert('提示', '请选择所需要的权限', 'info');
            return;
        }

        var json = {
            ID: editUser.ID,
            UserName: userName,
            LoginAccount: loginAccount,
            UploadAccount: uploadAccount,
            Effectiveness: dlgBtnUserStop.val() == "停用",
            Roles: roleArray,
            Locations: locationArray
        };
        var jsonData = JSON.stringify(json);
        var jsonText = { action: "SaveUser", data: jsonData };
        asyncPost(document.URL, jsonText, function (result) {
            if (result.success) {
                $.messager.alert('提示', $.isEmptyObject(editUser.ID) ? "新增用户成功。" : "修改用户成功。", 'info');

                if (reload != null)
                    reload();

                $.messager.progress('close');
                dlgUser.dialog('close');
            } else {
                $.messager.alert('错误', result.message, 'info');
            }
        });
    });
};