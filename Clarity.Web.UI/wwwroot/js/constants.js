var serviceUrls = {
    getRoles: "/Roles/LoadRoles",
    getDepartments: "/Department/LoadDepartments",
    getDesignations: "/Designation/fetchAllDesignations",
    getCountries: "/Country/fetchAllCountries",
    getCities: "/City/fetchAllCities",
    getStates: "/State/LoadStates",
    getUsers: "/Employee/fetchAllEmployess"
};

function generateNextCode(data) {

    if (data.length == 0)
        return "EN0097";

    var maxNumericPart = 0;
    data.forEach(function (item) {
        var numericPart = parseInt(item.EmployeeCode.slice(2));
        if (numericPart > maxNumericPart) {
            maxNumericPart = numericPart;
        }
    });

    // Generate the next code
    var nextNumericPart = maxNumericPart + 1;
    var nextCode = "EN" + ("0000" + nextNumericPart).slice(-4);

    return nextCode;
}