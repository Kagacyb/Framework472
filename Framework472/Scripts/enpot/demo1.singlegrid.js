$(function () {
    var gvItem = $("#gvItem");
    var gvItemTitle = $("#gvItemTitle");


    gvItem.datagrid({
        onLoadSuccess: function (data) {
            gvItemTitle.panel('setTitle', "共找到  " + data.rows.length + " 项");
        }
    });

    gvItem.datagrid('loadData', [{ SAPDeliveryNoteRef: 'ABC' }]);

});