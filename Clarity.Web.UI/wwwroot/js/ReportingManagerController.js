function ReportingManagerController() {
    var self = this;
    self.coreEmployeesData = [];
    self.coreManagersData = [];
    self.alreadyAvilableList = [];
    var requests = [];
    var actions = [];
    actions.push(serviceUrls.getUsers);
    self.init = function () {
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            if (responses[0] && responses[0].data) {
                responses[0].data.forEach(function (item) {
                    self.coreEmployeesData.push(item.employee);
                    self.coreManagersData.push(item.employee);
                    self.alreadyAvilableList.push(item.employee);
                });
            }
            self.loadEmployeesDropdown(self.coreEmployeesData);
            self.loadManagersDropdown(self.coreManagersData);
            console.log(responses);
        }).fail(function () {
            console.log('One or more requests failed.');
        });
        var reposrtingManagerGrid = $('#ReportingManagerGrid').DataTable({
            ajax: {
                url: '/ReportingManager/FetchAllReportingManager',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'EmployeeId' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.EmployeeName + "(" + row.EmployeeCode + "-" + row.EmployeeEmail + ")";
                    }
                },
                { data: 'ManagerId' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.ManagerName + "(" + row.ManagerCode + "-" + row.ManagerEmail + ")";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-edit icon-padding-right"  style="font-size:25px;color:blue" data-id="' + row.RepotingManagerId + '" ></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            "order": [[0, "asc"]],
            "pageLength": 20
        });

        $(document).on("click", "#addReportingManager", function () {
            $("#AddOrChangeManagerModal").modal("show");
        })
        self.loadEmployeesDropdown = function (response) {
            var $dropdown = $('#dropdownEmployees');
            $dropdown.empty();

            var $defaultOption = $('<option>', {
                value: '',
                text: 'Select an Employee'
            });
            $dropdown.append($defaultOption);

            response.forEach(function (item) {
                var $option = $('<option>', {
                    value: item.EmployeeId,
                    text: item.FirstName + " " + item.LastName + " (" + item.EmployeeCode + "-" + item.Email + " )"
                });
                $dropdown.append($option);
            });
            $dropdown.dropdown();
        };
        self.loadManagersDropdown = function (response) {

            var $dropdown = $('#dropdownManager');
            $dropdown.empty();

            var $defaultOption = $('<option>', {
                value: '',
                text: 'Select an Manager'
            });
            $dropdown.append($defaultOption);

            response.forEach(function (item) {
                var $option = $('<option>', {
                    value: item.EmployeeId,
                    text: item.FirstName + " " + item.LastName + " (" + item.EmployeeCode + "-" + item.Email + " )"
                });
                $dropdown.append($option);
            });
            $dropdown.dropdown();
        };

        $(document).on("change", "#dropdownEmployees", function () {
            var employee = $(this).val();
            console.log(employee);
        });
    };
}