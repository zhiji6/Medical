function MillSecond(d) {
    var s = d.getMilliseconds();
    if (s.length == 1) {
        s = "00" + s;
        return s;
    } else if (s.length == 2) {
        s = "0" + s;
        return s;
    } else {
        return s;
    }
}

Date.prototype.format = function (format) {
    var args = {
        "yyyy": this.getFullYear(),
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": MillSecond(this)
    };

    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var i in args) {
        var n = args[i];
        if (new RegExp("(" + i + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? n : ("00" + n).substr(("" + n).length));
    }
    return format; 
};


//======================================广东省医保  Start=============================

var autoMethod = [];
/*原来的
function LoadData()
{
    autoMethod = [SYB9001, SYB1101, SYB2201, SYB2203, SYB2204, SYB2206, SYB2207, SYB2208
    , SYB2201, SYB2203, SYB2204, SYB2206, SYB2207, SYB2601
     , SYB2201, SYB2203, SYB2204, SYB2206, SYB2207, SYB5201, SYB5202, SYB5203, SYB5204, SYB5205, SYB5206
    , SYB3201, SYB3301, SYB3501, SYB3502, SYB3503, SYB3504
    , SYB3505, SYB3506, SYB5101, SYB5102, SYB7101, SYB7104];
    autoMethod = autoMethod.reverse();
}
*/

//3201等接口调用错误先去掉
function LoadData()
{
    autoMethod = [SYB9001, SYB1101, SYB2201, SYB2203, SYB2204, SYB2206, SYB2207, SYB2208
    , SYB2201, SYB2203, SYB2204, SYB2206, SYB2207, SYB2601
     , SYB2201, SYB2203, SYB2204, SYB2206, SYB2207, SYB5201, SYB5202, SYB5203, SYB5204, SYB5205, SYB5206
    , SYB3301, SYB3501, SYB3502, SYB3503, SYB3504
    , SYB3505, SYB3506, SYB5101, SYB5102, SYB7101, SYB7104];
    autoMethod = autoMethod.reverse();
}

function ClearData()
{
    autoMethod = [];
}



function autoInterface()
{    
    if (autoMethod.length < 1)
    {
        return;
    }
    var method = autoMethod.pop();
    if (method != undefined && method.constructor.name == "Function") {
        SYbDatainit();
        method();
    }    
}

//省医保公共报文输入
var SYBDataEntityIn = {
    "infno": "",
    "msgid": $("#fixmedins_code").val() + (new Date().format("yyyyMMddhhmmssS")) + "1",
    "mdtrtarea_admvs": "440300",
    "insuplc_admdvs": "440300",
    "recer_sys_code": "SY",
    "dev_no": "",
    "dev_safe_info": "",
    "cainfo": "",
    "signtype": "",
    "infver": "V1.0",
    "opter_type": "1",
    "opter": $("#opter_code").val(),
    "opter_name": $("#opter_name").val(),
    "inf_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
    "fixmedins_code": $("#fixmedins_code").val(),
    "fixmedins_name": $("#fixmedins_name").val(),
    "sign_no": $("#sign_no").val(),
    "input": ""
}


var SYBDataCommon = {
    "mdtrt_cert_type": "02",
    "mdtrt_cert_no": $("#certno").val(),
    "card_sn": "",
    "omsgid":$("#omsgid").val(),
    "psn_no": $("#psn_no").val(),
    "psn_name": $("#psn_name").val(),
    "gend":$("#gend").val(),
    "naty": $("#naty").val(),
    "brdy":$("#brdy").val(),
    "psn_cert_type": "1",
    "certno": $("#certno").val(),
    "fixmedins_type": "1",
    "fixmedins_name": $("#fixmedins_name").val(),
    "fixmedins_code": $("#fixmedins_code").val(),
    "ver": "1.0",
    "nowdata": new Date().format("yyyy-MM-dd"),
    "insutype": $("#insutype").val(),
    "begntime": new Date().format("yyyy-MM-dd hh:mm:ss"),
    "ipt_otp_no": $("#fixmedins_code").val() + new Date().format("yyyyMMddhhmmssS") + "1",
    "atddr_no": $("#dr_code").val(),
    "dr_name": $("#dr_name").val(),
    "dept_code": $("#dept_code").val(),
    "dept_name": $("#dept_name").val(),
    "caty": $("#caty").val(),
    "med_type": $("#med_type").val()
}

function SYbDatainit()
{
    SYBDataEntityIn = {
        "infno": "",
        "msgid": $("#fixmedins_code").val() + (new Date().format("yyyyMMddhhmmssS")) + "1",
        "mdtrtarea_admvs": "440300",
        "insuplc_admdvs": "440300",
        "recer_sys_code": "SY",
        "dev_no": "",
        "dev_safe_info": "",
        "cainfo": "",
        "signtype": "",
        "infver": "V1.0",
        "opter_type": "1",
        "opter": $("#opter_code").val(),
        "opter_name": $("#opter_name").val(),
        "inf_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
        "fixmedins_code": $("#fixmedins_code").val(),
        "fixmedins_name": $("#fixmedins_name").val(),
        "sign_no": $("#sign_no").val(),
        "input": ""
    }

    SYBDataCommon = {
        "mdtrt_cert_type": "02",
        "mdtrt_cert_no": $("#certno").val(),
        "card_sn": "",
        "psn_no": $("#psn_no").val(),
        "psn_name": $("#psn_name").val(),
        "gend":$("#gend").val(),
        "psn_cert_type": "1",
        "certno": $("#certno").val(),
        "fixmedins_type": "1",
        "fixmedins_name": $("#fixmedins_name").val(),
        "fixmedins_code": $("#fixmedins_code").val(),
        "ver": "1.0",
        "nowdata": new Date().format("yyyy-MM-dd"),
        "insutype": $("#insutype").val(),
        "begntime": new Date().format("yyyy-MM-dd hh:mm:ss"),
        "ipt_otp_no": $("#fixmedins_code").val() + new Date().format("yyyyMMddhhmmssS") + "1",
        "atddr_no": $("#dr_code").val(),
        "dr_name": $("#dr_name").val(),
        "dept_code": $("#dept_code").val(),
        "dept_name": $("#dept_name").val(),
        "caty": $("#caty").val(),
        "med_type": $("#med_type").val()
    }
}





var SYBDataMZ = {
    "ipt_otp_no": "",
    "mdtrt_id": "",
    "feedetl_sn": "",
    "chrg_bchno": "",
    "rxno":"",
    "brdy":"",
    "gend":"",
    "age":"",
    "msgid":"",
    "inf_refmsgid": "",
    "medfee_sumamt": "",
    "fulamt_ownpay_amt": "",
    "overlmt_selfpay": "",
    "preselfpay_amt": "",
    "inscp_scp_amt": "",
    "setl_id":"",
    "omsgid":"",
    "inv_data_type": "",
    "trt_dcla_detl_sn": "",
    "fixmedins_hilist_id":""
}
var SYBDataDown=
{
"file_qury_no":"",
"filename":"",    
}
var SYBDataML = [{
    "med_list_codg":$("#mlbm_001").val(),
    "medins_list_codg":$("#jgbm_001").val(),
    "pric":$("#mldj_001").val(),
    "mc":$("#mlmc_001").val(),
}

]

function SZSI_Smart_AJAX(param,callback)
{
    $.ajax({
        async: true,
        type: "POST",
        url: $("#txtUrl").val(),
        data: { "": JSON.stringify(param) },
        error: function (msg) {
            viewWindow(param.infno, msg, "error");
        }
    }).then(function (result) {

        if (result.infcode == 0) {            
                viewWindow(param.infno, "成功", "success");
                callback(result);
            } else {
                viewWindow(param.infno, result.err_msg, "error");
            }
    })
}

function SZSI_Smart_AJAX_File(param) {

    var url = $("#txtUrl").val();
    var ajax = null;
    if (XMLHttpRequest) {
        ajax = new XMLHttpRequest();
    } else {
        ajax = new ActiveXObject("Microsoft.XMLHTTP");
    }
    ajax.open('POST', url, true);        // 也可以使用POST方式，根据接口
    ajax.setRequestHeader("Content-Type", "application/json;charset=utf-8");
    ajax.responseType = "blob";    // 返回类型blob
    // 定义请求完成的处理函数，请求前也可以增加加载框/禁用下载按钮逻辑
    ajax.onreadystatechange  = function () {
        // 请求完成
        if (this.status === 200 && ajax.readyState == 4) {
            // 返回200
            var blob = this.response;
            var reader = new FileReader();
            reader.readAsDataURL(blob);    // 转换为base64，可以直接放入a的href
            reader.onload = function (e) {
                // 转换完成，创建一个a标签用于下载
                var a = document.createElement('a');
                //a.download = param.fileName;
                a.href = e.target.result;
                $("body").append(a);    // 修复firefox中无法触发click
                a.click();
                $(a).remove();
            }
        }
    };
    // 发送ajax请求
    //ajax.send(JSON.stringify(param));            
    ajax.send(JSON.stringify(param));
}

//签到
function SYB9001() {
    var signIn = {
        "opter_no": $("#opter_name").val(),
        "mac": "00-E0-70-64-C3-D2",
        "ip": "192.168.1.1"
    };
    SYBDataEntityIn.infno = "9001";
    SYBDataEntityIn.input = { "signIn": signIn };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
        if (data.infcode == 0) {
            SYBDataEntityIn.sign_no = data.output.signinoutb.sign_no;
            $("#sign_no").val(data.output.signinoutb.sign_no);
            autoInterface();
        }
    })
    //return param;
}


//签退
function SYB9002() {
    var signOut = {
        "opter_no": $("#opter_name").val(),
        "sign_no": $("#sign_no").val()
    };
    SYBDataEntityIn.infno = "9002";
    SYBDataEntityIn.input = { "signOut": signOut };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {})
    //return param;
}


//1101获取人员信息
function SYB1101() {    
    var data = {
        "mdtrt_cert_type":SYBDataCommon.mdtrt_cert_type,
        "mdtrt_cert_no":SYBDataCommon.mdtrt_cert_no,
        "card_sn":SYBDataCommon.card_sn,
        "begntime":"",
        "psn_cert_type":"",
        "certno":SYBDataCommon.certno,
        "psn_name": SYBDataCommon.psn_name
    }
    SYBDataEntityIn.infno = "1101";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
        if (data.infcode == 0) {
            $("#certno").val(data.output.baseinfo.certno);
            $("#psn_name").val(data.output.baseinfo.psn_name);
            $("#psn_no").val(data.output.baseinfo.psn_no);
            $("#gend").val(data.output.baseinfo.gend);
            SYBDataMZ.gend=data.output.baseinfo.gend;
            SYBDataMZ.brdy=data.output.baseinfo.brdy;
            SYBDataMZ.age=data.output.baseinfo.age;
            //SYBDataMZ.brdy=data.output.baseinfo.brdy;
            //$("#gend").val(data.output.baseinfo.gend);
            //$("#brdy").val(data.output.baseinfo.brdy);          
            $("#balc").val(data.output.insuinfo[0].balc);
            $("#psn_type").val(data.output.insuinfo[0].psn_type);
            $("#insutype").val(data.output.insuinfo[0].insutype);
            $("#insuplc_admdvs").val(data.output.insuinfo[0].insuplc_admdvs);            
            autoInterface();
        }
    })
    //return param;
}


//挂号
function SYB2201()
{
    var data = {
        "psn_no": SYBDataCommon.psn_no,
        "insutype": SYBDataCommon.insutype,
        "begntime": SYBDataCommon.begntime,
        "mdtrt_cert_type": SYBDataCommon.mdtrt_cert_type,
        "mdtrt_cert_no": SYBDataCommon.mdtrt_cert_no,
        "ipt_otp_no": SYBDataCommon.ipt_otp_no,
        "atddr_no":SYBDataCommon.atddr_no,
        "dr_name": SYBDataCommon.dr_name,
        "dept_code": SYBDataCommon.dept_code,
        "dept_name": SYBDataCommon.dept_name,
        "caty": SYBDataCommon.caty
    }
    SYBDataEntityIn.infno = "2201";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
          
        if (data.infcode == 0) {
            SYBDataMZ.mdtrt_id = data.output.data.mdtrt_id;
            SYBDataMZ.ipt_otp_no = data.output.data.ipt_otp_no;
            autoInterface();
        }
         
    })
    //var param = SYBDataEntityIn;
}

//挂号撤销
function SYB2202()
{
    var data = {
        "psn_no": SYBDataCommon.psn_no,
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "ipt_otp_no": SYBDataMZ.ipt_otp_no
    }
    SYBDataEntityIn.infno = "2202";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
}

//门诊就诊信息上传
function SYB2203()
{
    var mdtrtinfo = {
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "psn_no": SYBDataCommon.psn_no,
        "med_type": SYBDataCommon.med_type,
        "begntime":SYBDataCommon.begntime
    }
    var diseinfo = [
        {
            "diag_type": "3",
            "diag_srt_no": 1,
            "diag_code": "A17.30",
            "diag_name": "胃痛",
            "diag_dept": SYBDataCommon.dept_name,
            "dise_dor_no": SYBDataCommon.atddr_no,
            "dise_dor_name": SYBDataCommon.dr_name,
            "diag_time": SYBDataCommon.begntime,
            "vali_flag":"1"
        }
    ];
    SYBDataEntityIn.infno = "2203";
    SYBDataEntityIn.input = { "mdtrtinfo": mdtrtinfo, "diseinfo": diseinfo };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
}


//门诊费用明细信息上传
function SYB2204()
{
    var feedetail = [{
        "feedetl_sn": SYBDataEntityIn.msgid,
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "psn_no": SYBDataCommon.psn_no,
        "chrg_bchno": SYBDataEntityIn.msgid,
        "dise_codg":"",
        "rxno": SYBDataEntityIn.msgid,
        "rx_circ_flag":"0",
        "fee_ocur_time":SYBDataCommon.begntime,
        "med_list_codg": SYBDataML[0].med_list_codg,
        "medins_list_codg": SYBDataML[0].medins_list_codg,        
        "cnt": 1,
        "pric": SYBDataML[0].pric,
        "sin_dos_dscr":"",
        "used_frqu_dscr":"",
        "prd_days":0,
        "medc_way_dscr":"",
        "det_item_fee_sumamt": SYBDataML[0].pric * 1,
        "bilg_dept_codg": SYBDataCommon.dept_code,
        "bilg_dept_name": SYBDataCommon.dept_name,
        "bilg_dr_codg": SYBDataCommon.atddr_no,
        "bilg_dr_name": SYBDataCommon.dr_name,
        "hosp_appr_flag": "1",
        "acord_dept_codg":"",
        "acord_dept_name":"",
        "orders_dr_code":"",
        "orders_dr_name":"",
        "tcmdrug_used_way":"",
        "etip_flag":"",
        "etip_hosp_code":"",
        "dscg_tkdrug_flag":"",
        "matn_fee_flag":""
    }]
    SYBDataEntityIn.infno = "2204";
    SYBDataEntityIn.input = { "feedetail": feedetail };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
        if (data.infcode == 0) {
            SYBDataMZ.feedetl_sn = data.output.result.feedetl_sn;
            SYBDataMZ.chrg_bchno = feedetail[0].chrg_bchno;
            SYBDataMZ.rxno = feedetail[0].rxno;
            autoInterface();
        }
    })
}

//门诊费用明细信息撤销
function SYB2205()
{
    var data = {
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "chrg_bchno": SYBDataMZ.chrg_bchno,
        "psn_no":SYBDataCommon.psn_no
    }
    SYBDataEntityIn.infno = "2205";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
}

//门诊预结算
function SYB2206()
{
    var data = {
        "psn_no":SYBDataCommon.psn_no,
        "mdtrt_cert_type": SYBDataCommon.mdtrt_cert_type,
        "mdtrt_cert_no": SYBDataCommon.mdtrt_cert_type,
        "med_type": SYBDataCommon.med_type,
        "medfee_sumamt": SYBDataML[0].pric * 1,
        "psn_setlway": "01",
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "chrg_bchno": SYBDataMZ.chrg_bchno,
        "acct_used_flag": "1",
        "insutype": SYBDataCommon.insutype
    }
    SYBDataEntityIn.infno = "2206";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
        if (data.infcode == 0) {
            SYBDataMZ.medfee_sumamt = data.output.setlinfo.medfee_sumamt;
            SYBDataMZ.fulamt_ownpay_amt = data.output.setlinfo.fulamt_ownpay_amt;
            SYBDataMZ.overlmt_selfpay = data.output.setlinfo.overlmt_selfpay;
            SYBDataMZ.preselfpay_amt = data.output.setlinfo.preselfpay_amt;
            SYBDataMZ.inscp_scp_amt = data.output.setlinfo.inscp_scp_amt;//            
            SYBDataMZ.setl_id = data.output.setlinfo.medins_setl_id;//medins_setl_id
            autoInterface();
        }
    })
}

//门诊结算
function SYB2207()
{
    var data = {
        "psn_no": SYBDataCommon.psn_no,
        "mdtrt_cert_type": SYBDataCommon.mdtrt_cert_type,
        "mdtrt_cert_no": SYBDataCommon.mdtrt_cert_no,
        "med_type": SYBDataCommon.med_type,
        "medfee_sumamt": SYBDataMZ.medfee_sumamt,
        "psn_setlway": "01",
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "chrg_bchno": SYBDataMZ.chrg_bchno,
        "insutype": SYBDataCommon.insutype,
        "acct_used_flag": "1",
        "fulamt_ownpay_amt": SYBDataMZ.fulamt_ownpay_amt,
        "overlmt_selfpay": SYBDataMZ.overlmt_selfpay,
        "preselfpay_amt": SYBDataMZ.preselfpay_amt,
        "inscp_scp_amt": SYBDataMZ.inscp_scp_amt
    }
    SYBDataEntityIn.infno = "2207";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
        if (data.infcode == 0) {
            SYBDataMZ.setl_id = data.output.setlinfo.setl_id;
            SYBDataCommon.psn_no=data.output.setlinfo.psn_no;
            SYBDataMZ.mdtrt_id=data.output.setlinfo.mdtrt_id;
            //$("#omsgid").val(SYBDataEntityIn.msgid);
            SYBDataMZ.omsgid = SYBDataEntityIn.msgid;
            autoInterface();
        }
    })
}


//门诊结算撤销
function SYB2208()
{
    var data = {
        "setl_id": SYBDataMZ.setl_id,
        "mdtrt_id": SYBDataMZ.mdtrt_id,
        "psn_no": SYBDataCommon.psn_no
    }
    SYBDataEntityIn.infno = "2208";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
}



function SYB1301()
{
    var data = {
        "ver": SYBDataCommon.ver
    };
    SYBDataEntityIn.infno = "1301";
    SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) 
    {
       // $("#file_qury_no").val(data.output.file_qury_no);
        SYBDataDown.file_qury_no = data.output.file_qury_no;
        SYBDataDown.filename = data.output.filename;
        autoInterface();
    })
    //return param;
}

    function SYB1302()
    {
        var data = {
            "ver": SYBDataCommon.ver
        };
        SYBDataEntityIn.infno = "1302";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) 
        {
            SYBDataDown.file_qury_no = data.file_qury_no;
            SYBDataDown.filename = data.filename;
            autoInterface();
        })
    }

    function SYB1303()
    {
        var data = {
            "ver": SYBDataCommon.ver
        };
        SYBDataEntityIn.infno = "1303";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
    }

    function SYB1304()
    {
        var data = {
            "med_list_codg": SYBDataML[0].med_list_codg,
            "genname_codg":"",
            "drug_genname":"",
            "drug_prodname":"",
            "reg_name":"",
            "tcmherb_name":"",
            "mlms_name":"",
            "vali_flag":"",
            "rid":"",
            "ver":SYBDataCommon.ver,
            "ver_name":"",
            "opt_begn_time":"",
            "opt_end_time":"",
            "updt_time":SYBDataCommon.nowdata,
            "page_num":10,
            "page_size":1
        }
        SYBDataEntityIn.infno = "1304";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            SYBDataDown.file_qury_no = data.file_qury_no;
            SYBDataDown.filename = data.filename;
            autoInterface();
        })
    }

    function SYB1305()
    {
        var data = {
            "ver": SYBDataCommon.ver
        };
        SYBDataEntityIn.infno = "1305";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            SYBDataDown.file_qury_no = data.file_qury_no;
            SYBDataDown.filename = data.filename;
            autoInterface();
        })
    }

    function SYB1306()
    {
        var data = {
            "ver": SYBDataCommon.ver
        };
        SYBDataEntityIn.infno = "1306";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            SYBDataDown.file_qury_no = data.file_qury_no;
            SYBDataDown.filename = data.filename;
            autoInterface();
        })
    }

    function SYB1312()
    {
        var data = {
            "query_date":"",
            "hilist_code":"",
            "insu_admdvs":"",
            "begndate":"",
            "hilist_name":"",
            "wubi":"",
            "pinyin":"",
            "med_chrgitm_type":"",
            "chrgitm_lv":"",
            "lmt_used_flag":"",
            "list_type":"",
            "med_use_flag":"",
            "matn_used_flag":"",
            "hilist_use_type":"",
            "lmt_cpnd_type":"",
            "vali_flag":"",
            "updt_time": SYBDataCommon.nowdata,
            "page_num":10,
            "page_size":1
        }
        SYBDataEntityIn.infno = "1312";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
    }


    function SYB1314()
    {
        var data = {
            "ver": SYBDataCommon.ver
        };
        SYBDataEntityIn.infno = "1314";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            SYBDataDown.file_qury_no = data.file_qury_no;
            SYBDataDown.filename = data.filename;
            autoInterface();
        })
    }

    function SYB1315()
    {
        var data = {
            "ver": SYBDataCommon.ver
        };
        SYBDataEntityIn.infno = "1315";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            SYBDataDown.file_qury_no = data.file_qury_no;
            SYBDataDown.filename = data.filename;
            autoInterface();
        })
    }

    function SYB1316()
    {
        var data = {
            "query_date":"",
            "medins_list_codg":"",
            "hilist_code":"",
            "list_type":"",
            "insu_admdvs":"",
            "begndate":"",
            "vali_flag":"",
            "updt_time":new Date().format("yyyy-MM-dd"),
            "page_num":1,
            "page_size":100
        }
        SYBDataEntityIn.infno = "1316";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }


    function SYB1317() {
        var data = {
            "query_date":"",
            "fixmedins_code":"",
            "medins_list_codg":"",
            "medins_list_name":"",
            "insu_admdvs":"",
            "list_type":"",
            "med_list_codg": SYBDataML[0].med_list_codg,
            "begndate":"",
            "vali_flag":"",
            "updt_time":SYBDataCommon.nowdata,
            "page_num":10,
            "page_size":1
        }
        SYBDataEntityIn.infno = "1317";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB1318() {
        var data = {
            "query_date":"",
            "hilist_code":"",
            "hilist_lmtpric_type":"",
            "overlmt_dspo_way":"",
            "insu_admdvs":"",
            "begndate":"",
            "enddate":"",
            "vali_flag":"",
            "rid":"",
            "tabname":"",
            "poolarea_no":"",
            "updt_time" :SYBDataCommon.nowdata,
            "page_num" :1,
            "page_size":10
        }
        SYBDataEntityIn.infno = "1318";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB1319() {
        var data = {
            "query_date":"",
            "hilist_code":"",
            "selfpay_prop_psn_type":"",
            "selfpay_prop_type":"",
            "insu_admdvs":"",
            "begndate":"",
            "enddate":"",
            "vali_flag":"",
            "rid":"",
            "tabname":"",
            "poolarea_no":"",
            "updt_time" : SYBDataCommon.nowdata,
            "page_num": 10,
            "page_size" :1
        }
        SYBDataEntityIn.infno = "1319";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB1901() {
        var data = {
            "type":"",
            "parentValue":"",
            "admdvs":"440300",
            "date":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "valiFlag":"1",
        }
        SYBDataEntityIn.infno = "1901";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            if (data.infcode == 0) {
                SYBDataEntityIn.psn_no = data.output.trtinfo.psn_no;
                $("#psn_no").val(data.output.signinoutb.psn_no);
                autoInterface();
            }
        })
        //return param;
    }

    function SYB2001() {
        var data = {
            "psn_no":SYBDataCommon.psn_no,
            "insutype":SYBDataCommon.insutype,
            "fixmedins_code":SYBDataCommon.fixmedins_code,
            "med_type":"41",//普通门诊
            "begntime":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "endtime":"",
            "dise_codg":"",
            "dise_name":"",
            "oprn_oprt_code":"",
            "oprn_oprt_name":"",
            "matn_type":"",
            "birctrl_type":""
        }
        SYBDataEntityIn.infno = "2001";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
       // return param;
    }

    function SYB2601() {
        var data = {
            "psn_no":SYBDataCommon.psn_no,
            "omsgid":SYBDataMZ.omsgid,
            "oinfno":"2207"
        }
        SYBDataEntityIn.infno = "2601";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB3301() {
        SYBDataMZ.fixmedins_hilist_id = SYBDataML[0].med_list_codg + (new Date().format("ss"));
        var data = {
            "fixmedins_hilist_id": SYBDataMZ.fixmedins_hilist_id,
            "fixmedins_hilist_name":SYBDataML[0].mc,
            "list_type":"101",//	目录类别	字符型	30		Y
            "med_list_codg":"101",//SYBDataML[0].med_list_codg,//	医疗目录编码	字符型	30		Y
            "begndate":new Date().format("yyyy-MM-dd"),
            "enddate":"2099-01-01",
            "aprvno":"",
            "dosform":"",
            "exct_cont":"",
            "item_cont":"",
            "prcunt":"",
            "spec":"",           
        };
        SYBDataEntityIn.infno="3301",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {  autoInterface(); })
        //return param;
    }
    function SYB3302() {
        var data = {
            "fixmedins_code":SYBDataEntityIn.fixmedins_code,
            "fixmedins_hilist_id":SYBDataMZ.fixmedins_hilist_id,//SYBDataML[0].med_list_codg,
            //"fixmedins_hilist_name":SYBDataML[0].mc,
            "list_type":"101",//	目录类别	字符型	30		Y
            "med_list_codg":SYBDataML[0].med_list_codg,//	医疗目录编码	字符型	30		Y
                  
        };
        SYBDataEntityIn.infno="3302",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB3201() {
        var data = {
            "insutype":SYBDataCommon.insutype,
            "clr_type":"11",//门诊
            "setl_optins":"440300",
            "stmt_begndate":new Date().format("yyyy-MM-dd"),
            "stmt_enddate":new Date().format("yyyy-MM-dd"),
            "medfee_sumamt":2.00,
            "fund_pay_sumamt":0.00,
            "acct_pay":2.00,
            "fixmedins_setl_cnt":1
        }
        SYBDataEntityIn.infno="3201";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
             autoInterface(); 
            })
        //return param;
    }


    function SYB3202() {
        var data = {
            "setl_optins":"440300",
            "file_qury_no":"",
            "stmt_begndate":"",
            "stmt_enddate":"",
            "medfee_sumamt":"",
            "fund_pay_sumamt":"",
            "cash_payamt":"",
            "fixmedins_setl_cnt":""
        }
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })

    }

    function SYB3401() {
        var data = {
            "hosp_dept_codg": "A01",
            "caty": "中医科",
            "hosp_dept_name": "中医科室",
            "begntime": new Date().format("yyyy-MM-dd"),
            "endtime": "",
            "itro": "中医科室",
            "dept_resper_name": "张医生",
            "dept_resper_tel": "13715113333",
            "dept_med_serv_scp": "",
            "dept_estbdat":  new Date().format("yyyy-MM-dd"),
            "aprv_bed_cnt": 0,
            "hi_crtf_bed_cnt": "",
            "poolarea_no": "440300",
            "dr_psncnt": 1,
            "phar_psncnt": 1,
            "nurs_psncnt": 1,
            "tecn_psncnt": 1,
            "memo": "",
            
        };
        SYBDataEntityIn.infno = "3401";
        SYBDataEntityIn.input = { "deptinfo": data };
        var param = SYBDataEntityIn;  
        SZSI_Smart_AJAX(param, function (data) {
            if (data.infcode == 0) {
                SYBDataEntityIn.hosp_dept_codg = data.hosp_dept_codg;
                SYBDataEntityIn.hosp_dept_name = data.hosp_dept_name;
                SYBDataEntityIn.begntime = data.begntime;
                autoInterface();
            }
        })    
        //return param;
    }

    function SYB3402() {
        var data = {
            "hosp_dept_codg":"A01",
            "hosp_dept_name":"中医科室",
            "begntime": new Date().format("yyyy-MM-dd"),
            "endtime": "",
            "itro": "科室简介",
            "dept_resper_name": "口腔科",//
            "dept_resper_tel": "13715113376",
            "dept_med_serv_scp": "",
            "caty":  "中医科",
            "dept_estbdat": new Date().format("yyyy-MM-dd"),
            "aprv_bed_cnt": 0,
            "hi_crtf_bed_cnt": 0,
            "poolarea_no": "440300",
            "dr_psncnt": 1,
            "phar_psncnt": 1,
            "nurs_psncnt": 1,
            "tecn_psncnt": 1,
            "memo": ""
        };
        SYBDataEntityIn.infno="3402";
        SYBDataEntityIn.input = { "deptinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
       // return param;
    }

    function SYB3403() {
        var data = {
            "hosp_dept_codg": "A01",
            "hosp_dept_name":  "口腔科",
            "begntime": new Date().format("yyyy-MM-dd")
        };
        SYBDataEntityIn.infno="3403";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB5101() {
        var data = {       
        };
        SYBDataEntityIn.infno="5101";
        SYBDataEntityIn.input = "";
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB5102() {
        var data = {  
            "prac_psn_type":"1",//	执业人员分类	字符型	6	Y　1	医师	3	药师            2	护士	4	医技人员    
            "psn_cert_type":"",//	人员证件类型	字符型	6	Y	　
            "certno":"",
            "prac_psn_name":"",
            "prac_psn_code":""    
        };
        SYBDataEntityIn.infno="5102";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB1201() {
        var data = {  
            "fixmedins_type":"1",
            "fixmedins_name":SYBDataEntityIn.fixmedins_name,
            "fixmedins_code":SYBDataEntityIn.fixmedins_code,            
        };
        SYBDataEntityIn.infno="1201";
        SYBDataEntityIn.input = { "medinsinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { })
        //return param;
    }
    function SYB3501() {
        var data = {  
            "med_list_codg":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_id":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_name":"维生素C泡腾片",
            "rx_flag":"1",
            "invdate":new Date().format("yyyy-MM-dd"),//	盘存日期	日期型	　	　	Y　	yyyy-MM-dd
            "inv_cnt":10,
            "manu_lotnum":"",
            "fixmedins_bchno":"0001",
            "manu_date":new Date().format("yyyy-MM-dd"),
            "expy_end":new Date().format("yyyy-MM-dd"),
            "memo":""                     
        };
        SYBDataEntityIn.infno="3501";
        SYBDataEntityIn.input = { "invinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data){
            SYBDataEntityIn.begntime = data.invdate;
            SYBDataMZ.inv_data_type = 1;
            autoInterface();
        })
        }
    function SYB3502() {
        var data = {  
            "med_list_codg":SYBDataML[0].med_list_codg,	　
            "inv_chg_type":"101",
            "fixmedins_hilist_id":SYBDataML[0].med_list_codg,//	定点医药机构目录编号	字符型	30	　	Y　	　
            "fixmedins_hilist_name":"维生素C泡腾片",
            "fixmedins_bchno":SYBDataEntityIn.msgid,
            "pric":"1",
            "cnt":20,
            "rx_flag":"1",//	处方药标志	字符型	3	Y　	Y　	　
            "inv_chg_time":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "inv_chg_opter_name":"测试人员",
            "memo":"",
            "trdn_flag":""            
        };
        SYBDataEntityIn.infno="3502";
        SYBDataEntityIn.input = { "invinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data)
        {
            SYBDataMZ.omsgid = data.fixmedins_bchno;
            SYBDataMZ.inv_data_type = 2;
            autoInterface();
        })
    }
    function SYB3503() {
        var data = {  
            "med_list_codg":SYBDataML[0].med_list_codg,
            "fixmedins_hilist_id":SYBDataML[0].medins_list_codg,
            "fixmedins_hilist_name":"维生素C泡腾片",
            "dynt_no":"",
            "fixmedins_bchno":"0001",
            "spler_name":"gysName",//"spler_name":"供应商名称",
            "spler_pmtno":"",
            "manu_lotnum":"0001",	
            "prdr_name": "scsName",//"prdr_name": "生产厂家名称",
            "prodentp_name": "cjName",//"prodentpName": "厂家名",
            "aprvno":"批准文号",
            "manu_date":"2021-01-01",
            "expy_end":"2088-12-31",           
            "finl_trns_pric":1,
            "purc_retn_cnt":10,
            "purc_invo_codg":"",
            "purc_invo_no":"",
            "rx_flag":"1",
            "purc_retn_stoin_time":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "purc_retn_opter_name":"test",
            "prod_geay_flag":"0",
            "memo":"",
                      
        };
        SYBDataEntityIn.infno="3503";
        SYBDataEntityIn.input = { "purcinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data)
        {
            SYBDataMZ.omsgid = param.input.purcinfo.fixmedins_bchno;
            SYBDataMZ.inv_data_type = 3;
            autoInterface();
        })
    }
    function SYB3504() {
        var data = {  
            "med_list_codg":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_id":SYBDataML[0].medins_list_codg,	　
            "fixmedins_hilist_name":"维生素C泡腾片",
            "dynt_no":"",
            "fixmedins_bchno":"0001",
            "spler_name":"供应商名称",
            "spler_pmtno":"",            
            "manu_date":"2021-01-01",
            "expy_end":"2088-12-31",           
            "finl_trns_pric":"",
            "purc_retn_cnt":1,
            "purc_invo_codg":"",
            "purc_invo_no":"ftest001",//发票号
            "rx_flag":"1",
            "purc_retn_stoin_time":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "purc_retn_opter_name":"test",
            "prod_geay_flag":"0",
            "memo":"",
            "medins_prod_purc_no":""//流水号
                      
        };
        SYBDataEntityIn.infno="3504";
        SYBDataEntityIn.input = { "purcinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data)
        {
            SYBDataMZ.omsgid = param.input.purcinfo.fixmedins_bchno;
            SYBDataMZ.inv_data_type = 3;
            autoInterface();
        })
    }
    function SYB3505() {
        var data = {  
            "med_list_codg":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_id":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_name":"维生素C泡腾片",
            "fixmedins_bchno":"0001",
            "prsc_dr_cert_type":"",
            "prsc_dr_certno":"",
            "prsc_dr_name":"张医生",
            "phar_cert_type":"",
            "phar_certno":"",
            "phar_name":"张医生",
            "phar_prac_cert_no":"y1111",
            "hi_feesetl_type":"",
            "setl_id":"",
            "mdtrt_sn":SYBDataEntityIn.msgid,
            "psn_no":"",
            "psn_cert_type":"",
            "certno": "",
            "psn_name":"",
            "manu_lotnum":"0001",                                   
            "manu_date":"2021-01-01",
            "expy_end":"2088-12-31",   
            "rx_flag":1,
            "trdn_flag":"0",
            "finl_trns_pric":1,
            "rxno":SYBDataEntityIn.msgid,
            "rx_circ_flag":"0",
            "rtal_docno":SYBDataEntityIn.msgid,
            "stoout_no":"",
            "bchno":"",
            "drug_trac_codg":""	,
            "drug_prod_barc":"",
            "shelf_posi":"",	
            "sel_retn_cnt":"1",
            "sel_retn_time":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "sel_retn_opter_name":"test",
            "memo":"",                 
        };
        SYBDataEntityIn.infno="3505";
        SYBDataEntityIn.input = { "selinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data)
        {
            SYBDataMZ.omsgid = SYBDataCommon.msgid;
            SYBDataMZ.inv_data_type = 4;
            autoInterface();
        })
    }
    function SYB3506() {
        var data = {  
            "med_list_codg":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_id":SYBDataML[0].med_list_codg,	　
            "fixmedins_hilist_name":"维生素C泡腾片",
            "fixmedins_bchno":"0001",//??          
            "setl_id":"",
            //"mdtrt_sn":SYBDataMZ.omsgid,
            "psn_no":"",
            "psn_cert_type":"",
            "certno": "",
            "psn_name":"",
            "manu_lotnum":"0001",                                   
            "manu_date":"2021-01-01",
            "expy_end":"2088-12-31",   
            "rx_flag":1,
            "trdn_flag":"0",
            "finl_trns_pric":"",
            "sel_retn_cnt":1,
            "sel_retn_time":new Date().format("yyyy-MM-dd"),
            "sel_retn_opter_name":"test",
            "memo":"",  
            "medins_prod_sel_no":"",               
        };
        SYBDataEntityIn.infno="3506";
        SYBDataEntityIn.input = { "selinfo": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data)
        {
            SYBDataMZ.omsgid = "0001";
            SYBDataMZ.inv_data_type = "4";
            autoInterface();
        })
    }
    function SYB3507() {
        var data = {  
            "fixmedins_bchno":SYBDataML[0].med_list_codg,
            "inv_data_type":"2",
        }
        SYBDataEntityIn.infno="3507";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data)
        {
            autoInterface();
        })
    }
    function SYB2505() {
        var data = {
            "psn_no":SYBDataCommon.psn_no,
            "tel":"13715112276",	
            "addr":	"联系地址",
            "biz_appy_type":"01",//01	门诊慢特病登记	08	异地安置登记            03	就医定点医疗机构登记		            
            "begndate":new Date().format("yyyy-MM-dd"),
            "enddate":"2099-01-01",
            "agnter_name":"",
            "agnter_cert_type":"",
            "agnter_certno":"",
            "agnter_tel":"",
            "agnter_addr":"",
            "agnter_rlts":"",
            "fix_srt_no":"1",
            "fixmedins_code":SYBDataEntityIn.fixmedins_code,
            "fixmedins_name":SYBDataEntityIn.fixmedins_name,
            "memo":""
            
        };
        SYBDataEntityIn.infno="2505",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data)
        {
            SYBDataMZ.trt_dcla_detl_sn = data.output.trt_dcla_detl_sn;
            autoInterface();
        })     
}
function SYB2506() {
    var data = {
        "psn_no": SYBDataCommon.psn_no,
        "trt_dcla_detl_sn": SYBDataMZ.trt_dcla_detl_sn,
        "memo":"患者暂时不需要"
        
    };
    SYBDataEntityIn.infno = "2506",
        SYBDataEntityIn.input = { "data": data };
    var param = SYBDataEntityIn;
    SZSI_Smart_AJAX(param, function (data) {
        autoInterface();
    })
}

    function SYB4601() {
        var data = {
            "mdtrt_sn":SYBDataEntityIn.msgid,
            "mdtrt_id":SYBDataMZ.mdtrt_id,	
            "psn_no":SYBDataEntityIn.psn_no,
            "abo_code":"",
            "rh_code":"",
            "bld_natu_code":"",
            "bld_abo_code":"4",	
            "bld_rh_code":"3",
            "bld_cat_code":"",
            "bld_amt":"",
            "bld_amt_unt":"",
            "bld_defs_type_code":"",
            "bld_cnt":"",
            "bld_time":"",
            "bld_rea":"",
            "vali_flag":"1",                   
        };
        SYBDataEntityIn.infno="4601",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB4201() {
        var data = {
            "mdtrt_sn":SYBDataMZ.mdtrt_id,
            "ipt_otp_no": SYBDataMZ.ipt_otp_no,
            "med_type": "11",
            "chrg_bchno": SYBDataEntityIn.msgid,
            "feedetl_sn": SYBDataEntityIn.msgid,
            "psn_cert_type":SYBDataCommon.psn_cert_type,
            "certno": SYBDataCommon.certno,
            "psn_name": SYBDataCommon.psn_name,
            "fee_ocur_time": SYBDataCommon.begntime,
            "cnt": 1,
            "pric": SYBDataML[0].pric,
            "det_item_fee_sumamt":  SYBDataML[0].pric * 1,
            "med_list_codg": SYBDataCommon.dept_code,
            "medins_list_codg":  SYBDataML[0].medins_list_codg,
            "medins_list_name": SYBDataCommon.fixmedins_name,
            "med_chrgitm_type": "09",//西药
            "prodname": SYBDataML.mlms_name,
            "bilg_dept_codg": SYBDataCommon.dept_code,
            "bilg_dept_name": SYBDataCommon.dept_name,
            "bilg_dr_codg":  SYBDataCommon.atddr_no,
            "bilg_dr_name": SYBDataCommon.dr_name,
            "acord_dept_codg": "",
            "acord_dept_name": "",
            "orders_dr_code": "",
            "orders_dr_name": "",
            "tcmdrug_used_way": "",
            "etip_flag": "",
            "etip_hosp_code": "",
            "dscg_tkdrug_flag": "",
            "memo": ""
        };
        SYBDataEntityIn.infno = "4201";
        SYBDataEntityIn.input = { "feedetail": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB4301() {
        var rgstinfo = {
            "mdtrt_sn": "",
            "mdtrt_id": "",
            "psn_no": "",
            "rgst_type_code": "",
            "rgst_way_code": "",
            "rgst_serv_fee": "",
            "ordr_way_code": "",
            "retnr_flag": "",
            "fstdiag_flag": "",
            "mdtrt_flag": "",
            "rgst_retnr_time": "",
            "medfee_paymtd_code": "",
            "vali_flag": ""
        };

        var caseinfo = [{
            "mdtrt_date": "",
            "chfcomp": "",
            "attk_date_time": "",
            "mdtrt_rea": "",
            "illhis": "",
            "algs": "",
            "aise_code": "",
            "phex": "",
            "disa_info_code": "",
            "symp_name": "",
            "symp_code": "",
            "dspo_opnn": "",
            "dept_code": "",
            "dept_name": "",
            "vali_flag": ""
        }];

        var diseinfo = [{
            "tcm_diag_flag": "",
            "maindiag_flag": "",
            "diag_code": "A17.30",
            "diag_name": "胃痛",
            "tcm_dise_code": "",
            "tcm_dise_name": "",
            "tcmsymp_code": "",
            "tcmsymp": "",
            "vali_flag": ""
        }]

        var rxinfo = [{
            "rxno": SYBDataEntityIn.msgid,
            "rx_prsc_time": "",
            "rx_type_code": "",
            "rx_item_type_code": "",
            "rx_item_type_name": "",
            "rx_detl_id": "",
            "rx_detl_name": "",
            "tcmdrug_type_name": "",
            "tcmdrug_type_code": "",
            "tcmherb_foote": "",
            "medn_type_code": "",
            "medn_type_name": "",
            "drug_dosform": "",
            "drug_dosform_name": "",
            "drug_spec": "",
            "drug_used_frqu": "",
            "drug_used-idose ": "",
            "drug_used_sdose": "",
            "drug_used_dosunt": "",
            "drug_used_way_code": "",
            "drug_medc_way": "",
            "skintst_dicm": "",
            "medc_begntime": "",
            "medc_endtime": "",
            "medc_days": "",
            "main_medc_flag": "",
            "urgt_flag": "",
            "unif_purc_drug": "",
            "drug_purc_code": "",
            "drug_mgt_plaf_code": "",
            "bas_medn_flag": "",
            "vali_flag": ""
        }]
        SYBDataEntityIn.input = { "rgstinfo": rgstinfo, "caseinfo": caseinfo, "diseinfo": diseinfo, "rxinfo": rxinfo };
        var param = SYBDataEntityIn;
        return param;
    
    }

    function SYB4701() {
        var adminfo = {
            "mdtrt_sn": "",
            "mdtrt_id": "",
            "psn_no": "",
            "mdtrtsn": "",
            "name": "",
            "gend": "",
            "age": "",
            "adm_rec_no": "",
            "wardarea_name": "",
            "dept_code": "",
            "dept_name": "",
            "bedno": "",
            "adm_time": "",
            "illhis_stte_name": "",
            "illhis_stte_rltl": "",
            "stte_rele": "",
            "chfcomp": "",
            "dise_now": "",
            "hlcon": "",
            "dise_his": "",
            "ifet": "",
            "ifet_his": "",
            "prev_vcnt": "",
            "oprn_his": "",
            "bld_his": "",
            "algs_his": "",
            "psn_his": "",
            "mrg_his": "",
            "mena_his": "",
            "fmhis": "",
            "physexm_tprt": "",
            "physexm_pule": "",
            "physexm_vent_frqu": "",
            "physexm_systolic_pre": "",
            "physexm_dstl_pre": "",
            "physexm_height": "",
            "physexm_wt": "",
            "physexm_ordn_stas": "",
            "physexm_skin_musl": "",
            "physexm_spef_lymph": "",
            "physexm_head": "",
            "physexm_neck": "",
            "physexm_chst": "",
            "physexm_abd": "",
            "physexm_finger_exam": "",
            "physexm_genital_area": "",
            "physexm_spin": "",
            "physexm_all_fors": "",
            "nersys": "",
            "spcy_info": "",
            "asst_exam_rslt": "",
            "tcm4d_rslt": "",
            "syddclft": "",
            "syddclft_name": "",
            "prnp_trt": "",
            "rec_doc_code": "",
            "rec_doc_name": "",
            "ipdr_code": "",
            "ipdr_name": "",
            "chfdr_code": "",
            "chfdr_name": "",
            "chfpdr_code": "",
            "chfpdr_name": "",
            "main_symp": "",
            "adm_rea": "",
            "adm_way": "",
            "apgr": "",
            "diet_info": "",
            "growth_deg": "",
            "mtl_stas_norm": "",
            "slep_info": "",
            "sp_info": "",
            "mind_info": "",
            "nurt": "",
            "self_ablt": "",
            "nurscare_obsv_item_name": "",
            "nurscare_obsv_rslt": "",
            "smoke": "",
            "stop_smok_days": "",
            "smok_info": "",
            "smok_day": "",
            "drnk": "",
            "drnk_frqu": "",
            "drnk_day": "",
            "eval_time": "",
            "resp_nurs_name": "",
            "vali_flag": "",

        }
        var diseinfo = [{
            "inout_diag_type": "",
            "maindiag_flag": "",
            "diag_seq": "",
            "diag_time": "",
            "wm_diag_code": "",
            "wm_diag_name": "",
            "tcm_dise_code": "",
            "tcm_dise_name": "",
            "tcmsymp_code": "",
            "tcmsymp": "",
            "vali_flag": ""
        }];
        var coursrinfo = [{
            "dept_code": "",
            "dept_name": "",
            "wardarea_name": "",
            "bedno": "",
            "rcd_time": "",
            "chfcomp": "",
            "cas_ftur": "",
            "tcm4d_rslt": "",
            "dise_evid": "",
            "prel_wm_diag_code": "",
            "prel_tcm_dise_name": "",
            "prel_tcm_diag_code": "",
            "prel_tcm_dise_name": "",
            "prel_tcmsymp_code": "",
            "prel_tcmsymp": "",
            "finl_wm_diag_code": "",
            "finl_wm_diag_name": "",
            "finl_tcm_dise_code": "",
            "finl_tcm_dise_name": "",
            "finl_tcmsymp_code": "",
            "finl_tcmsymp": "",
            "dise_plan": "",
            "prnp_trt": "",
            "ipdr_code": "",
            "ipdr_name": "",
            "prnt_doc_name": "",
            "vali_flag": ""
        }];
        var oprninfo = [{
            "oprn_appy_id": "",
            "oprn_seq": "",
            "blotype_abo": "",
            "oprn_time": "",
            "oprn_type_code": "",
            "oprn_type_name": "",
            "bfpn_diag_code": "",
            "bfpn_oprn_diag_name": "",
            "bfpn_inhosp_ifet": "",
            "afpn_diag_code": "",
            "afpn_diag_name": "",
            "sinc_heal_lv": "",
            "sinc_heal_lv_code": "",
            "back_oprn": "",
            "selv": "",
            "prev_abtl_medn": "",
            "abtl_medn_days": "",
            "oprn_oprt_code": "",
            "oprn_oprt_name": "",
            "oprn_lv_code": "",
            "oprn_lv_name": "",
            "anst_mtd_code": "",
            "anst_mtd_name": "",
            "anst_lv_code": "",
            "anst_lv_name": "",
            "exe_anst_dept_code": "",
            "exe_anst_dept_name": "",
            "anst_efft": "",
            "oprn_begntime": "",
            "oprn_endtime": "",
            "oprn_asps": "",
            "oprn_asps_ifet": "",
            "afpn_info": "",
            "oprn_merg": "",
            "oprn_conc": "",
            "oprn_anst_dept_code": "",
            "oprn_anst_dept_name": "",
            "palg_dise": "",
            "oth_med_dspo": "",
            "out_std_oprn_time": "",
            "oprn_oper_name": "",
            "oprn_asit_name1": "",
            "oprn_asit_name2": "",
            "anst_dr_name": "",
            "anst_asa_lv_code": "",
            "anst_asa_lv_name": "",
            "anst_medn_code": "",
            "anst_medn_name": "",
            "anst_medn_dos": "",
            "anst_dosunt": "",
            "anst_begntime": "",
            "anst_endtime": "",
            "anst_merg_symp_code": "",
            "anst_merg_symp": "",
            "anst_merg_symp_dscr": "",
            "pacu_begntime": "",
            "pacu_endtime": "",
            "oprn_selv": "",
            "canc_oprn": "",
            "vali_flag": ""
        }]
        var rescinfo = [{
            "dept": "",
            "dept_name": "",
            "wardarea_name": "",
            "bedno": "",
            "diag_name": "胃痛",
            "diag_code": "A17.30",
            "cond_chg": "",
            "resc_mes": "",
            "oprn_oprt_code": "",
            "oprn_oprt_name": "",
            "oprn_oper_part": "",
            "itvt_name": "",
            "oprt_mtd": "",
            "oprt_cnt": "",
            "resc_begntime": "",
            "resc_endtime": "",
            "dise_item_name": "",
            "dise_ccls": "",
            "dise_ccls_qunt": "",
            "dise_ccls_code": "",
            "mnan": "",
            "resc_psn_list": "",
            "proftechttl_code": "",
            "doc_code": "",
            "dr_name": "",
            "vali_flag": ""
        }]
        var dieinfo = [{
            "dept": "",
            "dept_name": "",
            "wardarea_name": "",
            "bedno": "",
            "adm_time": "",
            "adm_dise": "",
            "adm_info": "",
            "trt_proc_dscr": "",
            "die_time": "",
            "die_drt_rea": "",
            "die_drt_rea_code": "",
            "die_dise_name": "",
            "die_diag_code": "",
            "agre_corp_dset": "",
            "ipdr_name": "",
            "chfpdr_code": "",
            "chfpdr_name": "",
            "chfdr_name": "",
            "sign_time": "",
            "vali_flag": ""
        }]
        var dscginfo = [{
            "dscg_date": "",
            "adm_diag_dscr": "",
            "dscg_dise_dscr": "",
            "adm_info": "",
            "trt_proc_rslt_dscr": "",
            "dscg_info": "",
            "dscg_drord": "",
            "caty": "",
            "rec_doc": "",
            "main_drug_name": "",
            "oth_imp_info": "",
            "vali_flag": ""
        }]
        SYBDataEntityIn.input = { "adminfo": adminfo, "diseinfo": diseinfo, "coursrinfo": coursrinfo, "oprninfo": oprninfo, "rescinfo": rescinfo, "dieinfo": dieinfo, "dscginfo": dscginfo };
        var param = SYBDataEntityIn;
        return param;
    }


    function SYB5201() {
        var data = {
            "psn_no":SYBDataCommon.psn_no,
            "begntime":"2021-04-01 10:28:33",
            "endtime":"2021-04-10 10:28:33",
            "med_type": "11",
            "mdtrt_id": ""
        };
        SYBDataEntityIn.infno="5201";
        
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;//mdtrt_id
        SZSI_Smart_AJAX(param, function (data)
        {
            if( data.output.mdtrtinfo.length > 0)
            {
                SYBDataMZ.mdtrt_id = data.output.mdtrtinfo[0].mdtrt_id;
            }            
            autoInterface();
        }
        )
        //return param;
    }

    function SYB5202() {
        var data = {
            "mdtrt_id": SYBDataMZ.mdtrt_id,
            "psn_no": SYBDataCommon.psn_no,
        };
        SYBDataEntityIn.input = { "data": data };
        SYBDataEntityIn.infno="5202";
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            autoInterface();
        } )
        //return param;
    }

    function SYB5203() {
        var data = {
            "psn_no":SYBDataCommon.psn_no,
            "setl_id":SYBDataMZ.setl_id,
            "mdtrt_id":SYBDataMZ.mdtrt_id,
        };
        SYBDataEntityIn.infno="5203",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            autoInterface();
        }
        )
        //return param;
    }

    function SYB5204() {
        var data = {
            "psn_no":SYBDataCommon.psn_no,
            "setl_id":SYBDataMZ.setl_id,
            "mdtrt_id":SYBDataMZ.mdtrt_id,
        };
        SYBDataEntityIn.infno="5204",
        SYBDataEntityIn.input = { "data": data };       
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            autoInterface();
        }
        )
        //return param;
    }

    function SYB5205() {
        var data = {
            "psn_no": SYBDataCommon.psn_no,
            "begntime":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "endtime": ""
        };
        SYBDataEntityIn.infno="5205",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            autoInterface();
        }
        )
       // return param;
    }

    function SYB5206() {
        var data = {
            "psn_no": SYBDataCommon.psn_no,
            "cum_ym": ""        
        };
        SYBDataEntityIn.infno="5206";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            autoInterface();
        }
        )

       // return param;
    }

    function SYB5301() {
        var data = {
            "psn_no": SYBDataCommon.psn_no,
        };
        SYBDataEntityIn.infno="5301";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }


    function SYB5302() {
        var data = {
            "psn_no": SYBDataCommon.psn_no,
            "biz_appy_type":"08"//01	门诊慢特病登记	08	异地安置登记            03	就医定点医疗机构登记		
            
        };
        SYBDataEntityIn.infno="5302";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB5303() {
        var data = {
            "psn_no": SYBDataCommon.psn_no,
            "begntime": new Date().format("yyyy-MM-dd"),
            "endtime":"9999-12-31"
 
        };
        SYBDataEntityIn.infno="5303";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB5304() {
        var data = {
            "psn_no": SYBDataCommon.psn_no,
            "begntime": new Date().format("yyyy-MM-dd"),
            "endtime": "9999-12-31"
        };
        SYBDataEntityIn.infno="5304";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB7101() {
        var data = {
            "hosp_rxno": SYBDataEntityIn.msgid,
            "init_rxno": "",
            "rx_type_code": "1",//门诊西药
            "prsc_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
            "rx_drug_nums": "1",
            "rx_way_codg": "",
            "rx_way_name": "",
            "rx_freq_codg": "",
            "rx_freq_name": "",
            "rx_dosunt": "",
            "rx_doscnt": "",
            "rx_drord_dscr": "",
            "valid_days": "30",
            "valid_end_time": "2021-05-12 00:00:00",
            "rept_flag": "",
            "max_rept_cnt": "",
            "reptd_cnt": "",
            "min_inrv_days": "",
            "dr_sign_info": "",
            "phar_sign_info": "",
            "fixmedins_sign_info": "",
            "rx_cotn_flag": "0",//0 否 1 是
            "rx_file": "/9j/4AAQSkZJRgABAQAASABIAAD/4QCARXhpZgAATU0AKgAAAAgABAESAAMAAAABAAEAAAEaAAUAAAABAAAAPgEbAAUAAAABAAAARodpAAQAAAABAAAATgAAAAAAAABIAAAAAQAAAEgAAAABAAOgAQADAAAAAQABAACgAgAEAAAAAQAAAoCgAwAEAAAAAQAAAoAAAAAA/+0AOFBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAAOEJJTQQlAAAAAAAQ1B2M2Y8AsgTpgAmY7PhCfv/AABEIAoACgAMBIgACEQEDEQH/xAAfAAABBQEBAQEBAQAAAAAAAAAAAQIDBAUGBwgJCgv/xAC1EAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHBFVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+fr/xAAfAQADAQEBAQEBAQEBAAAAAAAAAQIDBAUGBwgJCgv/xAC1EQACAQIEBAMEBwUEBAABAncAAQIDEQQFITEGEkFRB2FxEyIygQgUQpGhscEJIzNS8BVictEKFiQ04SXxFxgZGiYnKCkqNTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqCg4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2dri4+Tl5ufo6ery8/T19vf4+fr/2wBDAAkJCQkJCRAJCRAWEBAQFh4WFhYWHiYeHh4eHiYuJiYmJiYmLi4uLi4uLi43Nzc3NzdAQEBAQEhISEhISEhISEj/2wBDAQsMDBIREh8RER9LMyozS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0v/3QAEACj/2gAMAwEAAhEDEQA/APcaKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAEooooAKKKKAFooooAKKKKACiiigAooooA//Q9xooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooASiiigAooooAWiiigAooooAKKKKACiiigD/9H3GiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigBKKKKACiiigBaKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigBKKKKACiiigAooooAKKKKACiiigAooooAWiiigAooooA//9L3GiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigBKKKKACiiigBaKSigBaKSloAKKKKACiikoAWiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAoopKAFopKKAFopKKACiiigAooooAKKKKACiiigAooooAKKKKAFopKWgAopKKAP/9P3GiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKSlpKAG0UtFKwgpaKKYBRRRTGFFJRSsFh1FFFIAooooAKKKKACiiigAooopgFFFFABRRRQAUUUUAJRRRQAUUUUAFFFFABRRRQAUUlLRYApKWipaAbRS0UIQUUUVYWFooooGFFFFIAooooAKKKKACikpaACiiimISlpKWgEJSUtFBR/9T3GiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKSlpKAEoooyKYC0UlJUNgOpKKKEwEopaQVZQ6lpKKRItFJRQAtFJRQAtFJRQAtFFFABRRRQAUUUUAFFFFACUUUUAFFFFABRRRQAUUUUAJRRRTGFLScUtAhKKKKQXCiikzTC4t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9xooooASilooASiiigAooooAKKWigBKKWigBtFFFABRRRQA1aU0LQaQmNqCY4Q1PVK5fEbVrBXMJnn+tTDLCvNbtwZTXZ6xKTI4rhJ8mQ172HRw1B64pSKag4p5r04o45DKKKKskKKKKYC0UUU7AFFFFFgBaGpVpHpJAC0N1oSlbrWdW1i4mtpq5da9U0ZQFGK8z0lMuteq6VHtUV4WJZ3UkdNGPlFPxSJ0FPNeRI9CK0FooopFhRRRQAUUUUAFJS0UAJS0lLQxDaaafTTSRMiAnANcR4jlwldrNwprzfxLL8pFd+GWtzlqHCai+TWcDU94xY1XAr36KOKZIelRGpKjNdqOZiCnimU6mIKKKKACiiigAooooQBRRRTew0LjirVkhY1VBrX0yPdXm4hnTBHfeGo8LXfouAMVx/h6PC12qjgV4Nd6nbBEo6UtFFcZ2IKKKKBi0UUUAFFFFAH//1PcaKKKACiiigBKKKKACiiigBaKKKACiikoASiiigAooooABSGlFIaBMYelZd22I2+lajdKxL5gI2+lb0lqc82eV6o+ZnFcnIMua3NSlzcyD3rALZavfw6OCoyUDApDTh0pDXoxRySGUUUVQgooopgFFJRTAWikopgSLTHp4qN6gEKlPP3hUaVKAS4rnqy0Noo6zRIssteqWEe1BXnGgoSV4r1K1XCCvBxMjupovJ0p5qNaVuteazuiSUUlLSKCiiigAooooAKKKKAEpaSloYCGmGnmmGmkTIqXbYQ15V4llzke9el3z4Q15L4ikySAc816WFicdVnMz84qI8CnyHIFROele7SRwzYtNNLRXWYDaKWkoELRRRQAUUUUAFFFFJAFFFFKTKiMGc10+jR7hXOqvNdjoUeVry8RI6qaO+0NNq11I7Vh6QmFreArw6z947oIkooormOpBRRRQAtFFFABRRRQB/9X3GiiigAooooAKKKKACiiigBKKKKACiiigBKKKKACiiigBjDmlalakbpTExrHCmuVv5cI4zXSynCGuG1KbG8V14danJUZ5dekm8l+tZ561cuWzcyGqh6179BaHBMkXpTWpRSGu5GDFooopkiUlLSUwCiiiqAKKKKAFNAoNArNlICatQDLiqZPNXrVcyqK4a8jeCPQdATG3ivSohhBXCaLDtCmu9jHyCvBxD1O+mh6dakpi1JXGzqiFFFFIoKKKKACiiigAooooAQ0CigUDFNV3NTnpVaQ8GriRIx79/wB030rxvUXLXMgY55r1fUJPkavH79s3Mn1r18NE4KrKIPJphIzSDqaj717NJaHDNk1FHaiugyEpKWkoASiiigAooooAdTTTqaaQBTqaKdWU2XEVeWFd94bT5K4SJdzivSPDcX7uvKxDOymjvdPXC1prVKyXC1fFeJUep3wQ6iiisjYKKKKACiiigAooooA//9b3GiiigAooooAKKKKACiiigBKKKKACiiigBKKKKACiiigCJzyKe3QUjDJFK3SqFIq3BxGa831aXBcV6HdtiM/SvKNZl+dxmu7DI4qrOLmbM7moaVuZGNFe9RWhwSYop1NFOFdqRkxKKKKCRKSlpKYBRRRTAKKKKAA0vam0+spFRGAfNWxYx5nSspfvCug05D9ojyO9ediGdUD1DTIdqLXVJ90Vi2SYjXjtW2nSvBrPU7oIeKdSClrmOlBRRRQMKKKKACiiigAooooAKSlpKBiHpVWX7pq03Sqkx+Rq0gZyOP1CXhxXk922bmSvRtQlGZBmvL7libmSvbw6PPqgnOaaRzSRk80Z5r16exwzH0UUlakBSUtJQAlFFFABRRRQA6mmnU00mMBTqaKcK56hpEuWqZcV6l4ci/dV5rYjLivWfDyFYfmGK8fEy0Z20jqLdcCrQqGIYFSDrXjy3O+KH0UUVJYUUUUAFFFFABRRRQB//9f3GiiigAooooAKKKKACiiigAooooAKSlooAbRRRQAUUUUARDOaJaeBzUc3SnHcU9jLv2xEfpXj2ruTM/Neq6nJiJq8f1OTdO9ethkebWZjHqaaad3ppr2qRwSAU6minV2IkSkooqWAUUUUgCiiigAooopjA9aeaiPWnk1Eti4jP+W6D3rtbFB58RxXGIM3Ef1r0HT4cyRmvLxDOmmemW4HlL9K0E6VSgH7tfpV5eleDU3O+A+iiisjpQtFFFIQUUUUAFFFFABRRRQAlIaWkoYw7Vn3RxG1aB6VlXr4jb6VrTWphUZ5bqsh81wDXGzYLk10mpy5nkHvXMSHLGvew6PPqMSkxS0V6sDkkR08UzvT61JFpKWkNSA2iiigAoooqgHU006m1IwFOHWkFOUfMK5a2xpE19MjJuY/rXstgm2NMcV5VpUX+kR/WvYbVMRr9K8LEM7aSNVelBpg7U8jNeaz0Ii0tFFSMKKKKACiiigAooooA//Q9xooooAKKKKACiiigAooooAKKKKACiiigBtFFFABRRRQAhqC4Py1KetQXJ+WrgtSJvQ5TV5MRtXkV6+64evUtacBGya8muTm4evZw6PNrMhppp1NNevTRxSAU6m06upCG0UUVIgooopAFFFFABRRS0DGkc05hR3pzDpWc2XEdAubqP616npsGdhrzG1Um7jxXsGlx5RDXk4lnXTR1cI+QVaFQRjCipxXhTZ3QQtLSUtQdAtFFFAgooooAKKKKACiiigBKSim96BsVjgVz2oSYjb6VvSn5TXJ6nIAjc100FqclRnlOoSZuZPrWMeWq/etm5k+tUO9e/QicE2ONNzQ1MzXoROZi0+mU8VqIWkNFFSA2iiigAoooqgHUlFFQxocKkQZkUUwVLCM3CCuOs9DWKOx0uLE0Zr1i3H7tfpXndhCQ8ZAr0eEfIteBiHqd9JFrFPpBTXOOlcJ2xH0UgpaQwooooAKKKKACiiigD//0fcaKKKACiiigAooooAKKKKACiiigAooooASkpaSgBKKKKAEas+8JArQ61lak21K1p7mM3ocJ4kkIj4Neby8uTXbeI5soa4fOSa9vDo86qMooor1oI42JSUtJWwBRRRUiCiiigApaSloAWiiigBMc1KRxUWeamByK55s0iXtLjzMp969h0hMIua8u0aPdIpr13TU2oK8bFS0O6kjbUcU8U0dKcK8aTO6KFooooRqLRRRQIKKKKAEooooAKKKKAAUhoBoNNAyKU/Ia4LWnIDYNdxcNhDXnGtzfertw0Tiqs84mP8ApD5qI06Q5mY1Ga+gox0PPkxDSYp2KdiutGLGUUUVoSFFFFSAUUUUAFFFFUArUq000orKTLQ8mrVqubmOqQPNatjHuuY/rXBXeh0QR6jpkY2rkV2UI+UVzmnRYRa6ZBhRXg13qd1ND6dTaWuQ64i0UUUDCiiigBaKKKACiiigD//S9xooooAKKKKACiiigAooooAKKKKACiiigBKSlpKAEpDS0hoAbWBrMm1K3m4Fcnrj4XmuiktTnmzzrXpNymuXWuh1hgw61zte5h0efVCiiivUgcjEpKWkqxhRRRRYQUUUUhBS0lLQAtFFFAEZzmpU60pUU6NcsMVyVGaQOt0GPLKa9Ys12oK828OxEleK9RgXCCvExUj0KSLg6UopBS15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p1JigB1FJRSsAtGaSimA6kpaKYxKKKKQBmlzSUUALmjNJRQAUUUUAFFFFABRRRTJCkpaKQC0UUUhhRRRSGLRRRVCCiiigAooooAKKKKAP//S9xooooAKKKKACiiigAooooASiiigAooooASilooASilooASiiigQUUUUXAKKKKBhRRRTASiiigAoooqGwG0tJS0riFpaSlq7hcKKKKkYUUUUwCilpKoApKKSgB9FFFIAooooAKKKKAExRilooAZRS0YqbgJRS0UXAWikpadxBRRRTC4UUUUguFFFFAXCiiigLhRRRQFwopaKYCUUtFIYlFFFIYUlFFADqKKKoQUUUUAFFFFABRRRQB//0/caKKKACiiigAooooAKKKKACiiigAooooASiiigAooooAbRRRQAUUUUAOooooAKKKKAG0lLRQAUUUUALS0UUAFFFFACUUUUAFFFFABRRRQAUUUUALRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAlFFFABRRRQAtFFFABRRRQAUUUUAFFFFABRRRQAUUUUALRRRQAUUUUAf/U9xooooAKKKKACiiigAooooAKKKKACiiigBKKKKACiiigBKKWigBKKWigAooooAKKKKAEopaKAEopaKACiiigAooooASiiigAooooAKKKKACiiigBaKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAEooooAKKKKAFooooAKKKKACiiigAooooAKKKKACiiigBaKKKACiiigD/9X3GiiigAooooAKKKKACiiigAooooAKKKKAEooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAFooooAKKKKAP/1vcaKKKACiiigAooooAKKKKACiiigAooooASiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAWiiigAooooA//Z",
            "rx_circ_flag": "1",
            "drug_res":"abc",
            "gend":SYBDataCommon.gend,
            "insutype":SYBDataCommon.insutype
        }
        var rxdrugdetail = [{
            "med_list_codg": SYBDataML[0].med_list_codg,
            "fixmedins_hilist_id": "",
            "hosp_prep_flag": "",
            "rx_item_type_code": "",
            "rx_item_type_name": "",
            "tcmdrug_type_name": "",
            "tcmdrug_type_code": "",
            "tcmherb_foote": "",
            "medn_type_code": "",
            "medn_type_name": "",
            "main_medc_flag": "",
            "urgt_flag": "",
            "bas_medn_flag": "",
            "imp_drug_flag": "",
            "prod_barc": "",
            "drug_prodname": "",
            "genname_codg": "",
            "drug_genname": SYBDataML[0].mc,
            "chemname": "",
            "drugstdcode": "",
            "drug_dosform": "片剂",//药品剂型          
            "drug_spec": "1.25g",//药品规格
            "prdr_name": "",
            "drug_pric": "",
            "drug_cnt": "1",
            "drug_cnt_unit": "片",//单位
            "drug_sumamt": "",
            "medc_way_codg": "1",
            "medc_way_dscr": "测试",
            "medc_starttime":new Date().format("yyyy-MM-dd hh:mm:ss"),
            "medc_endtime": new Date().format("yyyy-MM-dd hh:mm:ss"),
            "medc_days": "1",
            "drug_dosunt": "",
            "sin_doscnt": "",
            "sin_dosunt": "",
            "used_frqu_codg": "",
            "used_frqu_name": "",
            "drug_totlnt": "",
            "drug_totlnt_emp": "",
            "dise_codg": "",
            "drug_res":"abc",
            "gend":SYBDataCommon.gend,
            "insutype":SYBDataCommon.insutype
        }];
        var mdtrtinfo = {
            "mdtrt_id": SYBDataEntityIn.msgid,
            "med_type": "11",
            "ipt_op_no": SYBDataEntityIn.msgid,
            "psn_no": SYBDataCommon.psn_no,
            "patn_name": SYBDataCommon.psn_name,
            "age":"37.5",
            "patn_wt": "",
            "gend": SYBDataMZ.gend,
            "geso_val": "",
            "nwb_flag": "",
            "nwb_age": "",
            "suck_prd_flag": "",
            "algs_his": "",
            "insuplc_admdvs": "440300",
            "psn_cert_type": SYBDataCommon.psn_cert_type,
            "certno": SYBDataCommon.certno,
            "insutype": SYBDataCommon.insutype,
            "prsc_dept_name": SYBDataCommon.dept_name,
            "prsc_dept_code": SYBDataCommon.dept_code,
            "prsc_dr_name": SYBDataCommon.dr_name,
            "prsc_dr_cert_type": "",
            "prsc_dr_certno": "",
            "dr_profttl_codg": "",
            "dr_profttl_name": "",
            "phar_cert_type": "",
            "phar_certno": "",
            "phar_name": SYBDataCommon.dr_name,
            "phar_prac_cert_no": "",
            "phar_chk_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
            "mdtrt_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
            "dise_codg": "",
            "dise_name": "",
            "sp_dise_flag": "0",
            "diag_code": "A17.30",// 
            "diag_name":"胃痛",
            "dise_cond_dscr": "",
            "hi_feesetl_type": "",
            "hi_feesetl_name": "",
            "rgst_fee": "0",
            "medfee_sumamt": "",
            "fstdiag": "",
            "drug_res":"abc",
            "gend":SYBDataCommon.gend,
            "insutype":SYBDataCommon.insutype
        }
        var diseinfo = [{
            "diag_type": "1",
            "maindiag_flag": "1",
            "diag_srt_no": "1",
            "diag_code": "A17.30",
            "diag_name":"胃痛",
            "diag_dept":"1051",
            "dise_dor_no": SYBDataEntityIn.opter,       
            "dise_dor_name":SYBDataEntityIn.opter_name,
            "diag_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
        }]
        SYBDataEntityIn.infno="7101",
        SYBDataEntityIn.input = { "data": data, "rxdrugdetail": rxdrugdetail, "mdtrtinfo": mdtrtinfo, "diseinfo": diseinfo };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param,function(data){
            if (data.infcode == 0) {
                SYBDataMZ.rxno = data.output.hi_rxno;
                autoInterface();
            }
        })
        //return param;
    }

    function SYB7104() {
        var data = {
            "hi_rxno": SYBDataMZ.rxno,
            "prsc_dr_name": SYBDataCommon.dr_name,
            "undo_dr_cert_type": "",
            "undo_dr_certno": "4200111",
            "undo_rea": "近日不需要",
            "undo_time": new Date().format("yyyy-MM-dd hh:mm:ss"),
        }
        SYBDataEntityIn.infno="7104";
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        // return param;
    }



    function SYB9101() {
        var fsUploadIn = {
            "in": "",
            "filename": "name",
            "fixmedins_code": SYBDataCommon.fixmedins_code
        };
        var SYbDatain=[{
    
        }]
        SYBDataEntityIn.infno="9101",
        SYBDataEntityIn.input = { "fsUploadIn": fsUploadIn,"in":SYbDatain};
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    function SYB9102() {
        var fsDownloadIn = {
            "file_qury_no": SYBDataDown.file_qury_no,//.split('/')[2],// "cfc15266eadb4692a22ffb131afca8",//SYBDataDown.file_qury_no,//订正/ "fsi/plc/cfc15266eadb4692a22ffb131afca8"
            "filename":  SYBDataDown.filename,//"plc",
            "fixmedins_code": "plc"//"plc"// SYBDataCommon.fixmedins_code
        };
        SYBDataEntityIn.infno="9102",
        SYBDataEntityIn.input = { "fsDownloadIn": fsDownloadIn };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) {
            // 转换完成，创建一个a标签用于下载
            var a = document.createElement('a');
            //a.download = param.fileName;
            a.href = data.file;
            $("body").append(a);    // 修复firefox中无法触发click
            a.click();
            $(a).remove();
            autoInterface();
        })
        //return param;
    }

    function SYB3101() {
        var data = {
           "syscode":"sy",
           "patient_dtos":"",
           "rule_ids":"",
           "task_id":"",	
           "trig_scen":"23",
        };
        var patient_dtos=[
            {
                "patn_id": SYBDataCommon.psn_no,
                "patn_name":SYBDataCommon.psn_name,
                "gend":SYBDataMZ.gend,
                "brdy":SYBDataMZ.brdy,                           
                "poolarea":"440300",
                "curr_mdtrt_id":"440300",
                "fsi_encounter_dtos":"",
                "fsi_his_data_dto":"",
        }]
        var fsi_encounter_dtos=[
            {
                "mdtrt_id":SYBDataMZ.mdtrt_id ,
                "medins_id":SYBDataEntityIn.fixmedins_code,
                "medins_name":SYBDataEntityIn.fixmedins_name,
                "medins_admdvs":"440300",
                "medins_type":"1",
                "medins_lv":"1",
                "wardarea_codg":"",
                "wardno":"",
                "bedno":"",
                "adm_date":new Date().format("yyyy-mm-dd"),
                "dscg_date":new Date().format("yyyy-mm-dd"),
                "dscg_main_dise_codg":"I63.9",
                "dscg_main_dise_name":"脑梗塞",
                "fsi_diagnose_dtos":"",
                "dr_codg":SYBDataCommon.atddr_no,
                "adm_dept_codg":SYBDataCommon.dept_code,
                "adm_dept_name":SYBDataCommon.dept_name,
                "dscg_dept_codg":SYBDataCommon.dept_code,
                "dscg_dept_name":SYBDataCommon.dept_name,
                "med_mdtrt_type":"1",//类型搜索不到 1门诊
                "med_type":"11",//普通门诊 11
                "fsi_order_dtos":"",
                "matn_stas":"0",
                "matn_stas":SYBDataML[0].pric * 1,
                "medfee_sumamt":"0",
                "ownpay_amt":"0",
                "selfpay_amt":"0",
                "acct_payamt":"",
                "ma_amt":"",
                "hifp_payamt":"",
                "setl_totlnum":"1",
                "insutype":SYBDataCommon.insutype,
                "reim_flag":"1",
                "out_setl_flag":"1",
                "fsi_operation_dtos":""
            }
        ]
        var fsi_diagnose_dtos =[{
            "dise_id":SYBDataCommon.msgid,
            "inout_dise_type":"",//?
            "maindise_flag":"",//
            "dias_srt_no":"1",
            "dise_codg" :""  ,
            "dise_name":"",
            "dise_date":new Date().format("yyyy-MM-dd"),
        }]
        SYBDataEntityIn.infno="3101",
        SYBDataEntityIn.input = { "data": data,"patient_dtos":patient_dtos,"fsi_encounter_dtos":fsi_encounter_dtos,"fsi_diagnose_dtos":fsi_diagnose_dtos };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }
    function SYB3102() {
        var data = {
            "file_qury_no": "",
            "filename": "plc",
            "fixmedins_code": "",
        };
        SYBDataEntityIn.infno="3102",
        SYBDataEntityIn.input = { "data": data };
        var param = SYBDataEntityIn;
        SZSI_Smart_AJAX(param, function (data) { autoInterface(); })
        //return param;
    }

    //======================================广东省医保  End=============================