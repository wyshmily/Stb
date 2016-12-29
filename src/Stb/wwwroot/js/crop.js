
accessid = 'Cx6nmkDFzea2meyL';
accesskey = 'quR4Nrj4D69nhLRPH0hfnrBjtLZlcX';
host = 'http://ebank.oss-cn-qingdao.aliyuncs.com';

g_dirname = ''
g_object_name = ''
g_object_name_type = 'random_name'
now = timestamp = Date.parse(new Date()) / 1000;

g_rt_object_name = ''
g_x = 0;
g_y = 0;
g_w = 0;
g_h = 0;


var policyText = {
    "expiration": "2020-01-01T12:00:00.000Z", //设置该Policy的失效时间，超过这个失效时间之后，就没有办法通过这个policy上传文件了
    "conditions": [
        ["content-length-range", 0, 1048576000] // 设置上传文件的大小限制
    ]
};

var policyBase64 = Base64.encode(JSON.stringify(policyText))
message = policyBase64
var bytes = Crypto.HMAC(Crypto.SHA1, message, accesskey, { asBytes: true });
var signature = Crypto.util.bytesToBase64(bytes);

//function check_object_radio() {
//    var tt = document.getElementsByName('myradio');
//    for (var i = 0; i < tt.length; i++) {
//        if (tt[i].checked) {
//            g_object_name_type = tt[i].value;
//            break;
//        }
//    }
//}

//function get_dirname() {
//    dir = document.getElementById("dirname").value;
//    if (dir != '' && dir.indexOf('/') != dir.length - 1) {
//        dir = dir + '/'
//    }
//    //alert(dir)
//    g_dirname = dir
//}

function random_string(len) {
    len = len || 32;
    var chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';
    var maxPos = chars.length;
    var pwd = '';
    for (i = 0; i < len; i++) {
        pwd += chars.charAt(Math.floor(Math.random() * maxPos));
    }
    return pwd;
}

function get_suffix(filename) {
    pos = filename.lastIndexOf('.')
    suffix = ''
    if (pos != -1) {
        suffix = filename.substring(pos)
    }
    return suffix;
}

function calculate_object_name(filename) {
    if (g_object_name_type == 'local_name') {
        g_object_name += "${filename}"
    }
    else if (g_object_name_type == 'random_name') {
        suffix = get_suffix(filename)
        g_object_name = g_dirname + random_string(15) + suffix
    }
    return ''
}

function get_uploaded_object_name(filename) {
    if (g_object_name_type == 'local_name') {
        tmp_name = g_object_name
        tmp_name = tmp_name.replace("${filename}", filename);
        return tmp_name
    }
    else if (g_object_name_type == 'random_name') {
        return g_object_name
    }
}

function set_upload_param(up, filename, ret) {
    g_object_name = g_dirname;
    if (filename != '') {
        suffix = get_suffix(filename)
        calculate_object_name(filename)
    }
    new_multipart_params = {
        'key': g_object_name,
        'policy': policyBase64,
        'OSSAccessKeyId': accessid,
        'success_action_status': '200', //让服务端返回200,不然，默认会返回204
        'signature': signature,
    };

    up.setOption({
        'url': host,
        'multipart_params': new_multipart_params
    });

    up.start();
}

var uploader = new plupload.Uploader({
    runtimes: 'html5,flash,silverlight,html4',
    browse_button: 'selectfiles',
    //multi_selection: false,
    container: document.getElementById('container'),
    flash_swf_url: 'lib/plupload-2.1.2/js/Moxie.swf',
    silverlight_xap_url: 'lib/plupload-2.1.2/js/Moxie.xap',
    url: 'http://oss.aliyuncs.com',
    filters: [
        { title: "Image files", extensions: "jpg,gif,png" },
        { title: "Zip files", extensions: "zip" }
    ],


    init: {
        PostInit: function () {
            document.getElementById('ossfile').innerHTML = '';
            //document.getElementById('postfiles').onclick = function () {
            //    set_upload_param(uploader, '', false);
            //    return false;
            //};
        },

        FilesAdded: function (up, files) {
            plupload.each(files, function (file) {
                document.getElementById('ossfile').innerHTML = '<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ')<b></b>'
                    + '<div class="progress"><div class="progress-bar" style="width: 0%"></div></div>'
                    + '</div>';
            });
            set_upload_param(uploader, '', false);
        },

        BeforeUpload: function (up, file) {
            //check_object_radio();
            //get_dirname();
            set_upload_param(up, file.name, true);
        },

        UploadProgress: function (up, file) {
            var d = document.getElementById(file.id);
            d.getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
            var prog = d.getElementsByTagName('div')[0];
            var progBar = prog.getElementsByTagName('div')[0]
            progBar.style.width = 2 * file.percent + 'px';
            progBar.setAttribute('aria-valuenow', file.percent);
        },

        FileUploaded: function (up, file, info) {
            if (info.status == 200) {
                g_rt_object_name = get_uploaded_object_name(file.name);
                document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = ' 上传完毕，请剪裁头像';
                $("#crop-div").css("display", "block");
                $("#crop-container").empty().append($('<img id="portrait-origin" />'));
                $("#portrait-origin").prop("src", "http://ebank-oss.ivmchina.net/" + g_rt_object_name)
                $('#portrait-origin').Jcrop({
                    onSelect: saveCoords,
                    onChange: saveCoords,
                    bgColor: 'black',
                    bgOpacity: .4,
                    setSelect: [0, 0, 200, 200],
                    aspectRatio: 1,
                }, function () {
                    var jcrop_api = this;
                    thumbnail = this.initComponent('Thumbnailer', { width: 150, height: 150 });
                    this.ui.preview = thumbnail;
                });


            }
            else {
                document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = info.response;
            }
        },

        Error: function (up, err) {
            //document.getElementById('console').appendChild(document.createTextNode("\nError xml:" + err.response));
        }
    }
});

uploader.init();

function saveCoords(c) {
    g_w = c.w;
    g_h = c.h;
    g_x = c.x;
    g_y = c.y;
};


$(document).ready(function () {
    $("#save-crop").click(function (event) {
        event.preventDefault();

        $("#crop-div").css("display", "none");

        var src = "http://ebank-oss.ivmchina.net/" + g_rt_object_name + "?x-oss-process=image/crop,x_" + g_x + ",y_" + g_y
            + ",w_" + g_w + ",h_" + g_h + "/resize,m_lfit,h_150,w_150";
        $("#portrait-img").prop("src", src);
        $("#Portrait").val(src);

        clearFileUi();
    });

    $("#cancel-crop").click(function (event) {
        event.preventDefault();
        $("#crop-div").css("display", "none");
        clearFileUi();
    });
})

function clearFileUi() {
    $('#ossfile').empty();
}