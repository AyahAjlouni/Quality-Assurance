/*
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
var showControllersOnly = false;
var seriesFilter = "";
var filtersOnlySampleSeries = true;

/*
 * Add header in statistics table to group metrics by category
 * format
 *
 */
function summaryTableHeader(header) {
    var newRow = header.insertRow(-1);
    newRow.className = "tablesorter-no-sort";
    var cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Requests";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 3;
    cell.innerHTML = "Executions";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 7;
    cell.innerHTML = "Response Times (ms)";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Throughput";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 2;
    cell.innerHTML = "Network (KB/sec)";
    newRow.appendChild(cell);
}

/*
 * Populates the table identified by id parameter with the specified data and
 * format
 *
 */
function createTable(table, info, formatter, defaultSorts, seriesIndex, headerCreator) {
    var tableRef = table[0];

    // Create header and populate it with data.titles array
    var header = tableRef.createTHead();

    // Call callback is available
    if(headerCreator) {
        headerCreator(header);
    }

    var newRow = header.insertRow(-1);
    for (var index = 0; index < info.titles.length; index++) {
        var cell = document.createElement('th');
        cell.innerHTML = info.titles[index];
        newRow.appendChild(cell);
    }

    var tBody;

    // Create overall body if defined
    if(info.overall){
        tBody = document.createElement('tbody');
        tBody.className = "tablesorter-no-sort";
        tableRef.appendChild(tBody);
        var newRow = tBody.insertRow(-1);
        var data = info.overall.data;
        for(var index=0;index < data.length; index++){
            var cell = newRow.insertCell(-1);
            cell.innerHTML = formatter ? formatter(index, data[index]): data[index];
        }
    }

    // Create regular body
    tBody = document.createElement('tbody');
    tableRef.appendChild(tBody);

    var regexp;
    if(seriesFilter) {
        regexp = new RegExp(seriesFilter, 'i');
    }
    // Populate body with data.items array
    for(var index=0; index < info.items.length; index++){
        var item = info.items[index];
        if((!regexp || filtersOnlySampleSeries && !info.supportsControllersDiscrimination || regexp.test(item.data[seriesIndex]))
                &&
                (!showControllersOnly || !info.supportsControllersDiscrimination || item.isController)){
            if(item.data.length > 0) {
                var newRow = tBody.insertRow(-1);
                for(var col=0; col < item.data.length; col++){
                    var cell = newRow.insertCell(-1);
                    cell.innerHTML = formatter ? formatter(col, item.data[col]) : item.data[col];
                }
            }
        }
    }

    // Add support of columns sort
    table.tablesorter({sortList : defaultSorts});
}

$(document).ready(function() {

    // Customize table sorter default options
    $.extend( $.tablesorter.defaults, {
        theme: 'blue',
        cssInfoBlock: "tablesorter-no-sort",
        widthFixed: true,
        widgets: ['zebra']
    });

    var data = {"OkPercent": 98.97435897435898, "KoPercent": 1.0256410256410255};
    var dataset = [
        {
            "label" : "FAIL",
            "data" : data.KoPercent,
            "color" : "#FF6347"
        },
        {
            "label" : "PASS",
            "data" : data.OkPercent,
            "color" : "#9ACD32"
        }];
    $.plot($("#flot-requests-summary"), dataset, {
        series : {
            pie : {
                show : true,
                radius : 1,
                label : {
                    show : true,
                    radius : 3 / 4,
                    formatter : function(label, series) {
                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'
                            + label
                            + '<br/>'
                            + Math.round10(series.percent, -2)
                            + '%</div>';
                    },
                    background : {
                        opacity : 0.5,
                        color : '#000'
                    }
                }
            }
        },
        legend : {
            show : true
        }
    });

    // Creates APDEX table
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.9485294117647058, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/Lumia_1520.jpg"], "isController": false}, {"data": [0.95, 500, 1500, "https://www.demoblaze.com/css/fonts/Lato-Regular.woff2"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/prod.html?idp_=1"], "isController": false}, {"data": [1.0, 500, 1500, "https://hls.demoblaze.com/about_demo_hls_600k00000.ts"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/tether/dist/js/tether.min.js"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/front.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/iphone1.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/Nexus_6.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/xperia_z5.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/video.js/dist/video-js.min.css"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/galaxy_s6.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://hls.demoblaze.com/index.m3u8"], "isController": false}, {"data": [1.0, 500, 1500, "https://hls.demoblaze.com/about_demo_hls_600k.m3u8"], "isController": false}, {"data": [0.0, 500, 1500, "Start Request"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/sony_vaio_5.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/bootstrap/dist/js/bootstrap.min.js"], "isController": false}, {"data": [0.75, 500, 1500, "Login -  https://api.demoblaze.com/login"], "isController": false}, {"data": [1.0, 500, 1500, "https://api.demoblaze.com/check"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/jquery/dist/jquery.min.js"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/bm.png"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/nexus1.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/iphone_6.jpg"], "isController": false}, {"data": [0.0, 500, 1500, "Homepage Test"], "isController": true}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/js/prod.js"], "isController": false}, {"data": [0.0, 500, 1500, "LoginToAddtocart Test"], "isController": true}, {"data": [1.0, 500, 1500, "https://api.demoblaze.com/entries"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/config.json"], "isController": false}, {"data": [0.9166666666666666, 500, 1500, "https://www.demoblaze.com/index.html"], "isController": false}, {"data": [1.0, 500, 1500, "https://api.demoblaze.com/view"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/imgs/HTC_M9.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/bootstrap/dist/css/bootstrap.min.css"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/favicon.ico"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/videojs-contrib-hls/dist/videojs-contrib-hls.min.js"], "isController": false}, {"data": [1.0, 500, 1500, "https://api.demoblaze.com/addtocart"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/js/index.js"], "isController": false}, {"data": [0.3333333333333333, 500, 1500, "addtocart"], "isController": true}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/node_modules/video.js/dist/video.min.js"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/Samsung1.jpg"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/css/latofonts.css"], "isController": false}, {"data": [1.0, 500, 1500, "https://www.demoblaze.com/css/latostyle.css"], "isController": false}]}, function(index, item){
        switch(index){
            case 0:
                item = item.toFixed(3);
                break;
            case 1:
            case 2:
                item = formatDuration(item);
                break;
        }
        return item;
    }, [[0, 0]], 3);

    // Create statistics table
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 195, 2, 1.0256410256410255, 193.65128205128195, 61, 1290, 190.0, 253.40000000000003, 330.4, 689.999999999995, 7.723078141708583, 499.66741411788587, 1.5424883782526042], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["https://www.demoblaze.com/imgs/Lumia_1520.jpg", 3, 0, 0.0, 212.66666666666666, 208, 221, 209.0, 221.0, 221.0, 221.0, 0.2780094523213789, 39.50648254332314, 0.05837112524325827], "isController": false}, {"data": ["https://www.demoblaze.com/css/fonts/Lato-Regular.woff2", 10, 0, 0.0, 244.0, 105, 526, 215.0, 502.70000000000005, 526.0, 526.0, 0.4931939238508582, 88.14348321291182, 0.0785064546754784], "isController": false}, {"data": ["https://www.demoblaze.com/prod.html?idp_=1", 4, 0, 0.0, 162.25, 83, 190, 188.0, 190.0, 190.0, 190.0, 0.639386189258312, 11.213141634031329, 0.2737996523337596], "isController": false}, {"data": ["https://hls.demoblaze.com/about_demo_hls_600k00000.ts", 9, 0, 0.0, 197.44444444444446, 91, 422, 123.0, 422.0, 422.0, 422.0, 0.5559673832468495, 294.79301797627875, 0.08795577742772424], "isController": false}, {"data": ["https://www.demoblaze.com/node_modules/tether/dist/js/tether.min.js", 5, 0, 0.0, 168.4, 79, 200, 188.0, 200.0, 200.0, 200.0, 0.23993473775133164, 2.170893891861414, 0.04123878305101013], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/front.jpg", 5, 0, 0.0, 166.8, 81, 190, 188.0, 190.0, 190.0, 190.0, 0.24254183846713556, 5.909351882731022, 0.049740025466893045], "isController": false}, {"data": ["https://www.demoblaze.com/iphone1.jpg", 3, 0, 0.0, 192.33333333333334, 191, 194, 192.0, 194.0, 194.0, 194.0, 0.24789291026276647, 8.448774608535778, 0.050111164476945955], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/Nexus_6.jpg", 3, 0, 0.0, 190.33333333333334, 113, 236, 222.0, 236.0, 236.0, 236.0, 0.28079371022089106, 63.64392359486148, 0.05813307281916885], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/xperia_z5.jpg", 3, 0, 0.0, 212.0, 204, 226, 206.0, 226.0, 226.0, 226.0, 0.28174305033809166, 39.611311983471076, 0.058879895285499625], "isController": false}, {"data": ["https://www.demoblaze.com/node_modules/video.js/dist/video-js.min.css", 5, 0, 0.0, 194.8, 190, 213, 190.0, 213.0, 213.0, 213.0, 0.23701175578308684, 4.149140758319112, 0.0446711610020857], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/galaxy_s6.jpg", 9, 0, 0.0, 209.33333333333331, 93, 332, 203.0, 332.0, 332.0, 332.0, 0.5912106680680549, 62.20009658493726, 0.1235537919595349], "isController": false}, {"data": ["https://hls.demoblaze.com/index.m3u8", 9, 0, 0.0, 123.55555555555556, 61, 409, 62.0, 409.0, 409.0, 409.0, 0.5720823798627003, 0.710633581235698, 0.08100775886727689], "isController": false}, {"data": ["https://hls.demoblaze.com/about_demo_hls_600k.m3u8", 9, 0, 0.0, 63.666666666666664, 62, 66, 64.0, 66.0, 66.0, 66.0, 0.5458184244041482, 3.238131765874219, 0.08475110300806597], "isController": false}, {"data": ["Start Request", 2, 2, 100.0, 186.0, 186, 186, 186.0, 186.0, 186.0, 186.0, 0.1895195678953852, 0.0903179190751445, 0.02350486828390031], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/sony_vaio_5.jpg", 3, 0, 0.0, 199.66666666666666, 195, 206, 198.0, 206.0, 206.0, 206.0, 0.28192839018889204, 24.991501767925946, 0.05946926980546941], "isController": false}, {"data": ["https://www.demoblaze.com/node_modules/bootstrap/dist/js/bootstrap.min.js", 5, 0, 0.0, 166.8, 82, 190, 188.0, 190.0, 190.0, 190.0, 0.24128945082520992, 3.651105256490686, 0.04288542973651192], "isController": false}, {"data": ["Login -  https://api.demoblaze.com/login", 2, 0, 0.0, 485.5, 306, 665, 485.5, 665.0, 665.0, 665.0, 0.23693875133278047, 0.056458061841014096, 0.055995290842317254], "isController": false}, {"data": ["https://api.demoblaze.com/check", 8, 0, 0.0, 218.0, 209, 234, 216.0, 234.0, 234.0, 234.0, 0.5033029254482542, 0.14253696130858762, 0.10862299465240642], "isController": false}, {"data": ["https://www.demoblaze.com/node_modules/jquery/dist/jquery.min.js", 5, 0, 0.0, 198.2, 191, 217, 193.0, 217.0, 217.0, 217.0, 0.2384813507583707, 8.413221480611465, 0.04029030632929505], "isController": false}, {"data": ["https://www.demoblaze.com/bm.png", 10, 0, 0.0, 153.60000000000002, 77, 187, 186.0, 187.0, 187.0, 187.0, 0.4710315591144607, 1.9609448598681112, 0.09291833490343852], "isController": false}, {"data": ["https://www.demoblaze.com/nexus1.jpg", 3, 0, 0.0, 192.33333333333334, 191, 194, 192.0, 194.0, 194.0, 194.0, 0.24785194976867153, 8.263345278420358, 0.049860841457369465], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/iphone_6.jpg", 3, 0, 0.0, 215.33333333333334, 213, 218, 215.0, 218.0, 218.0, 218.0, 0.28137310073157007, 55.29723331105796, 0.05852780317951604], "isController": false}, {"data": ["Homepage Test", 1, 0, 0.0, 8752.0, 8752, 8752, 8752.0, 8752.0, 8752.0, 8752.0, 0.11425959780621572, 272.1642419232747, 0.7515023351805301], "isController": true}, {"data": ["https://www.demoblaze.com/js/prod.js", 2, 0, 0.0, 186.0, 185, 187, 186.0, 187.0, 187.0, 187.0, 0.33041467041136624, 0.9083176730546836, 0.04678723360317198], "isController": false}, {"data": ["LoginToAddtocart Test", 2, 0, 0.0, 14319.0, 13274, 15364, 14319.0, 15364.0, 15364.0, 15364.0, 0.09340992947550325, 477.9397491359581, 1.500214550931764], "isController": true}, {"data": ["https://api.demoblaze.com/entries", 3, 0, 0.0, 314.3333333333333, 228, 484, 231.0, 484.0, 484.0, 484.0, 0.2589108483645465, 0.7410817349184431, 0.03590365280055234], "isController": false}, {"data": ["https://www.demoblaze.com/config.json", 5, 0, 0.0, 178.8, 79, 257, 186.0, 257.0, 257.0, 257.0, 0.25456952293671403, 0.1305663217249631, 0.05543848009266331], "isController": false}, {"data": ["https://www.demoblaze.com/index.html", 6, 0, 0.0, 359.33333333333337, 79, 1290, 188.5, 1290.0, 1290.0, 1290.0, 0.43645886375209136, 4.267763534771223, 0.18434419782498], "isController": false}, {"data": ["https://api.demoblaze.com/view", 6, 0, 0.0, 216.66666666666666, 204, 229, 217.0, 229.0, 229.0, 229.0, 0.5679666792881485, 0.27399955035971224, 0.11204030196895115], "isController": false}, {"data": ["https://www.demoblaze.com/imgs/HTC_M9.jpg", 3, 0, 0.0, 199.33333333333334, 199, 200, 199.0, 200.0, 200.0, 200.0, 0.28190189813944744, 26.768016761417027, 0.05808720752678068], "isController": false}, {"data": ["https://www.demoblaze.com/node_modules/bootstrap/dist/css/bootstrap.min.css", 5, 0, 0.0, 198.2, 190, 220, 194.0, 220.0, 220.0, 220.0, 0.23667518697339773, 6.596118970699611, 0.045994494343463026], "isController": false}, {"data": ["https://www.demoblaze.com/favicon.ico", 15, 0, 0.0, 160.0, 77, 420, 186.0, 284.4000000000001, 420.0, 420.0, 0.967305088024763, 5.548653430708712, 0.1955392121300058], "isController": false}, {"data": ["https://www.demoblaze.com/node_modules/videojs-contrib-hls/dist/videojs-contrib-hls.min.js", 5, 0, 0.0, 175.2, 90, 200, 195.0, 200.0, 200.0, 200.0, 0.23977365367093464, 17.10560231141802, 0.04659663777394139], "isController": false}, {"data": ["https://api.demoblaze.com/addtocart", 6, 0, 0.0, 259.8333333333333, 237, 315, 250.5, 315.0, 315.0, 315.0, 0.6287331027978622, 0.12136677407523841, 0.18051516818610502], "isController": false}, {"data": ["https://www.demoblaze.com/js/index.js", 3, 0, 0.0, 186.0, 185, 187, 186.0, 187.0, 187.0, 187.0, 0.2458210422812193, 1.282878564405113, 0.03504870329400196], "isController": false}, {"data": ["addtocart", 6, 0, 0.0, 1607.5, 1266, 2524, 1336.5, 2524.0, 2524.0, 2524.0, 0.5121201775349948, 335.4547200409696, 0.9057125405428474], "isController": true}, {"data": ["https://www.demoblaze.com/node_modules/video.js/dist/video.min.js", 5, 0, 0.0, 222.6, 102, 298, 209.0, 298.0, 298.0, 298.0, 0.2385610000477122, 37.35664906782289, 0.04053673242998235], "isController": false}, {"data": ["https://www.demoblaze.com/Samsung1.jpg", 3, 0, 0.0, 190.0, 188, 192, 190.0, 192.0, 192.0, 192.0, 0.24785194976867153, 6.6995059742647065, 0.050344927296761405], "isController": false}, {"data": ["https://www.demoblaze.com/css/latofonts.css", 5, 0, 0.0, 187.2, 187, 188, 187.0, 188.0, 188.0, 188.0, 0.23729296189075033, 0.3110762422286555, 0.038699145152104784], "isController": false}, {"data": ["https://www.demoblaze.com/css/latostyle.css", 5, 0, 0.0, 143.8, 78, 189, 186.0, 189.0, 189.0, 189.0, 0.23851547965462958, 0.22873261818442014, 0.03889852060773744], "isController": false}]}, function(index, item){
        switch(index){
            // Errors pct
            case 3:
                item = item.toFixed(2) + '%';
                break;
            // Mean
            case 4:
            // Mean
            case 7:
            // Median
            case 8:
            // Percentile 1
            case 9:
            // Percentile 2
            case 10:
            // Percentile 3
            case 11:
            // Throughput
            case 12:
            // Kbytes/s
            case 13:
            // Sent Kbytes/s
                item = item.toFixed(2);
                break;
        }
        return item;
    }, [[0, 0]], 0, summaryTableHeader);

    // Create error table
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["404/Not Found", 2, 100.0, 1.0256410256410255], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 195, 2, "404/Not Found", 2, "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Start Request", 2, 2, "404/Not Found", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
