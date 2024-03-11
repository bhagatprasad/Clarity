function UserDashBoardController() {
    var self = this;
    var requests = [];
    var actions = [];
    var dataObjects = [];
    self.employeeSalaries = [];
    self.ApplicationUser = {};
    actions.push(serviceUrls.fetchEmployeeSalariesById);
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
            dataObjects.push({ userId: self.ApplicationUser.Id });
        }
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };

            ajaxConfig.data = dataObjects[0];
            
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;

            if (responses[0] && responses[0].data) {
                responses[0].data.forEach(function (item) {
                    self.employeeSalaries.push(item.employeeSalary);
                });
            }
            console.log(self.employeeSalaries);
            self.loadPayslipDropdown(self.employeeSalaries);
        }).fail(function () {
            console.log('One or more requests failed.');
        });
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
                $('#dropdownTimesheets,#dropdownLeaves').append(`<option value="${optionValue}">${optionText}</option>`);
            });
        }
    }
    $(document).on("change", "#dropdownPayslipMonth", function (event) {
        console.log(event);
        event.preventDefault();
        var employeeSalaryId = parseInt($(this).val());
        $.ajax({
            url: '/UserDashBoard/GenaratePaySlip',
            type: 'GET',
            data: { employeeSalaryId: JSON.stringify(employeeSalaryId) },
            success: function (status) {
                console.log(status);
                window.location.href = "/UserDashBoard/GenaratePaySlip?employeeSalaryId=" + employeeSalaryId;
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
    self.loadPayslipDropdown = function (response) {
        var $dropdown = $('#dropdownPayslipMonth');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Choose month and year',
            selected: true,
            disabled: true 
        });
        $dropdown.append($defaultOption);
        response.forEach(function (item) {
            var $option = $('<option>', {
                value: item.EmployeeSalaryId,
                text: item.SalaryMonth + " " + item.SalaryYear
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();


    };
}