( function ( $ ) {
	"use strict";
    var ctx = document.getElementById( "lineChart" );
    ctx.height = 100;
	
    var myChart = new Chart( ctx, {
        type: 'line',
        data: {
            labels: [ "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018"],
            datasets: [
                {
                    label: "Profit",
                    borderColor: "rgb(159, 120, 255)",
                    borderWidth: "4",
                    backgroundColor: "rgb(159, 120, 255, 0.5)",
                    data: [0, 30, 60, 25, 60, 25, 50, 10, 50, 90, 120]
                            },
                {
                    label: "sales",
                    borderColor: "rgb(50, 202, 254)",
                    borderWidth: "4",
                    backgroundColor: "rgba(50, 202, 254, 0.5)",
                    pointHighlightStroke: "rgba(26, 179, 148, 1)",
                    data: [0, 60, 25, 100, 20, 75, 30, 55, 20, 60, 20],
                            }
                        ]
        },
        options: {
            responsive: true,
            tooltips: {
                mode: 'index',
                intersect: false,
				
            },
			tooltips: {
				  
				},
            hover: {
                mode: 'nearest',
                intersect: true
            }

        }
    } );
} )( jQuery );

