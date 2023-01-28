$(function(e){
  'use strict'
  /*-----echart2-----*/
  var chartdata = [
    {
      name: 'sales',
      type: 'line',
	  smooth:true,
	  data: [4, 16, 22, 28, 14, 0],
	  symbolSize:18,
	  lineStyle: {
        normal: { width: 5 }
      },
    },
    {
      name: 'profit',
      type: 'line',
	  smooth:true,
      data: [0, 20, 10, 15, 8, 5, 0],
	  symbolSize:18,
	  lineStyle: {
			normal: { width: 5 }
		},
	  
    }
  ];

  var chart = document.getElementById('echart1');
  var barChart = echarts.init(chart);
	var symbolSize = 50;
  var option = {
    grid: {
      top: '6',
      right: '0',
      bottom: '17',
      left: '25',
    },
    xAxis: {
      data: [  '2013', '2014', '2015', '2016', '2017', '2018'],
      axisLine: {
        lineStyle: {
          color: '#eaeaea'
        }
      },
      axisLabel: {
        fontSize: 10,
        color: '#000'
      }
    },
	tooltip: {
		show: true,
		showContent: true,
		alwaysShowContent: true,
		triggerOn: 'mousemove',
		trigger: 'axis',
		axisPointer:
		{
			label: {
				show: false,
			},
		}
	},
    yAxis: {
      splitLine: {
        lineStyle: {
          color: 'none'
        }
      },
      axisLine: {
        lineStyle: {
          color: '#eaeaea'
        }
      },
      axisLabel: {
        fontSize: 10,
        color: '#000'
      }
    },
    series: chartdata,
    color:[ '#9f78ff','#32cafe']
  };

  barChart.setOption(option);
  
  var chartdata5 = [
    {
      name: 'data',
      type: 'line',
      smooth: true,
      data: [2, 15, 28, 35, 25, 20, 15, 20],
	   lineStyle: {
        normal: { width: 1 }
      },
	  symbolSize:15,
      itemStyle: {
        normal: {
          areaStyle: { type: 'default' ,
			color: new echarts.graphic.LinearGradient(
				0, 0, 0, 1,
				[
					{offset: 0, color: '#9f78ff'},
					{offset: 1, color: '#32cafe'}
				]
			)
		  }
        }
      }
    }
  ];

  var option8 = {
    grid: {
      top: '6',
      right: '0',
      bottom: '17',
      left: '25',
    },
    xAxis: {
      data: [ 'MON', 'TUE', 'WEN', 'THUR', 'FRI', 'SAT','SUN', 'MON'],
      axisLine: {
        lineStyle: {
          color: '#eaeaea'
        },
		
      },
	  
      axisLabel: {
        fontSize: 10,
        color: '#000'
      }
    },
	tooltip: {
		show: true,
		showContent: true,
		alwaysShowContent: true,
		triggerOn: 'mousemove',
		trigger: 'axis',
		axisPointer:
		{
			label: {
				show: false,
			},
		}
	},
    yAxis: {
      splitLine: {
        lineStyle: {
          color: '#eaeaea'
        }
      },
      axisLine: {
        lineStyle: {
          color: '#eaeaea'
        }
      },
      axisLabel: {
        fontSize: 10,
        color: '#000'
      }
    },
    series: chartdata5,
    color:[ '#32cafe']
  };

  var chart8 = document.getElementById('echart8');
  var lineChart2 = echarts.init(chart8);
  lineChart2.setOption(option8);

  });