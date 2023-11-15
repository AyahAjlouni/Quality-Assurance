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

    var data = {"OkPercent": 100.0, "KoPercent": 0.0};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.9717261904761905, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "https://localhost:44318/api/Billercategory/GetAllBillercategory"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/scripts.js"], "isController": false}, {"data": [0.0, 500, 1500, "Register process"], "isController": true}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/animate.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/js/jquery.min.js"], "isController": false}, {"data": [0.9444444444444444, 500, 1500, "https://localhost:44318/api/User/GetUserprofile/689"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/js/roberto.bundle.js"], "isController": false}, {"data": [1.0, 500, 1500, "https://localhost:44318/api/Login/Loginuser"], "isController": false}, {"data": [1.0, 500, 1500, "https://localhost:44318/api/Testimonial"], "isController": false}, {"data": [0.5, 500, 1500, "update fullname process"], "isController": true}, {"data": [1.0, 500, 1500, "https://localhost:44318/api/User/UpdateUserProfile"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/bootstrap-datepicker.min.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/user/profile"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/style.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/js/default-assets/active.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/nice-select.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/fonts/ElegantIcons.woff"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/js/bootstrap.min.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/font-awesome.min.css"], "isController": false}, {"data": [1.0, 500, 1500, "Login Process"], "isController": true}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/img/core-img/logo22.jpeg"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/main.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/styles.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/fonts/fontawesome-webfont.woff2?v=4.7.0"], "isController": false}, {"data": [1.0, 500, 1500, "https://localhost:44318/api/User/Regester"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/runtime.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/style.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/vendor.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/default-assets/classy-nav.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/img/core-img/favicon.png"], "isController": false}, {"data": [0.9523809523809523, 500, 1500, "https://localhost:44318/api/SiteSetting/GetAllSitesetting"], "isController": false}, {"data": [0.9444444444444444, 500, 1500, "https://localhost:44318/api/User/GetUserprofile/691"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/styles.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/magnific-popup.css"], "isController": false}, {"data": [1.0, 500, 1500, "https://localhost:44318/api/User/GetUserprofile/690"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/favicon.ico"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/bootstrap.min.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/jquery-ui.min.css"], "isController": false}, {"data": [0.0, 500, 1500, "http://localhost:4200/auth/reqester"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/css/owl.carousel.min.css"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/assets/assestR/js/popper.min.js"], "isController": false}, {"data": [1.0, 500, 1500, "http://localhost:4200/polyfills.js"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 327, 0, 0.0, 42.65749235474008, 1, 2188, 2.0, 50.0, 90.99999999999966, 1792.359999999973, 57.428872497365646, 4267.203038560327, 38.42963810590095], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["https://localhost:44318/api/Billercategory/GetAllBillercategory", 3, 0, 0.0, 47.0, 45, 48, 48.0, 48.0, 48.0, 48.0, 9.433962264150942, 10.29075766509434, 6.246314858490566], "isController": false}, {"data": ["http://localhost:4200/scripts.js", 9, 0, 0.0, 5.555555555555556, 2, 12, 5.0, 12.0, 12.0, 12.0, 2.7514521553041886, 633.4179627789667, 1.7599620719963314], "isController": false}, {"data": ["Register process", 3, 0, 0.0, 2889.6666666666665, 2285, 3633, 2751.0, 3633.0, 3633.0, 3633.0, 0.704390702042733, 5689.050943590044, 15.637611161657665], "isController": true}, {"data": ["http://localhost:4200/assets/assestR/css/animate.css", 9, 0, 0.0, 12.444444444444446, 1, 98, 2.0, 98.0, 98.0, 98.0, 2.7607361963190185, 51.987574290644176, 1.8584643404907977], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/js/jquery.min.js", 9, 0, 0.0, 1.7777777777777777, 1, 2, 2.0, 2.0, 2.0, 2.0, 2.664298401420959, 74.88916102353463, 1.758853241563055], "isController": false}, {"data": ["https://localhost:44318/api/User/GetUserprofile/689", 9, 0, 0.0, 99.55555555555556, 22, 556, 35.0, 556.0, 556.0, 556.0, 6.721433905899925, 2.2317261015683347, 4.37739217699776], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/js/roberto.bundle.js", 9, 0, 0.0, 3.7777777777777777, 1, 9, 3.0, 9.0, 9.0, 9.0, 2.6698309107089884, 195.18793106644912, 1.7729345891426875], "isController": false}, {"data": ["https://localhost:44318/api/Login/Loginuser", 3, 0, 0.0, 192.0, 46, 276, 254.0, 276.0, 276.0, 276.0, 5.474452554744526, 2.1277657390510947, 3.7529938412408756], "isController": false}, {"data": ["https://localhost:44318/api/Testimonial", 9, 0, 0.0, 36.666666666666664, 3, 82, 39.0, 82.0, 82.0, 82.0, 5.617977528089887, 0.7680828651685393, 3.972085674157303], "isController": false}, {"data": ["update fullname process", 3, 0, 0.0, 1343.6666666666667, 1294, 1392, 1345.0, 1392.0, 1392.0, 1392.0, 1.9011406844106464, 37.00231206432193, 86.02290280418251], "isController": true}, {"data": ["https://localhost:44318/api/User/UpdateUserProfile", 6, 0, 0.0, 42.0, 4, 117, 34.5, 117.0, 117.0, 117.0, 4.2283298097251585, 0.5780919661733616, 3.646108615221987], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/bootstrap-datepicker.min.css", 9, 0, 0.0, 1.4444444444444444, 1, 2, 1.0, 2.0, 2.0, 2.0, 2.7556644213104713, 14.787476557715861, 1.9007984729026333], "isController": false}, {"data": ["http://localhost:4200/user/profile", 6, 0, 0.0, 2.166666666666667, 1, 4, 2.0, 4.0, 4.0, 4.0, 4.3604651162790695, 5.1823105922965125, 3.44493777252907], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/style.css", 9, 0, 0.0, 4.0, 1, 13, 2.0, 13.0, 13.0, 13.0, 2.6533018867924527, 63.71983941995873, 1.7723227446933962], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/js/default-assets/active.js", 9, 0, 0.0, 2.4444444444444446, 1, 9, 2.0, 9.0, 9.0, 9.0, 2.67538644470868, 9.086908256539834, 1.7931707974137931], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/nice-select.css", 9, 0, 0.0, 7.888888888888889, 1, 55, 2.0, 55.0, 55.0, 55.0, 2.7556644213104713, 4.267153532608695, 1.8640203995713411], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/fonts/ElegantIcons.woff", 18, 0, 0.0, 2.444444444444445, 1, 8, 2.0, 4.400000000000006, 8.0, 8.0, 8.298755186721992, 87.9392505186722, 5.633806621715076], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/js/bootstrap.min.js", 9, 0, 0.0, 2.333333333333333, 1, 7, 2.0, 7.0, 7.0, 7.0, 2.665876777251185, 49.06792663284953, 1.7659698052428912], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/font-awesome.min.css", 9, 0, 0.0, 1.4444444444444444, 1, 3, 1.0, 3.0, 3.0, 3.0, 2.7556644213104713, 28.449185261022656, 1.879269844611145], "isController": false}, {"data": ["Login Process", 3, 0, 0.0, 306.3333333333333, 155, 387, 377.0, 387.0, 387.0, 387.0, 4.559270516717325, 12.410358092705167, 15.365275930851062], "isController": true}, {"data": ["http://localhost:4200/assets/assestR/img/core-img/logo22.jpeg", 6, 0, 0.0, 2.0, 1, 4, 1.5, 4.0, 4.0, 4.0, 8.823529411764707, 152.38683363970588, 6.3376034007352935], "isController": false}, {"data": ["http://localhost:4200/main.js", 9, 0, 0.0, 15.444444444444448, 2, 98, 3.0, 98.0, 98.0, 98.0, 2.738892270237371, 750.2995663420572, 1.743904062690201], "isController": false}, {"data": ["http://localhost:4200/styles.js", 9, 0, 0.0, 2.1111111111111116, 1, 4, 2.0, 4.0, 4.0, 4.0, 2.7522935779816518, 160.94018778669724, 1.7578125], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/fonts/fontawesome-webfont.woff2?v=4.7.0", 6, 0, 0.0, 2.833333333333333, 1, 9, 1.0, 9.0, 9.0, 9.0, 10.309278350515465, 390.97233408505156, 6.946681701030928], "isController": false}, {"data": ["https://localhost:44318/api/User/Regester", 3, 0, 0.0, 69.0, 51, 83, 73.0, 83.0, 83.0, 83.0, 4.82315112540193, 1.5920166800643087, 2.901426848874598], "isController": false}, {"data": ["http://localhost:4200/runtime.js", 9, 0, 0.0, 1.6666666666666667, 1, 4, 1.0, 4.0, 4.0, 4.0, 2.676181980374665, 6.783668320695807, 1.710073316235504], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/style.css", 9, 0, 0.0, 1.4444444444444444, 1, 3, 1.0, 3.0, 3.0, 3.0, 2.7556644213104713, 23.314607413502756, 1.849667980710349], "isController": false}, {"data": ["http://localhost:4200/vendor.js", 9, 0, 0.0, 23.22222222222222, 8, 81, 11.0, 81.0, 81.0, 81.0, 2.674591381872214, 4458.8659686106985, 1.709927563150074], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/default-assets/classy-nav.css", 9, 0, 0.0, 2.111111111111111, 1, 6, 2.0, 6.0, 6.0, 6.0, 2.7607361963190185, 14.226957917944786, 1.906992906441718], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/img/core-img/favicon.png", 3, 0, 0.0, 1.0, 1, 1, 1.0, 1.0, 1.0, 1.0, 5.272407732864675, 8.418346331282953, 3.6505244947275926], "isController": false}, {"data": ["https://localhost:44318/api/SiteSetting/GetAllSitesetting", 21, 0, 0.0, 145.7142857142857, 2, 1135, 36.0, 669.8000000000002, 1094.9999999999995, 1135.0, 6.359781950333132, 4.292497917928528, 3.7938654603270745], "isController": false}, {"data": ["https://localhost:44318/api/User/GetUserprofile/691", 9, 0, 0.0, 92.88888888888889, 3, 561, 45.0, 561.0, 561.0, 561.0, 6.517016654598118, 2.202038830557567, 4.244263667632151], "isController": false}, {"data": ["http://localhost:4200/styles.css", 9, 0, 0.0, 3.222222222222222, 1, 10, 2.0, 10.0, 10.0, 10.0, 2.660360626662725, 106.4897673108188, 1.740665644398463], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/magnific-popup.css", 9, 0, 0.0, 1.8888888888888888, 1, 3, 2.0, 3.0, 3.0, 3.0, 2.7556644213104713, 6.910689681567667, 1.873887687538273], "isController": false}, {"data": ["https://localhost:44318/api/User/GetUserprofile/690", 9, 0, 0.0, 34.33333333333333, 3, 78, 35.0, 78.0, 78.0, 78.0, 5.692599620493358, 1.8790026091081595, 3.707354917773561], "isController": false}, {"data": ["http://localhost:4200/favicon.ico", 6, 0, 0.0, 2.166666666666667, 1, 5, 2.0, 5.0, 5.0, 5.0, 7.371007371007371, 5.2727176750614255, 5.089162315724816], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/bootstrap.min.css", 9, 0, 0.0, 2.7777777777777777, 1, 10, 2.0, 10.0, 10.0, 10.0, 2.7598896044158234, 138.2999367525299, 1.8758624655013798], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/jquery-ui.min.css", 9, 0, 0.0, 1.6666666666666667, 1, 3, 1.0, 3.0, 3.0, 3.0, 2.756508422664625, 15.867869448698315, 1.8717697166921898], "isController": false}, {"data": ["http://localhost:4200/auth/reqester", 3, 0, 0.0, 2095.3333333333335, 2048, 2188, 2050.0, 2188.0, 2188.0, 2188.0, 0.762001524003048, 1.6378567913385826, 0.5834074168148337], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/css/owl.carousel.min.css", 9, 0, 0.0, 2.2222222222222223, 1, 5, 2.0, 5.0, 5.0, 5.0, 2.7573529411764706, 3.3093620749080883, 1.878626206341912], "isController": false}, {"data": ["http://localhost:4200/assets/assestR/js/popper.min.js", 9, 0, 0.0, 2.0, 1, 5, 2.0, 5.0, 5.0, 5.0, 2.665876777251185, 18.552662636996445, 1.758159619372038], "isController": false}, {"data": ["http://localhost:4200/polyfills.js", 9, 0, 0.0, 3.111111111111111, 1, 7, 2.0, 7.0, 7.0, 7.0, 2.676977989292088, 268.33218694229623, 1.7175532607079118], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": []}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 327, 0, "", "", "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
