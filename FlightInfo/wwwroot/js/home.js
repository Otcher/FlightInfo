$(function () {
    $.ajax({
        method: "GET",
        url: "/Hospitals/PatientsByHospitalCount",
        success: function (data) {
            alert("success");
        },
        error: function (data) {
            alert("error!");
        }
    });

    $.ajax({
        method: "GET",
        url: "Doctors/PatientsForDoctorStatistic",
        success: function (data) {
            alert("success");
        },
        error: function (data) {
            alert("error!");
        }
    });
});