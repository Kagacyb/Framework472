$(function () {
    var gvItem = $("#gvItem");
    var gvItemTitle = $("#gvItemTitle");


    gvItem.datagrid({
        loadFilter: pagerFilter
    });

    var initUI = function () {
        axios.post("../Demo1/Load")
            .then(res => {
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

    var btnLoad = $("#btnLoad");
    btnLoad.click(function () { initUI(); });

    initUI(); 
});