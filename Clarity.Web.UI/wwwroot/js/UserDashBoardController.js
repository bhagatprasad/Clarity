function UserDashBoardController() {
    var self = this;
    self.init = function () {
        populateDropdownOptions();
        populatedropdownFormSixteenOptions();
    };
    function populateDropdownOptions() {
        var currentYear = new Date().getFullYear();
        var startYear = currentYear - 2; // Start from 2 years ago
        var endYear = currentYear + 2;   // End 2 years from now

        for (var year = endYear; year >= startYear; year--) {
            var optionText = 'FY ' + year + '-' + (year + 1).toString().slice(2); // e.g., "FY 2022-23"
            $('#dropdownFormSixteen,#dropdownHikeLetter').append(`<option value="${year}">${optionText}</option>`);
        }
    }
    function populatedropdownFormSixteenOptions() {
        var currentYear = new Date().getFullYear()-1;
        var months = [
            'January', 'February', 'March', 'April', 'May', 'June',
            'July', 'August', 'September', 'October', 'November', 'December'
        ];

        for (var yearOffset = 0; yearOffset < 2; yearOffset++) {
            var year = currentYear + yearOffset;

            months.forEach(function (month) {
                var optionValue = month.toLowerCase() + '-' + year;
                var optionText = month + ' ' + year;
                $('#dropdownPayslipMonth,#dropdownTimesheets,#dropdownLeaves').append(`<option value="${optionValue}">${optionText}</option>`);
            });
        }
    }
}