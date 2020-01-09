$(function () {

    var txtbox = $("#textbox");
    var numbox = $("#numberbox");
    var pwdbox = $("#passwordbox");
    var combox = $("#combobox");

    var datbox = $("#datebox");
    var filebox = $("#filebox");
    var colorbox = $("#colorbox");
    var switchbtn = $("#switchbutton");

    var file = null;
    var base64 = null;

    function change(_obj) {
        if ($(_obj).context.ownerDocument.activeElement.files != undefined) {
            file = $(_obj).context.ownerDocument.activeElement.files[0]
            if (FileReader) {
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    base64 = e.target.result;
                };
            }
        } else { file = null; base64 = null; }
    }

    filebox.filebox({ onChange: function c() { change(this) } });
    $("#btnSave").click(function () {

        var str = "";
        str += txtbox.attr("label");
        str += txtbox.val();
        str += "\r"

        str += numbox.attr("label");
        str += numbox.val();
        str += "\r"

        str += pwdbox.attr("label");
        str += pwdbox.val();
        str += "\r"

        str += combox.attr("label");
        str += combox.val();
        str += "\r"

        str += datbox.attr("label");
        str += datbox.val();
        str += "\r"

        str += filebox.attr("label");
        str += filebox.filebox('getValue');
        str += "\r"

        var radio1 = $("input[name='radiobutton1']:checked");
        if (radio1.length > 0) {
            str += "单选(123):  "
            str += radio1.val();
            str += "\r"
        }

        var radio2 = $("input[name='radiobutton2']:checked");
        if (radio2.length > 0) {
            str += "单选(ABC):  "
            str += radio2.val();
            str += "\r"
        }

        var chkBox = $("input[name='checkbox']:checked");
        if (chkBox.length > 0) {
            str += "多选标题: "
            $.each(chkBox, function (index, item) {
                str += $(item).val();
            })

            str += "\r"
        }

        str += colorbox.attr("label");
        str += colorbox.val();
        str += "\r"

        str += "按钮:  ";
        str += switchbtn.switchbutton("options").checked
        str += "\r"

        alert(str);
    })

    $("#btnReset").click(function () {
        txtbox.textbox('setValue', '');
        numbox.numberbox('setValue', '');
        pwdbox.passwordbox('setValue', '');
        combox.combobox('setValue', '');
        datbox.datebox('setValue', '');
        filebox.textbox('setValue', '');

        $("#radio1-1").radiobutton('clear');
        $("#radio1-2").radiobutton('clear');
        $("#radio1-3").radiobutton('clear');

        $("#radio2-1").radiobutton('clear');
        $("#radio2-2").radiobutton('clear');
        $("#radio2-3").radiobutton('clear');

        //$("#radio1-2").radiobutton('check', true);

        $("#chkRed").checkbox("clear");
        $("#chkYellow").checkbox("clear");
        $("#chkBlue").checkbox("clear");

        colorbox.textbox('setValue', '');
        colorbox.textbox('textbox').css('background', '#ffffff')
        switchbtn.switchbutton('clear');
    });

})



