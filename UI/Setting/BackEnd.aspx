
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="../easyui/themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="../themes/demo.css"/>
    <script type="text/javascript" src="../easyui/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../easyui/jquery.easyui.min.js"></script>
    <style type="text/css">
        
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="easyui-tabs" style="width:700px;height:550px">
        <div title="计费参数" style="padding:10px">
            <p>
              入住后,<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟允许退单。 <input type="checkbox" /> 登记时必须输入押金，不得低于<input type="text" class="easyui-numberbox" style="width:50px;"/>元
           </p>
           <p>
              天房超过退房时间可以宽限<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟，钟点房超时可以宽限<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟
          </p>
          <hr/>
           <p>
               <input type="checkbox"/>启用凌晨房，<input class="easyui-timespinner" style="width:80px;"/>到<input class="easyui-timespinner" style="width:80px;"/>开房执行凌晨房价格 
          </p>
          <p>
            &nbsp; &nbsp;凌晨房退房时间<input class="easyui-timespinner" style="width:80px;"/>超时，
              <select class="easyui-combobox" name="state" style="width:100px;">
               <option value="AL">Alabama</option> <option value="AK">Alaska</option>
               <option value="AZ">Arizona</option>
                <option value="AR">Arkansas</option>
          </select>,直到<select class="easyui-combobox" name="state" style="width:80px;">
               <option value="AL">次日</option> <option value="AK">次日</option>
               <option value="AZ">次日</option>
                <option value="AR">次日</option>
          </select>
              <input class="easyui-timespinner" style="width:80px;"/>
              转为<select class="easyui-combobox" name="state" style="width:80px;">
               <option value="AL">Alabama</option> <option value="AK">Alaska</option>
               <option value="AZ">Arizona</option>
                <option value="AR">Arkansas</option>
          </select>
          </p>
            <p>
               &nbsp; &nbsp;每隔<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟加收<input type="text" class="easyui-numberbox" style="width:50px;"/>元，
               不足<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟，超过<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟，加收<input type="text" class="easyui-numberbox" style="width:50px;"/>元
            </p>
            <hr/>
            <p>
             天房退房时间<input class="easyui-timespinner" style="width:80px;"/>超时，
              <select class="easyui-combobox" name="state" style="width:100px;">
               <option value="AL">Alabama</option> <option value="AK">Alaska</option>
               <option value="AZ">Arizona</option>
                <option value="AR">Arkansas</option>
          </select>,直到<select class="easyui-combobox" name="state" style="width:80px;">
               <option value="AL">次日</option> <option value="AK">次日</option>
               <option value="AZ">次日</option>
                <option value="AR">次日</option>
          </select>
              <input class="easyui-timespinner" style="width:80px;"/>加收全天费用
          </p>
             <p>
               &nbsp; &nbsp;每隔<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟加收<input type="text" class="easyui-numberbox" style="width:50px;"/>元，
               不足<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟，超过<input type="text" class="easyui-numberbox" style="width:50px;"/>分钟，加收<input type="text" class="easyui-numberbox" style="width:50px;"/>元
            </p>
            <p>
                天房在<input class="easyui-timespinner" style="width:80px;"/>之前住客人视同前一天入住
             </p>
        </div>
        <div title="单据参数" style="padding:10px">
           
        </div>
        <div title="接口参数" style="padding:10px">
            This is the help content.
        </div>
    </div>
    </form>
</body>
</html>
