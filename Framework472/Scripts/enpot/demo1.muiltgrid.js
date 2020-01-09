$(function () {
    var gvItem = $("#gvItem");
    var gvItemTitle = $("#gvItemTitle");

    var gvDetail = $("#gvDetail");
    var gvDetailTitle = $("#gvDetailTitle");

    gvItem.datagrid({
        loadFilter: pagerFilter,
        onClickRow: function (index, row) {
            getDetail(index, row);
        }
    });

    var rowIndex;
    var initUI = function () {
        axios.post("../Demo1/Load")
            .then(res => {
                rowIndex = -1;
                var result = res.data;
                gvItemTitle.panel('setTitle', "共找到  " + result.length + " 项");
                var pager = $(gvItem.datagrid('getPager'));
                pager.pagination('select', 1);
                gvItem.datagrid('loadData', result);
            })
            .catch(err => {
                $.messager.alert('提示', err, 'error');
            });
    }

    var clearDetail = function () {
        gvDetailTitle.panel('setTitle', " ");
        gvDetail.datagrid('loadData', { total: 0, rows: [] });
    }

    var rowIndex;
    var getDetail = function (index, row) {
        if (rowIndex == index) return;
        rowIndex = index;
        gvDetailTitle.panel('setTitle', "SAP单号 " + row.SAPRef);

        var array = new Array();
        for (var i = 0; i < 10; i++)
            array.push({ ProductCode: i, ProductName: row.SAPRef })

        gvDetail.datagrid('loadData', array);
    }

    initUI();
});