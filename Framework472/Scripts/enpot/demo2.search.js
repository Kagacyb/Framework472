$(function () {
    var btnSearch = $("#btnSearch");
    var btnReset = $("#btnReset");



    var gvItem = $("#gvItem");
    var txtFrom = $("#txtFrom");
    var txtTo = $("#txtTo");
    var txtVbeln = $("#txtVbeln");
    var txtMatnr = $("#txtMatnr");
    var txtTanum = $("#txtTanum");

    var searchReq = function () {
        var json = {
            fromDate: txtFrom.val(),
            toDate: txtTo.val(),
            vbeln: txtVbeln.val(),
            matnr: txtMatnr.val(),
            tanum: txtTanum.val()
        }
        return json;
    }

    var searchValid = function (json) {
        if ($.isEmptyObject(json.fromDate) &&
            $.isEmptyObject(json.toDate) &&
            $.isEmptyObject(json.vbeln) &&
            $.isEmptyObject(json.matnr) &&
            $.isEmptyObject(json.tanum)) {
            $.messager.alert('提示', '请填写搜索条件。', 'info');
            return false;
        }
        else {
            return true;
        }
    }

    var search = function (json) {
        var jsonData = JSON.stringify(json);
        axios.post("Find", { data: jsonData })
            .then(result => {
                var pager = $(gvItem.datagrid('getPager'));
                pager.pagination('select', 1);
                gvItem.datagrid('loadData', result.data);
            }).catch(error => {
                $.messager.alert('错误', error, 'info');
            });
    };

    btnSearch.click(function () {

        var json = searchReq();

        if (!searchValid(json)) return;

        search(json);
    });

    gvItem.datagrid({
        loadFilter: pagerFilter
    });


    //弹出窗体
    var btnCreate = $("#btnCreate");
    var userInfo = new EditUserInfo();
    btnCreate.click(function () {
        userInfo.setData({ Name: "名字", Account: "账号", UploadAccount: "", Effectiveness: false })
    });
})