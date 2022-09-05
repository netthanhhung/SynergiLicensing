var _chartTitle, _chartData;

function onPluginLoaded(sender) {
    alert("onPluginLoaded");
    
    // get reference to the silverlight control on the page
    var silverlightControl = sender.get_element();

    // call scriptable method in silverlight application
    // we call it here to ensure silverlight controls is fully loaded.
    if (_chartTitle != null) {
        silverlightControl.content.uiPickupGroup.ChartTitle = _chartTitle;
    }

    if (_chartData != null) {
        silverlightControl.content.uiPickupGroup.ChartData = _chartData;
    }
}

function SetChartTitleValue(title) {
    _chartTitle = title;
}

function SetChartDataValue(jsonData) {
    _chartData = jsonData;
}

function onSilverlightError(sender, args) {
    var appSource = "";
    if (sender != null && sender != 0) {
      appSource = sender.getHost().Source;
    }
    
    var errorType = args.ErrorType;
    var iErrorCode = args.ErrorCode;

    if (errorType == "ImageError" || errorType == "MediaError") {
      return;
    }

    var errMsg = "Unhandled Error in Silverlight Application " +  appSource + "\n" ;

    errMsg += "Code: "+ iErrorCode + "    \n";
    errMsg += "Category: " + errorType + "       \n";
    errMsg += "Message: " + args.ErrorMessage + "     \n";

    if (errorType == "ParserError") {
        errMsg += "File: " + args.xamlFile + "     \n";
        errMsg += "Line: " + args.lineNumber + "     \n";
        errMsg += "Position: " + args.charPosition + "     \n";
    }
    else if (errorType == "RuntimeError") {           
        if (args.lineNumber != 0) {
            errMsg += "Line: " + args.lineNumber + "     \n";
            errMsg += "Position: " +  args.charPosition + "     \n";
        }
        errMsg += "MethodName: " + args.methodName + "     \n";
    }

    throw new Error(errMsg);
}
