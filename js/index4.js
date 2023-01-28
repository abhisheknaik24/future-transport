$(function(e){
  'use strict'
  /*-----echart8-----*/
  var chartdata5 = [
    {
      name: 'profit',
      type: 'line',
      smooth: true,
      data: [20, 20, 36, 18, 15, 20, 25, 20],
	  symbolSize:10,
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
      data: [ 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul','Aug', 'Sep'],
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
  
  /*-----echart3-----*/
   var chartdata = [
    {
      name: 'profit',
      type: 'bar',
      data: [15325, 10582, 12852, 18658, 10452]
    },
    
    {
      name: 'loss',
      type: 'bar',
      data: [10526, 14325, 10326, 15625, 14254]
    }
  ];
  var option3 = {
    grid: {
      top: '6',
      right: '0',
      bottom: '17',
      left: '32',
    },
    xAxis: {
      type: 'value',
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
      type: 'category',
      data: [ '2014', '2015', '2016', '2017', '2018'],
      splitLine: {
        lineStyle: {
          color: '#eaeaea'
        }
      },
      axisLine: {
        lineStyle: {
          color: '#c0dfd8'
        }
      },
      axisLabel: {
        fontSize: 10,
        color: '#000'
      }
    },
    series: chartdata,
	color:[ '#9f78ff','#32cafe',]
	};

  var chart3 = document.getElementById('echart3');
  var barChart3 = echarts.init(chart3);
  barChart3.setOption(option3);

  });