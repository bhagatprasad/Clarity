function ReportingManagerController() {
    var self = this;
    self.ApplicationUser = {};
    self.coreEmployeesData = [];
    self.coreManagersData = [];
    self.alreadyAvilableList = [];
    self.managersList = [];
    var requests = [];
    var actions = [];
    actions.push(serviceUrls.getUsers);
    actions.push('/ReportingManager/FetchAllReportingManager');
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        var form = $('#AddOrChangeManagerForm');
        var signUpButton = $('#AddOrChangeReportingManager');
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
                        return '<i class="fas fa fa-unlink icon-padding-right"  style="font-size:25px;color:blue" data-id="' + row.RepotingManagerId + '" ></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '400px'
        });
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            if (responses[0][0] && responses[0][0].data) {
                responses[0][0].data.forEach(function (item) {
                    self.coreEmployeesData.push(item.employee);
                    self.coreManagersData.push(item.employee);
                    self.alreadyAvilableList.push(item.employee);
                });
            }


            self.loadManagersDropdown(self.coreManagersData);
            self.managersList = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];

            //loading employee dropdonw which is not avilable in managers list
            if (self.coreEmployeesData && self.managersList) {
                var mainArray = $.grep(self.coreEmployeesData, function (mainUser) {
                    return $.grep(self.managersList, function (userToRemove) {
                        return userToRemove.EmployeeId === mainUser.EmployeeId;
                    }).length === 0;
                });

                self.loadEmployeesDropdown(mainArray);
            } else {
                self.loadEmployeesDropdown(self.coreEmployeesData);
            }

            console.log(responses);
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();

        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
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
            self.loadManagersDropdownBasedOnEmployee(getManagersBasedOnEmployee(parseInt(employee)));

        });

        $(document).on("click", "#AddOrChangeReportingManager", function () {
            var employeeId = $('#dropdownEmployees').val();
            var managerId = $('#dropdownManager').val();
            var repotingManagerId = $("#RepotingManagerId").val();

            var resportingManager = {
                RepotingManagerId: repotingManagerId ? parseInt(repotingManagerId) : 0,
                EmployeeId: parseInt(employeeId),
                ManagerId: parseInt(managerId),
                CreatedOn: new Date(),
                CreatedBy: self.ApplicationUser?self.ApplicationUser.Id:1,
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser ? self.ApplicationUser.Id : 1,
                IsActive: true
            };
            $.ajax({
                url: '/ReportingManager/AddEditReportingManager',
                data: JSON.stringify(resportingManager),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#AddOrChangeManagerModal').modal('hide');
                    $("#AddOrChangeManagerForm")[0].reset();
                    reposrtingManagerGrid.ajax.reload();
                    if (response[0] && response[0].data) {
                        response[0].data.forEach(function (item) {
                            self.coreEmployeesData.push(item.employee);
                            self.coreManagersData.push(item.employee);
                            self.alreadyAvilableList.push(item.employee);
                        });
                    }
                    self.loadManagersDropdown(self.coreManagersData);
                    self.managersList = reposrtingManagerGrid.data();
                    
                    if (self.coreEmployeesData && self.managersList) {
                        var mainArray = $.grep(self.coreEmployeesData, function (mainUser) {
                            return $.grep(self.managersList, function (userToRemove) {
                                return userToRemove.EmployeeId === mainUser.EmployeeId;
                            }).length === 0;
                        });

                        self.loadEmployeesDropdown(mainArray);
                    } else {
                        self.loadEmployeesDropdown(self.coreEmployeesData);
                    }
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });
        $(document).on("click", ".fa-unlink", function () {
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = reposrtingManagerGrid.row(row).data();
            console.log(dataItem);
            self.loadEmployeesDropdown(self.coreEmployeesData);
            $("#RepotingManagerId").val(parseInt(dataItem.RepotingManagerId));
            $("#dropdownEmployees").val(dataItem.EmployeeId);
            $("#dropdownEmployees").prop('disabled', true);
            $('#dropdownManager').val(dataItem.ManagerId);
            $("#AddOrChangeManagerModal").modal('show');
        });
        $(document).on("click", "#closeBtn", function () {
            $('#dropdownEmployees').prop('selectedIndex', 0);
            $('#dropdownManager').prop('selectedIndex', 0);
            $("#dropdownEmployees").prop('disabled', false);
            $("#RepotingManagerId").val('');
            $("#AddOrChangeManagerModal").modal('show');
        });
        self.loadManagersDropdownBasedOnEmployee = function (response) {
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
        }
    };
    function getManagersBasedOnEmployee(employeeId) {

        var managers = [];
        if (employeeId) {
            managers = (self.coreManagersData) ? self.coreManagersData.filter(x => x.EmployeeId != employeeId) : [];
        }
        else {
            managers = self.coreManagersData;
        }
        return managers;
    };
}