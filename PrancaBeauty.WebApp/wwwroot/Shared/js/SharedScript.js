window.confirm = function (_text, _title, _yesAction = function () { }) {
    swal.fire({
        title: _title,
        text: _text,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonColor: '#3085d6',
        confirmButtonColor: '#d33',
        confirmButtonText: YesText,
        cancelButtonText: NoText
    }).then((result) => {
        if (result.isConfirmed) {
            _yesAction();
        }
    });
}

function SendData(_url, _data, _Funcs_Success = function (res) { }) {

    $.ajax({
        type: "post",
        enctype: 'multipart/form-data',
        url: _url,
        data: _data,
        timeout: 600000,
        beforeSend: function (xhr) {
            $('.loading').show();

            var securityToken = $("[name=__RequestVerificationToken]").val();
            xhr.setRequestHeader("XSRF-TOKEN", securityToken);
        },
        success: function (response) {
            _Funcs_Success(response);
        },
        complete: function (data) {
            $('.loading').hide(100);
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    });
}

function SendForm(_url, _FormId, _Funcs_Success = function (res) { }) {
    var form = $('#' + _FormId)[0];
    var _formdata = new FormData(form);

    $.ajax({
        type: "post",
        enctype: 'multipart/form-data',
        url: _url,
        data: _formdata,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        beforeSend: function (xhr) {
            $('.loading').show();

            var securityToken = $("[name=__RequestVerificationToken]").val();
            xhr.setRequestHeader("XSRF-TOKEN", securityToken);
        },
        success: function (response) {
            _Funcs_Success(response);
        },
        complete: function (data) {
            $('.loading').hide(100);
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    });
}

function LoadComponent(_Url, _Data, _CallbackFuncs = function (data) { }, _EnableLoading = true) {
    $.ajax({
        url: _Url,
        type: 'get',
        data: _Data,
        beforeSend: function (xhr) {
            if (_EnableLoading)
                $('.loading').show();
        },
        complete: function (data) {
            if (_EnableLoading)
            $('.loading').hide(100);
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    }).done(function (data) {
        _CallbackFuncs(data);
    });
}

function RemoveData(_Url, _Data = {}) {
    confirm(DeleteMsg, '', function () {
        SendData(_Url, _Data);
    });
}

function AreYouSureData(_Url, _Data = {}) {
    confirm(AreYouSureMsg, '', function () {
        SendData(_Url, _Data);
    });
}

function RefreshGrid(_GridId) {
    var _Grid = $('#' + _GridId).data('kendoGrid');
    _Grid.dataSource.read();
    _Grid.refresh();
}

function ChangeUrl(_NewUrl) {
    history.pushState({}, null, _NewUrl);
}

function Alert429() {
    return swal.fire({
        title: '429',
        html: $.parseHTML(Err429Msg)[0].data,
        icon: 'warning',
        confirmButtonText: OkText
    });
}

function Alert(_Title, _Text, _Type = 'info') {
    return swal.fire({
        title: _Title,
        html: $.parseHTML(_Text)[0].data,
        icon: _Type, // warning, info, succcess, error
        confirmButtonText: OkText
    });
}

function Logout(_ReturnUrl) {
    for (var i = 0; i < 10; i++) {
        document.cookie = 'PrancaBeautyAuth' + i + '=; expires=Thu, 01-Jan-70 00:00:01 GMT; path=/;';
    }
    location.href = _ReturnUrl;
}

function ReloadPage() {
    location.reload();
}

function trim(Text, char) {
    var start = 0,
        end = Text.length;

    while (start < end && Text[start] === char)
        ++start;

    while (end > start && Text[end - 1] === char)
        --end;

    return (start > 0 || end < Text.length) ? Text.substring(start, end) : Text;
}

function CloseModal(_ModalId) {
    $('.' + _ModalId).remove();
    $('body').removeClass("modal-open");
}

function forgeryToken() { return kendo.antiForgeryTokens() }

function ShowModal(_ModalHtmlCode, _ModalId) {
    $('body').append("<div class='" + _ModalId + "'>" + _ModalHtmlCode + "<div class='modal-backdrop show'></div></div>");
    $('#' + _ModalId).modal({ keyboard: false, backdrop: false });
}

$(document).ready(function () {
    $('.color-code').each(function () {
        $(this).minicolors({
            format: 'rgb',
            opacity: 1
        });
    });
});