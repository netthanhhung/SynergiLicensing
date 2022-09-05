var map;
var menu;
var geocoder = new google.maps.Geocoder();
var directionsService = new google.maps.DirectionsService();
var directionsDisplay = new google.maps.DirectionsRenderer();

var _defaultStartEndLocation = "164 Hindley St Adelaide, SA 5000 Australia";
var _companyArray = new CompanyList();
var _companyArrayNeedToBeUpdated = new CompanyList();
var _infoWindow = new google.maps.InfoWindow({disableAutoPan:true});
var _itinerary = new Array();
var _startLocation = "";
var _endlocation = "";
var _currentSelectedCompany;

var latLongResult;
var q, curProgress, totProgress;

function hideMap() {
    debugger
}

function initialize() {
    var $map = $('#map_canvas');
    menu = new Menu($map);
    var center = [-27.293689, 136.054688];

    setMapSizeToFullPage();
    $map.gmap3(
          { action: 'init',
              options: {
                  center: center,
                  zoom: 4
              },
              callback: function (returnMap) {
                  map = returnMap;
                  directionsDisplay.setMap(map);
                  $(window).resize(function () {
                      setMapSizeToFullPage();
                  });
                  CreateMenu();
              }
          });

    //    var latlng = new google.maps.LatLng(-27.293689, 136.054688);
    //    var myOptions = {
    //        zoom: 4,
    //        center: latlng,
    //        mapTypeId: google.maps.MapTypeId.ROADMAP
    //    }


    //    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
}

function setMapSizeToFullPage() {
    $('div[id="map_canvas"]').width($(window).width() - 15);
    $('div[id="map_canvas"]').height($(window).height() - 35);
}

function GetLatLng(companyAddress) {
    geocoder.geocode({ "address": companyAddress }, function (result, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            return result[0].geometry.location;
        }
        else if (status == google.maps.GeocoderStatus.OVER_QUERY_LIMIT) {
            return GetLatLng(companyAddress);
        }
        else {
            alert("Some error happen that cannot get acquire company position");
            return null;
        }
    });
}

function CreateMarker(company, latlng) {
    var marker = new google.maps.Marker(
        {
            map: map
            , position: latlng
            , title: company.companyName
            , icon: GetMarkerIcon(company)
        });
    google.maps.event.addListener(marker, 'mouseover', function () {
        company.GetInfoWindow().open(map, marker);

    });
    google.maps.event.addListener(marker, 'mouseout', function () {
        company.GetInfoWindow().close();

    });

    google.maps.event.addListener(marker, 'rightclick', function (event) {
        _currentSelectedCompany = company;
        menu.open(event);
    });

    google.maps.event.addListener(marker, 'click', function (event) {
        for (var i = 0; i < _itinerary.length; i++) {
            if (_itinerary[i] == company.companyId) {
                _itinerary.splice(i, 1);
                company.GetMarker().setIcon(GetMarkerIcon(company));
                return;
            }
        }
        addItinerary(company.companyId);
    });
    return marker;
}

function GetMarkerIcon(company) {

    switch (company.activityStatus) {
        case 0:
            return "../Images/GoogleMapMarker/WhiteMarker.png";
            break;
        case 1:
            return "../Images/GoogleMapMarker/GreenMarker.png";
            break;
        case 2:
            return "../Images/GoogleMapMarker/YellowMarker.png";
            break;
        case 3:
            return "../Images/GoogleMapMarker/RedMarker.png";
            break;
    }
}

function initializeCompany(companyProfilesInJson) {
    var companyProfileArray = jQuery.parseJSON(companyProfilesInJson);
    hideAllMarker();
    _companyArray = new CompanyList();
    for (var i = 0; i < companyProfileArray.length; i++) {
        var cp = new Company(companyProfileArray[i]);
        _companyArray.Add(cp);
        if (cp.lat && cp.lat != "") {
            cp.marker = cp.GetMarker();
        }
    }

    initializeCompanyDontHaveCache();
}

function initializeCompanyDontHaveCache() {
    var todo = new Array();
    for (var i = 0; i < _companyArray.getLength(); i++) {
        var curObj = _companyArray.GetItemByIndex(i);
        if (curObj.lat == null || curObj.lat == "") {
            todo.push(start_GeoCode(curObj));
        }
    }
    if (todo.length > 0) {
        q = queue(), todo;
        totProgress = todo.length;
        curProgress = 0;
        $("#progressbar").progressbar({
            value: 0
        });
        q.start(todo);
    }

}

function ShowingCompanyOnList(cpJson) {
    var companyProfileArray = jQuery.parseJSON(cpJson);
    hideAllMarker();
    for (var i = 0; i < companyProfileArray.length; i++) {
        var cp = _companyArray.GetItemByCompanyId(companyProfileArray[i].CompanyId);
        if (cp.marker) {
            cp.marker.setMap(map);
        }
    }
}

function company_selected(cpjSon) {
    var companyProfile = jQuery.parseJSON(cpjSon);
    var cp = _companyArray.GetItemByCompanyId(companyProfile.CompanyId);
    removeBouncing();
    if (cp) {
        setbouncing(cp.GetMarker());
    }
    else {
        cp = new Company(companyProfile);
        cp.marker = cp.GetMarker();
        setbouncing(cp.GetMarker());
    }
}


function setbouncing(marker) {
    if (marker) {
        if (marker.getMap() == null) {
            marker.setMap(map);
        }
        marker.setAnimation(google.maps.Animation.BOUNCE);
        map.setCenter(marker.getPosition());
        map.setZoom(14);
    }
}

function removeAllInfoWindow() {
}

function removeBouncing() {
    if (_companyArray == null || _companyArray.items.length <= 0) {
        return;
    }

    for (var i = 0; i < _companyArray.items.length; i++) {
        var cp = _companyArray.GetItemByIndex(i);
        if (cp.GetMarker() != null && cp.GetMarker().getAnimation() != null) {
            cp.GetMarker().setAnimation(null);
        }
    }
}

function hideAllMarker() {
    if (_companyArray == null || _companyArray.items.length <= 0) {
        return;
    }
    for (var i = 0; i < _companyArray.items.length; i++) {
        var marker = _companyArray.GetItemByIndex(i).marker;
        if (marker) {
            marker.setMap(null);
        }
    }
}

function checkItinerary() {
    var jsonStr = $.toJSON(_itinerary);
    return jsonStr;
}

function setStartLocation(companyId) {
    _startLocation = companyId;
}

function getStartLocation() {
    return _startLocation + "";
}

function addItinerary(companyId) {
    if (_itinerary == null && typeof (_itinerary) != "Array") {
        _itinerary = new Array();
    }

    _itinerary.push(companyId);
    _companyArray.GetItemByCompanyId(companyId).GetMarker().setIcon("../Images/GoogleMapMarker/GrayMarker.png");
}

function removeItiCompanies() {
    while (_itinerary.length > 0) {
        var iti = _itinerary.pop();
        var company = _companyArray.GetItemByCompanyId(iti);
        company.GetMarker().setIcon(GetMarkerIcon(company));
    }
    //_itinerary = new Array();
}

function setEndLocation(companyId) {
    _endlocation = companyId;
}

function getEndLocation() {
    return _endlocation + "";
}

var routeResult = "";
function requestDirection(sl, el) {
    if (_itinerary.length < 2) {
        return;
    }
    routeResult = "";
    var originCom = sl; //_startLocation == "" ? _defaultStartEndLocation : _companyArray.GetItemByCompanyId(parseInt(_startLocation)).companyAddress;
    var destCom = el; //_endlocation == "" ? _defaultStartEndLocation : _companyArray.GetItemByCompanyId(parseInt(_endlocation)).companyAddress;

    var request = {
        origin: originCom,
        destination: destCom,
        waypoints: createWaypoints(),
        optimizeWaypoints: true,
        travelMode: google.maps.TravelMode.DRIVING
    };
    directionsService.route(request, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            routeResult = result;
            directionsDisplay.setDirections(result);
        }
    });
}

function createWaypoints() {
    var waypoints = new Array();
    if (_itinerary.length > 0) {
        for (var i = 0; i < _itinerary.length; i++) {
            var curComp = _companyArray.GetItemByCompanyId(_itinerary[i]);
            var curCompLatLng = new google.maps.LatLng(curComp.lat, curComp.lng);
            waypoints.push({ location: curCompLatLng, stopover: true });
        }
    }
    return waypoints;
}

function getRoute() {
    if (routeResult != "");
    {
        return $.toJSON(routeResult);
    }
    return "";
}

function generateInfo(company) {
    var contentString = ''
        + '<div><table style="width:300px; font-size:smaller;font-family:Times New Roman;color:#808080"><tr><td colspan="3" style="font-weight: bold;font-size:medium;color:#666666">'
        + company.companyName
        + '</td></tr><tr><td colspan="3" style="font-style:italic;border-bottom-style:dotted;border-bottom-width:medium">'
        + company.companyAddress
        + '</td></tr><tr><td style="width:80px">Telephone:</td><td colspan="2">'
        + company.telephone
        + '</td></tr><tr><td>Fax:</td><td colspan="2">'
        + company.fax
        + '</td></tr><tr><td>Classification:</td><td colspan="2">'
        + company.classification
        + '</td></tr><tr><td>No. of activity</td><td colspan="2">'
        + company.numberOfActivity
        + '</td></tr><tr><td colspan="2">RmNt last year: <strong>'
        + company.rmNtLast
        + '</strong></td>'
        + '<td>RmNt YTD:<strong>'
        + company.rmNtYTD
        + '</strong></td></tr><tr><td colspan="2">Revenue last year: <strong>$'
        +  + number_format(company.revenueLast, 2, '.', ',')
        + '</strong></td>'
        + '<td>Revenue YTD:<strong>$'
        + number_format(company.revenueYTD, 2, '.',',');
        +'</strong></td></tr></table></div>';
        
    return contentString;
}

function ShowNearestCompany(company, radius) {
    //var company = _companyArray.GetItemByIndex(0);
    var center = new google.maps.LatLng(company.lat, company.lng);
    var count = _companyArray.getLength();
    hideAllMarker();
    for (var i = 0; i < count; i++) {
        var companyToCompare = _companyArray.GetItemByIndex(i);
        //        if (companyToCompare.companyId == companny.companyId) {
        //            continue;
        //        }
        if (companyToCompare.lat != null && companyToCompare.lat != ""
            && companyToCompare.lng != null && companyToCompare.lng != "") {
            var cp = new google.maps.LatLng(companyToCompare.lat, companyToCompare.lng);
            if (google.maps.geometry.spherical.computeDistanceBetween(center, cp) < radius) {
                companyToCompare.GetMarker().setMap(map);
            }
        }
    }

}

var CreateMenu = function () {
    // MENU : ITEM 3
    menu.add('Set Start Location', 'startLocation',
          function () {
              if (_currentSelectedCompany) {
                  setStartLocation(_currentSelectedCompany.companyId);
              }
              _currentSelectedCompany = null;
              menu.close();
          });

    // MENU : ITEM 4
    menu.add('Set Finish Location', 'stopLocation',
          function () {
              if (_currentSelectedCompany) {
                  setEndLocation(_currentSelectedCompany.companyId);
              }
              _currentSelectedCompany = null;
              menu.close();
          });

    // MENU : ITEM 5
    menu.add('Companies in 5Km', 'Find5Km',
          function () {
              if (_currentSelectedCompany) {
                  ShowNearestCompany(_currentSelectedCompany, 5000);
              }
              _currentSelectedCompany = null;
              menu.close();
          });

    menu.add('Companies in 10Km', 'Find10Km',
          function () {
              if (_currentSelectedCompany) {
                  ShowNearestCompany(_currentSelectedCompany, 10000);
              }
              _currentSelectedCompany = null;
              menu.close();
          });

    menu.add('Companies in 20Km', 'Find20Km',
          function () {
              if (_currentSelectedCompany) {
                  ShowNearestCompany(_currentSelectedCompany, 20000);
              }
              _currentSelectedCompany = null;
              menu.close();
          });
}

/// *****************************************************************
/// Company object & CompanyList object
/// *****************************************************************
function Company(cpObject) {
    this.companyId = cpObject.CompanyId;
    this.companyName = cpObject.CompanyName;
    this.companyAddress = cpObject.CompanyAddress;
    this.telephone = cpObject.Telephone;
    this.fax = cpObject.Fax;
    this.classification = cpObject.Classification;
    this.numberOfActivity = cpObject.NumberOfActivity;
    this.rmNtLast = cpObject.RmNtLast;
    this.revenueLast = cpObject.TotalRevenueLast;
    this.rmNtYTD = cpObject.RmNtYTD;
    this.revenueYTD = cpObject.TotalRevenueYTD
    this.salesPersonId = cpObject.SalesPersonId;
    this.salesPerson = cpObject.SalesPerson;
    this.lat = cpObject.Latitude;
    this.lng = cpObject.Longitude;
    this.nextActivityDate = cpObject.NextActivityDate;
    this.clearedDate = cpObject.ClearedDate;
    this.lastActivityDate = cpObject.LastActivityDate;
    this.activityStatus = cpObject.ActivityStatus
    this.color = cpObject.Color;
    this.marker = null;
    this.infoWindow = null;
    var me = this;

    this.GetMarker = function () {
        if (!me.marker) {
            if (me.lat == null || me.lng == null) {
                var result = GetLatLng(me.companyAddress);
                if (result != null) {
                    me.lat = result.lat();
                    me.lng = result.lng();
                    me.marker = CreateMarker(me, result);
                }
            }
            else {
                var myLatlng = new google.maps.LatLng(me.lat, me.lng);
                me.lat = myLatlng.lat();
                me.lng = myLatlng.lng();
                me.marker = CreateMarker(me, myLatlng);
            }
        }
        return me.marker;
    }

    this.GetInfoWindow = function () {
        _infoWindow.setContent(generateInfo(me));
        return _infoWindow;
    }
}

function CompanyList() {
    this.items = new Array();
    this.indexedItem = new Array();

    this.GetItemByCompanyId = function (companyId) {
        return this.indexedItem[companyId];
    }

    this.GetItemByIndex = function (index) {
        return this.indexedItem[this.items[index]];
    }

    this.Add = function (company) {
        this.items.push(company.companyId);
        this.indexedItem[company.companyId] = company;
    }

    this.getLength = function () {
        return this.items.length;
    }
}

/// *****************************************************************
/// TASK QUEUE 
/// *****************************************************************
var queue = function () {
    var tasks;

    return {
        start: function (list) {
            tasks = list;
            first = tasks.pop();
            first();
        },

        next: function () {
            if (tasks.length > 0) {
                tasks.pop()();
            }
        }
    }
}

// example implementation
start_GeoCode = function (company) {
    return function () {
        geocoder.geocode({ "address": company.companyAddress }, function (result, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var result = result[0].geometry.location;
                company.lat = result.lat();
                company.lng = result.lng();
                company.marker = company.GetMarker();
            }
            else if (status == google.maps.GeocoderStatus.OVER_QUERY_LIMIT) {
            }
            else if (status == google.maps.GeocoderStatus.ZERO_RESULTS) {
            }
            else {
            }
            setTimeout("done_GeoCode()", 800);
        });
    }
}

done_GeoCode = function () {
    curProgress++;
    var p = curProgress / totProgress * 100;
    $("#progressbar").progressbar({
        value: p
    });
    q.next();
}

// ==============================================================================
// NUMBER FORMAT
// ==============================================================================
function number_format(number, decimals, dec_point, thousands_sep) {
    // http://kevin.vanzonneveld.net
    // +   original by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
    // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // +     bugfix by: Michael White (http://getsprink.com)
    // +     bugfix by: Benjamin Lupton
    // +     bugfix by: Allan Jensen (http://www.winternet.no)
    // +    revised by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
    // +     bugfix by: Howard Yeend
    // +    revised by: Luke Smith (http://lucassmith.name)
    // +     bugfix by: Diogo Resende
    // +     bugfix by: Rival
    // +      input by: Kheang Hok Chin (http://www.distantia.ca/)
    // +   improved by: davook
    // +   improved by: Brett Zamir (http://brett-zamir.me)
    // +      input by: Jay Klehr
    // +   improved by: Brett Zamir (http://brett-zamir.me)
    // +      input by: Amir Habibi (http://www.residence-mixte.com/)
    // +     bugfix by: Brett Zamir (http://brett-zamir.me)
    // +   improved by: Theriault
    // +      input by: Amirouche
    // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // *     example 1: number_format(1234.56);
    // *     returns 1: '1,235'
    // *     example 2: number_format(1234.56, 2, ',', ' ');
    // *     returns 2: '1 234,56'
    // *     example 3: number_format(1234.5678, 2, '.', '');
    // *     returns 3: '1234.57'
    // *     example 4: number_format(67, 2, ',', '.');
    // *     returns 4: '67,00'
    // *     example 5: number_format(1000);
    // *     returns 5: '1,000'
    // *     example 6: number_format(67.311, 2);
    // *     returns 6: '67.31'
    // *     example 7: number_format(1000.55, 1);
    // *     returns 7: '1,000.6'
    // *     example 8: number_format(67000, 5, ',', '.');
    // *     returns 8: '67.000,00000'
    // *     example 9: number_format(0.9, 0);
    // *     returns 9: '1'
    // *    example 10: number_format('1.20', 2);
    // *    returns 10: '1.20'
    // *    example 11: number_format('1.20', 4);
    // *    returns 11: '1.2000'
    // *    example 12: number_format('1.2000', 3);
    // *    returns 12: '1.200'
    // *    example 13: number_format('1 000,50', 2, '.', ' ');
    // *    returns 13: '100 050.00'
    // Strip all characters but numerical ones.
    number = (number + '').replace(/[^0-9+\-Ee.]/g, '');
    var n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
        s = '',
        toFixedFix = function (n, prec) {
            var k = Math.pow(10, prec);
            return '' + Math.round(n * k) / k;
        };
    // Fix for IE parseFloat(0.55).toFixed(0) = 0;
    s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
    if (s[0].length > 3) {
        s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
    }
    if ((s[1] || '').length < prec) {
        s[1] = s[1] || '';
        s[1] += new Array(prec - s[1].length + 1).join('0');
    }
    return s.join(dec);
}


//********************************************************************************
//*                               MENU                                           *
//********************************************************************************
function Menu($div) {
    var that = this,
            ts = null;

    this.$div = $div;
    this.items = [];

    // create an item using a new closure 
    this.create = function (item) {
        var $item = $('<div class="item ' + item.cl + '">' + item.label + '</div>');
        $item
        // bind click on item
            .click(function () {
                if (typeof (item.fnc) === 'function') {
                    item.fnc.apply($(this), []);
                }
            })
        // manage mouse over coloration
            .hover(
              function () { $(this).addClass('hover'); },
              function () { $(this).removeClass('hover'); }
            );
        return $item;
    };
    this.clearTs = function () {
        if (ts) {
            clearTimeout(ts);
            ts = null;
        }
    };
    this.initTs = function (t) {
        ts = setTimeout(function () { that.close() }, t);
    };
}

// add item
Menu.prototype.add = function (label, cl, fnc) {
    this.items.push({
        label: label,
        fnc: fnc,
        cl: cl
    });
}

// close previous and open a new menu 
Menu.prototype.open = function (event) {
    this.close();
    var k,
            that = this,
            offset = {
                x: 0,
                y: 0
            },
            $menu = $('<div id="menu"></div>');

    // add items in menu
    for (k in this.items) {
        $menu.append(this.create(this.items[k]));
    }

    // manage auto-close menu on mouse hover / out
    $menu.hover(
          function () { that.clearTs(); },
          function () { that.initTs(3000); }
        );
    //    debugger
    //    // change the offset to get the menu visible (#menu width & height must be defined in CSS to use this simple code)
    //    if (event.pixel.y + $menu.height() > this.$div.height()) {
    //        offset.y = -$menu.height();
    //    }
    //    if (event.pixel.x + $menu.width() > this.$div.width()) {
    //        offset.x = -$menu.width();
    //    }



    // use menu as overlay
    this.$div.gmap3({
        action: 'addOverlay',
        latLng: event.latLng,
        content: $menu,
        offset: offset
    });

    // start auto-close
    this.initTs(5000);
}

// close the menu
Menu.prototype.close = function () {
    this.clearTs();
    this.$div.gmap3({ action: 'clear', name: 'overlay' });
}

