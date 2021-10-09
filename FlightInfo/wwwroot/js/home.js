$(function () {
    $.ajax({
        method: "GET",
        url: "/Passengers/PassengersByAgeGroup",
        success: function (data) {
            alert("success");
        },
        error: function (data) {
            alert("error!");
        }
    });
});


function createPassengerByAgeGroupGraph(data) {
    height = 500;
    width - 500;

    margin = ({ top: 30, right: 0, bottom: 30, left: 40 })

    x = d3.scaleBand()
        .domain(data.map(d => d.age))
        .rangeBound([margin.left, width - margin.right])
        .padding(0.1)

    y = d3.scaleLinear()
        .domain([0, d3.max(data, d => d.count)]).nice()
        .range([height - margin.bottom, margin.top])

    xAxis = g => g
        .attr("transform", `translate(0,${height - margin.bottom})`)
        .call(d3.axisBottom(x).tickFormat(i => i).tickSizeOuter(0))

    yAxis = g => g
        .attr("transform", `translate(${margin.left},0)`)
        .call(d3.axisLeft(y).ticks(null, data.format))
        .call(g => g.select(".domain").remove())
        .call(g => g.append("text")
            .attr("x", -margin.left)
            .attr("y", 10)
            .attr("fill", "currentColor")
            .attr("text-anchor", "start"));


    const svg = d3.select("#age-group-graph")
        .attr("viewBox", [0, 0, width, height]);

    svg.append("g")
        .attr("fill", "cadetblue")
        .selectAll("rect")
        .data(data)
        .join("rect")
        .attr("x", d => x(d.hospitalName))
        .attr("y", d => y(d.count))
        .attr("height", d => y(0) - y(d.count))
        .attr("height", d => y(0) - y(d.count))
        .attr("width", x.bandwidth());

    svg.append("g")
        .call(xAxis);

    svg.append("g")
        .call(yAxis);
}