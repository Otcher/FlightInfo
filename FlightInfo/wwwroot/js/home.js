$(function () {
    $.ajax({
        method: "GET",
        url: "https://api.covid19api.com/summary",
        success: function (data) {
            var globalReasults = data["Global"];
            globalReasults["Country"] = "Global";
            var results = [globalReasults]

            for (const country of data["Countries"]) {
                results.push(country);
            }
            $('tbody').html('');
            $('#global-covid19-data-results').tmpl(results).appendTo('tbody');


        },
        error: function (data) {
            alert("error!");
        }
    });

    $.ajax({
        method: "GET",
        url: "/Passengers/PassengersByAgeGroup",
        success: function (data) {
            createPassengerByAgeGroupGraph(data);
        },
        error: function (data) {
            alert("error!");
        }
    });

    $.ajax({
        method: "GET",
        url: "/Countries/AirpotsInCountries",
        success: function (data) {
            airportsInCountriesCountGraph(data);
        },
        error: function (data) {
            alert("error!");
        }
    });

    $.ajax({
        method: "GET",
        url: "/Countries/FlightsToCountries",
        success: function (data) {
            getFlightByCountryCountGraph(data);
        },
        error: function (data) {
            alert("error!");
        }
    });
});

function createPassengerByAgeGroupGraph(data) {
    var margin = { top: 20, right: 20, bottom: 30, left: 40 },
        width = 500 - margin.left - margin.right,
        height = 580 - margin.top - margin.bottom

    // set the ranges
    var x = d3.scaleBand()
        .range([0, width])
        .padding(0.1)

    var y = d3.scaleLinear()
        .range([height, 0])

    var svg = d3.select("#age-distribution-graph").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")")

    x.domain(data.map(d => d.age * 10 + "-" + Number(d.age * 10 + +9)))
    y.domain([0, d3.max(data, function (d) { return d.count; })])

    svg.selectAll(".bar")
        .data(data)
        .enter().append("rect")
        .attr("x", function (d) { return x(d.age * 10 + "-" + Number(d.age * 10 + +9)); })
        .attr("width", x.bandwidth())
        .attr("y", function (d) { return y(d.count); })
        .attr("height", function (d) { return height - y(d.count); })
        .attr("fill", "aliceblue")


    // add the x Axis
    svg.append("g")
        .attr("transform", "translate(0," + height + ")")
        .call(d3.axisBottom(x));

    // add the y Axis
    svg.append("g")
        .call(d3.axisLeft(y));

}

function getFlightByCountryCountGraph(data) {
    var width = 500
    height = 500
    margin = 30

    // The radius of the pieplot is half the width or half the height (smallest one). I subtract a bit of margin.
    var radius = Math.min(width, height) / 2 - margin

    // append the svg object to the div called 'patients-by-doctor-graph'
    var svg = d3.select("#flight-country-graph")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    var color = d3.scaleOrdinal()
        .domain(data)
        .range(d3.schemeSet2);

    var pie = d3.pie()
        .value(function (d) {
            return d.value.flightsCount;
        })
    var data_ready = pie(d3.entries(data))

    var arcGenerator = d3.arc()
        .innerRadius(0)
        .outerRadius(radius)

    svg
        .selectAll('mySlices')
        .data(data_ready)
        .enter()
        .append('path')
        .attr('d', arcGenerator)
        .attr('fill', function (d) {
            return (color(d.data.value.countryName))
        })
        .attr("stroke", "black")
        .style("stroke-width", "2px")
        .style("opacity", 0.7)

    svg
        .selectAll('mySlices')
        .data(data_ready)
        .enter()
        .append('text')
        .text(function (d) {
            return d.data.value.countryName
        })
        .attr("transform", function (d) { return "translate(" + arcGenerator.centroid(d) + ")"; })
        .style("text-anchor", "middle")
        .style("font-size", 17)

}


function airportsInCountriesCountGraph(data) {
    var margin = { top: 20, right: 20, bottom: 30, left: 40 },
        width = 500 - margin.left - margin.right,
        height = 580 - margin.top - margin.bottom

    // set the ranges
    var x = d3.scaleBand()
        .range([0, width])
        .padding(0.1)

    var y = d3.scaleLinear()
        .range([height, 0])

    var svg = d3.select("#airport-country-graph").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")")

    x.domain(data.map(d => d.countryName))
    y.domain([0, d3.max(data, function (d) { return d.airportsCount; })])

    svg.selectAll(".bar")
        .data(data)
        .enter().append("rect")
        .attr("x", function (d) { return x(d.countryName); })
        .attr("width", x.bandwidth())
        .attr("y", function (d) { return y(d.airportsCount); })
        .attr("height", function (d) { return height - y(d.airportsCount); })
        .attr("fill", "aliceblue")


    // add the x Axis
    svg.append("g")
        .attr("transform", "translate(0," + height + ")")
        .call(d3.axisBottom(x));

    // add the y Axis
    svg.append("g")
        .call(d3.axisLeft(y));

}