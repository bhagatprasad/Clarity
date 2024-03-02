function EmployeeController() {
    var self = this;
    self.roles = [];
    self.departments = [];
    self.designation = [];
    self.countries = [];
    self.states = [];
    self.citis = [];
    self.employeeEmergencyContacts = [];
    self.employeeEducations = [];
    self.employeeAddresses = [];
    self.employeeEmployments = [];
    self.employees = [];
    var requests = [];
    var actions = [];
    actions.push(serviceUrls.getRoles);
    actions.push(serviceUrls.getDesignations);
    actions.push(serviceUrls.getDepartments);
    actions.push(serviceUrls.getCountries);
    actions.push(serviceUrls.getStates);
    actions.push(serviceUrls.getCities);
    actions.push(serviceUrls.getUsers);
    self.init = function () {
        $('#AddEditEmployeeDetailsModal').modal({ backdrop: 'static', keyboard: false });
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            self.roles = responses[0][0] ? responses[0][0].data : [];
            self.designation = responses[0][1] ? responses[0][1].data : [];
            self.departments = responses[0][2] ? responses[0][2].data : [];
            self.countries = responses[0][3] ? responses[0][3].data : [];
            self.states = responses[0][4] ? responses[0][4].data : [];
            self.citis = responses[0][5] ? responses[0][5].data : [];
            self.employees = responses[0][6] ? responses[0][6].data.employee : [];
            console.log(responses);
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        var employeeGrid = $('#EmployeesGrid').DataTable({
            data: self.employees,
            columns: [
                { data: 'EmployeeCode' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.FirstName + " " + row.LastName;
                    }
                },
                { data: 'Email' },
                { data: 'Phone' },
                {
                    data: null,
                    render: function (data, type, row) {
                        var department = $.grep(self.departments, function (department) {
                            return department.DepartmentId === row.DepartmentId;
                        });
                        return department[0] ? department[0].Name : "";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var roles = $.grep(self.roles, function (role) {
                            return role.Id === row.RoleId;
                        });
                        return roles[0] ? roles[0].Name : "";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var designs = $.grep(self.designation, function (design) {
                            return design.DesignationId === row.DesignationId;
                        });
                        return designs[0] ? designs[0].Name : "";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.EmployeeId + '"></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            "order": [[0, "asc"]],
            "pageLength": 20
        });


        var educationGrid = $('#EducationGrid').DataTable({
            data: self.employeeEducations,
            columns: [
                { data: 'Degree' },
                { data: 'FieldOfStudy' },
                { data: 'Institution' },
                { data: 'YearOfCompletion' },
                { data: 'PercentageMarks' }
            ]
        });

        var employmentGrid = $('#EmployementGrid').DataTable({
            data: self.employeeEmployments,
            columns: [
                { data: 'CompanyName' },
                { data: 'Designation' },
                { data: 'Address' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.StartedOn + " " + row.EndedOn;
                    }
                },
                { data: 'Reason' },
                { data: 'ReportingManager' },
                { data: 'HREmail' },
                { data: 'Reference' }
            ]
        });

        var contactsGrid = $('#EmployeeContactsGrid').DataTable({
            data: self.employeeEmergencyContacts,
            columns: [
                { data: 'Name' },
                { data: 'Relation' },
                { data: 'Phone' },
                { data: 'Email' },
                { data: 'Address' }
            ]
        });

        $('#toggleDrawer').click(function () {
            $('#AddEditEmployeeDetailsModal').modal('show');
        });

        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
        });
        $('#DateOfBirth,#OfferAcceptedOn,#OfferRelesedOn,#LastWorkingDay,#ResignedOn,#EndedOn,#StartedOn,#YearOfCompletion').datepicker({
            format: 'dd-mm-yyyy',
            autoclose: true,
            endDate: 'today',
            orientation: 'bottom'
        });
    };
}