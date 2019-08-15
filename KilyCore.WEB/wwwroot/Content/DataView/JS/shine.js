(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['exports', 'echarts'], factory);
    } else if (typeof exports === 'object' && typeof exports.nodeName !== 'string') {
        // CommonJS
        factory(exports, require('echarts'));
    } else {
        // Browser globals
        factory({}, root.echarts);
    }
}(this, function (exports, echarts) {
    var log = function (msg) {
        if (typeof console !== 'undefined') {
            console && console.error && console.error(msg);
        }
    };
    if (!echarts) {
        log('ECharts is not Loaded');
        return;
    }
    echarts.registerTheme('shine', {
        "color": [
            "#c12e34",
            "#e6b600",
            "#0098d9",
            "#2b821d",
            "#005eaa",
            "#339ca8",
            "#cda819",
            "#32a487"
        ],
        "backgroundColor": "rgba(0,0,0,0)",
        "textStyle": {},
        "title": {
            "textStyle": {
                "color": "#ffffff"
            },
            "subtextStyle": {
                "color": "#ffffff"
            }
        },
        "line": {
            "itemStyle": {
                "normal": {
                    "borderWidth": "0"
                }
            },
            "lineStyle": {
                "normal": {
                    "width": "1"
                }
            },
            "symbolSize": "3",
            "symbol": "circle",
            "smooth": true
        },
        "radar": {
            "itemStyle": {
                "normal": {
                    "borderWidth": "0"
                }
            },
            "lineStyle": {
                "normal": {
                    "width": "1"
                }
            },
            "symbolSize": "3",
            "symbol": "circle",
            "smooth": true
        },
        "bar": {
            "itemStyle": {
                "normal": {
                    "barBorderWidth": 0,
                    "barBorderColor": "#ffffff"
                },
                "emphasis": {
                    "barBorderWidth": 0,
                    "barBorderColor": "#ffffff"
                }
            }
        },
        "pie": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "scatter": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "boxplot": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "parallel": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "sankey": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "funnel": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "gauge": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            }
        },
        "candlestick": {
            "itemStyle": {
                "normal": {
                    "color": "#c12e34",
                    "color0": "#2b821d",
                    "borderColor": "#c12e34",
                    "borderColor0": "#2b821d",
                    "borderWidth": 1
                }
            }
        },
        "graph": {
            "itemStyle": {
                "normal": {
                    "borderWidth": 0,
                    "borderColor": "#ffffff"
                }
            },
            "lineStyle": {
                "normal": {
                    "width": 1,
                    "color": "#ffffff"
                }
            },
            "symbolSize": "3",
            "symbol": "circle",
            "smooth": true,
            "color": [
                "#c12e34",
                "#e6b600",
                "#0098d9",
                "#2b821d",
                "#005eaa",
                "#339ca8",
                "#cda819",
                "#32a487"
            ],
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                }
            }
        },
        "map": {
            "itemStyle": {
                "normal": {
                    "areaColor": "#126aee",
                    "borderColor": "#ffffff",
                    "borderWidth": "1"
                },
                "emphasis": {
                    "areaColor": "rgba(230,182,0,1)",
                    "borderColor": "#ffffff",
                    "borderWidth": 1
                }
            },
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                }
            }
        },
        "geo": {
            "itemStyle": {
                "normal": {
                    "areaColor": "#126aee",
                    "borderColor": "#ffffff",
                    "borderWidth": "1"
                },
                "emphasis": {
                    "areaColor": "rgba(230,182,0,1)",
                    "borderColor": "#ffffff",
                    "borderWidth": 1
                }
            },
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                }
            }
        },
        "categoryAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#ffffff"
                }
            },
            "splitLine": {
                "show": false,
                "lineStyle": {
                    "color": [
                        "#ccc"
                    ]
                }
            },
            "splitArea": {
                "show": false,
                "areaStyle": {
                    "color": [
                        "rgba(250,250,250,0.3)",
                        "rgba(200,200,200,0.3)"
                    ]
                }
            }
        },
        "valueAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#ffffff"
                }
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#ffffff"
                    ]
                }
            },
            "splitArea": {
                "show": false,
                "areaStyle": {
                    "color": [
                        "rgba(250,250,250,0.3)",
                        "rgba(200,200,200,0.3)"
                    ]
                }
            }
        },
        "logAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#ffffff"
                }
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#ffffff"
                    ]
                }
            },
            "splitArea": {
                "show": false,
                "areaStyle": {
                    "color": [
                        "rgba(250,250,250,0.3)",
                        "rgba(200,200,200,0.3)"
                    ]
                }
            }
        },
        "timeAxis": {
            "axisLine": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisTick": {
                "show": true,
                "lineStyle": {
                    "color": "#ffffff"
                }
            },
            "axisLabel": {
                "show": true,
                "textStyle": {
                    "color": "#ffffff"
                }
            },
            "splitLine": {
                "show": true,
                "lineStyle": {
                    "color": [
                        "#ffffff"
                    ]
                }
            },
            "splitArea": {
                "show": false,
                "areaStyle": {
                    "color": [
                        "rgba(250,250,250,0.3)",
                        "rgba(200,200,200,0.3)"
                    ]
                }
            }
        },
        "toolbox": {
            "iconStyle": {
                "normal": {
                    "borderColor": "#ffffff"
                },
                "emphasis": {
                    "borderColor": "#ffffff"
                }
            }
        },
        "legend": {
            "textStyle": {
                "color": "#ffffff"
            }
        },
        "tooltip": {
            "axisPointer": {
                "lineStyle": {
                    "color": "#ffffff",
                    "width": 1
                },
                "crossStyle": {
                    "color": "#ffffff",
                    "width": 1
                }
            }
        },
        "timeline": {
            "lineStyle": {
                "color": "#ffffff",
                "width": 1
            },
            "itemStyle": {
                "normal": {
                    "color": "#ffffff",
                    "borderWidth": "1"
                },
                "emphasis": {
                    "color": "#ffffff"
                }
            },
            "controlStyle": {
                "normal": {
                    "color": "#ffffff",
                    "borderColor": "#ffffff",
                    "borderWidth": "0"
                },
                "emphasis": {
                    "color": "#ffffff",
                    "borderColor": "#ffffff",
                    "borderWidth": "0"
                }
            },
            "checkpointStyle": {
                "color": "#ffffff",
                "borderColor": "#ffffff"
            },
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                }
            }
        },
        "visualMap": {
            "color": [
                "#126aee",
                "#127aee",
                "#128aee",
                "#129aee"
            ]
        },
        "dataZoom": {
            "backgroundColor": "rgba(47,69,84,0)",
            "dataBackgroundColor": "rgba(47,69,84,0.3)",
            "fillerColor": "rgba(167,183,204,0.4)",
            "handleColor": "#ffffff",
            "handleSize": "100%",
            "textStyle": {
                "color": "#ffffff"
            }
        },
        "markPoint": {
            "label": {
                "normal": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                },
                "emphasis": {
                    "textStyle": {
                        "color": "#ffffff"
                    }
                }
            }
        }
    });
}));
