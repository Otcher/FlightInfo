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

    var svg = d3.select("#age-group-graph").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")")

    x.domain(data.map(d => d.age * 10 + "-" + Number(d.age * 10 + +9 )))
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