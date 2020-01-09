$(function () {
    var tvMenu = $("#tvMenu");
    var tabMain = $("#tabMain");

    tvMenu.tree({
        onClick: function (node) {
            if (node.url == undefined) return;
            var url = node.url;
            var content = '<iframe scrolling="no" frameborder="0"  src="' + url + '"  style="width: 100%; height: 100%"  ></iframe>';
            tabMain.tabs('add', {
                title: node.text,
                content: content,
                closable: true,
                //  href: url
            });
        }
    })


    var initUI = function () {
        axios.post("Menu")
            .then(res => {
                tvMenu.tree('loadData', res.data);
            })
            .catch(err => {
                $.messager.alert('提示', err, 'error');
            });
    }

    initUI();
})