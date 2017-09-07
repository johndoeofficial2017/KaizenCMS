function KendoGraph() {

    var self = this;

    self.divId = '';
    self.dataURL = '';
    self.serviceURL = '';
    self.title = '';
    self.legendVisible = false;
    self.legendPosition = undefined;
    self.type = 'bar';
    self.categoryAxisMajorGridLinesVisible = false;
    self.chartAreaBackground = undefined;

    self.valueAxisLineVisible = false;
    self.valueAxisAxisCrossingValue = undefined;
    self.valueAxisMinorGridLinesVisible = true;
    self.valueAxisLabelsRotation = "auto";

    self.seriesDefaultsType = undefined;
    self.seriesDefaultsstyle = undefined;
    self.seriesDefaultsLable = {};

    self.labelsTemplate = "#= category # - #= kendo.format('{0:P}', percentage)#";
    self.labelsPosition = "outsideEnd";
    self.labelsVisible = false;
    self.labelBackground = "transparent";

    self.tooltipVisible = true;
    self.tooltipTemplate = undefined;

    self.categories = [];
    self.series = [];

    self.dataBound = function (e) { }

    var __construct = function () {

        self.divId = '';
        self.dataURL = '';
        self.serviceURL = '';
        self.title = '';
        self.legendVisible = false;
        self.legendPosition = undefined;
        self.type = 'bar';
        self.categoryAxisMajorGridLinesVisible = false;
        self.chartAreaBackground = undefined;

        self.valueAxisLineVisible = false;
        self.valueAxisAxisCrossingValue = undefined;
        self.valueAxisMinorGridLinesVisible = true;
        self.valueAxisLabelsRotation = "auto";

        self.seriesDefaultsType = undefined;
        self.seriesDefaultsstyle = undefined;
        self.seriesDefaultsLable = {};

        self.labelsTemplate = "#= category # - #= kendo.format('{0:P}', percentage)#";
        self.labelsPosition = "outsideEnd";
        self.labelsVisible = false;
        self.labelBackground = "transparent";

        self.tooltipVisible = true;
        self.tooltipTemplate = undefined;

        self.categories = [];
        self.series = [];

        self.dataBound = function (e) { }
    }();

    self.Load = function () {
        if (self.serviceURL != "") {
            // Ajax call graph data
            $.ajax({
                method: 'get',
                url: self.serviceURL,
                contentType: "application/json; charset=utf-8",
                //headers: {
                //    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                //},
                success: function (data) {
                    self.legendPosition = data.legendPosition;
                    self.seriesDefaultsstyle = data.seriesDefaultsstyle;
                    self.valueAxisLineVisible = data.valueAxisLineVisible;

                    if (self.dataURL != "") {
                        self.GetData();
                    } else if (self.categories.length > 0 && self.series.length > 0) {
                        self.LoadGraph();
                    }
                }
            });
        }
        else {
            if (self.dataURL != "") {
                self.GetData();
            } else if (self.categories.length > 0 && self.series.length > 0) {
                self.LoadGraph();
            }
        }

    };

    self.GetData = function (data) {
        $.ajax({
            method: 'get',
            url: self.dataURL,
            contentType: "application/json; charset=utf-8",
            //headers: {
            //    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            //},
            success: function (data) {
                //self.categories = data[5].Value;
                //self.title = data[4].Value;
                //self.series = data[6].Value;
                //self.LoadSeries(data);

                self.LoadGraph();
            }
        });
    };

    self.LoadSeries = function (data) {
        if (self.type.toLowerCase() == "pie" || self.type.toLowerCase() == "funnel") {
            //Push only one series
            var obj = {};
            obj.type = self.type.toLowerCase();
            obj.data = data;
            //$.each(data, function (i1, v1) {
            //    obj.data.push({ category: v1.category, value: v1.value });
            //});
            self.series.push(obj);
        }
        else if (self.type.toLowerCase() == "donut") {
            //Push only one series
            var obj = {};
            obj.data = data;
            //$.each(data, function (i1, v1) {
            //    obj.data.push({ category: v1.category, value: v1.value });
            //});
            self.series.push(obj);
        }
        else {
            self.series = data;
            //$.each(data.data, function (i, v) {
            //    var obj = {};
            //    obj.name = v.seriesName;
            //    obj.data = [];
            //    $.each(v.series, function (i1, v1) {
            //        obj.data.push(v1.value);
            //    });
            //    self.series.push(obj);
            //});
        }
    };

    self.SetDefaultSetting = function () {

        if (self.divId.indexOf('#') == -1 && self.divId.indexOf('.') == -1) {
            self.divId = "#" + self.divId;
        }

        if (self.legendVisible === true && self.legendPosition == undefined) {
            self.legendPosition = self.legendPosition;
        }

        if (self.type == 'line' && self.seriesDefaultsstyle == undefined) {
            self.seriesDefaultsstyle = self.seriesDefaultsstyle
        }

        //if (lineChart.type == "line" && self.valueAxisLineVisible == undefined) {
        //    self.valueAxisLineVisible = self.valueAxisLineVisible;
        //}

        self.seriesDefaultsLable = {};
        if (self.labelsVisible == true) {

            self.seriesDefaultsLable.template = self.labelsTemplate;
            self.seriesDefaultsLable.position = self.labelsPosition;
            self.seriesDefaultsLable.visible = true;
            self.seriesDefaultsLable.background = self.labelBackground;
        }
        //set default template 
        if (self.tooltipVisible == true && self.tooltipTemplate == undefined) {
            if (self.type == "pie" || self.type.toLowerCase() == "donut" || self.type.toLowerCase() == "funnel") {
                self.tooltipTemplate = "#= category # - #= kendo.format('{0:P}', percentage) #";
            } else {
                self.tooltipTemplate = "#= series.name #: #= value #"
            }
        }
    }

    self.LoadGraph = function () {

        self.SetDefaultSetting();

        $(self.divId).kendoChart({
            title: {
                text: self.title
            },
            legend: {
                visible: self.legendVisible,
                position: self.legendPosition
            },
            series: self.series,
            valueAxis: {
                line: {
                    visible: self.valueAxisLineVisible
                },
                minorGridLines: {
                    visible: self.valueAxisMinorGridLinesVisible
                },
                labels: {
                    rotation: self.valueAxisLabelsRotation
                },
                axisCrossingValue: self.valueAxisAxisCrossingValue
            },
            categoryAxis: {
                categories: self.categories,
                majorGridLines: {
                    visible: self.categoryAxisMajorGridLinesVisible
                }
            },
            chartArea: {
                background: self.chartAreaBackground
            },
            seriesDefaults: {
                type: self.type,
                style: self.seriesDefaultsstyle,
                labels: self.seriesDefaultsLable
            },
            tooltip: {
                visible: self.tooltipVisible,
                template: self.tooltipTemplate
            },
            dataBound: function (e) {
                self.dataBound(e);
            }
        });
    };

}
