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
    self.userAccess = {};
    self.tenants = [];
    var requests = [];
    var actions = [];
    self.salaryHikeItem = {};
    self.ApplicationUser = {};
    actions.push(serviceUrls.getRoles);
    actions.push(serviceUrls.getDesignations);
    actions.push(serviceUrls.getDepartments);
    actions.push(serviceUrls.getCountries);
    actions.push(serviceUrls.getStates);
    actions.push(serviceUrls.getCities);
    actions.push(serviceUrls.getUsers);
    actions.push(serviceUrls.getFetchUsers);
    self.init = function () {

        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
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
                        var icons = '';
                        var exists = $.grep(self.tenants, function (record) {
                            return record.EmployeeId === row.EmployeeId;
                        }).length > 0;
                        if (exists) {
                            icons += '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.EmployeeId + '" style="font-size: 20px;color: red;"></i>' +
                                '<i class="fas fa-solid fa-money money-icon icon-padding-right" data-id="' + row.EmployeeId + '" style="font-size: 20px; color: blue;padding-left: 5px;"></i>';
                        } else {
                            icons += '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.EmployeeId + '" style="font-size: 20px;color: red;"></i>' +
                                '<i class="fas fa-solid fa-user-circle-o assign-icon icon-padding-right" data-id="' + row.EmployeeId + '" style="font-size: 20px; color: green;padding-left: 5px;"></i>';
                        }

                        return icons;
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            "order": [[0, "asc"]],
            "pageLength": 20
        });
        $('#AddEditEmployeeDetailsModal').modal({ backdrop: 'static', keyboard: false });
        $('#CreateUserAccessModal').modal({ backdrop: 'static', keyboard: false });
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
            self.designation = responses[1][0] ? responses[1][0].data : [];
            self.departments = responses[2][0] ? responses[2][0].data : [];
            self.countries = responses[3][0] ? responses[3][0].data : [];
            self.states = responses[4][0] ? responses[4][0].data : [];
            self.citis = responses[5][0] ? responses[5][0].data : [];

            if (responses[6][0] && responses[6][0].data) {
                responses[6][0].data.forEach(function (item) {
                    self.employees.push(item.employee);
                });
            }
            self.tenants = responses[7][0] ? responses[7][0].data : [];
            self.loadRoleDropdown(self.roles);
            self.loadDepartmentsDropdown(self.departments);
            self.loadDesignationsDropdown(self.designation);
            self.loadCountriesDropdown(self.countries);
            self.loadStatesBasedOnCountry();
            self.loadCitiesByStates();
            employeeGrid.clear().rows.add(self.employees).draw();
            employeeGrid.draw();
            var employeeCode = generateNextCode(self.employees);
            $("#Code").val(employeeCode);
            $('#Code').prop('disabled', true);
            console.log(responses);
        }).fail(function () {
            console.log('One or more requests failed.');
        });




        var educationGrid = $('#EducationGrid').DataTable({
            data: self.employeeEducations,
            columns: [
                { data: 'Degree' },
                { data: 'FeildOfStudy' },
                { data: 'Institution' },
                { data: 'YearOfCompletion' },
                { data: 'PercentageMarks' }
            ]
        });
        var employeeAddressGrid = $('#EmployeeAddressGrid').DataTable({
            data: self.employeeEmployments,
            columns: [
                { data: 'HNo' },
                { data: 'AddressLineOne' },
                { data: 'AddressLineTwo' },
                {
                    data: null,
                    render: function (data, type, row) {
                        var city = $.grep(self.citis, function (design) {
                            return design.Id === row.CityId;
                        });
                        return city[0] ? city[0].Name : "";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var state = $.grep(self.states, function (design) {
                            return design.StateId === row.StateId;
                        });
                        return state[0] ? state[0].Name : "";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var country = $.grep(self.countries, function (design) {
                            return design.Id === row.CountryId;
                        });
                        return country[0] ? country[0].Name : "";
                    }
                },
                { data: 'Zipcode' }
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

        $(document).on("click", "#AddEditEmployeeContacts", function () {
            var Name = $("#Name").val();
            var contactEmail = $("#contactEmail").val();
            var contactPhone = $("#contactPhone").val();
            var Relation = $("#Relation").val();
            var contactAddress = $("#contactAddress").val();
            var contact = {
                EmployeeEmergencyContactId: 0,
                EmployeeId: 0,
                Name: Name,
                Relation: Relation,
                Phone: contactPhone,
                Email: contactEmail,
                Address: contactAddress,
                CreatedOn: new Date(),
                CreatedBy: 1,
                ModifiedOn: new Date(),
                ModifiedBy: 1,
                IsActive: true
            };
            self.employeeEmergencyContacts.push(contact);
            contactsGrid.clear().rows.add(self.employeeEmergencyContacts).draw();
            contactsGrid.draw();


            $("#Name").val("");
            $("#contactEmail").val("");
            $("#contactPhone").val("");
            $("#contactAddress").val("");
            $('#Relation').prop('selectedIndex', 0);
        });

        $(document).on("click", "#AddEditEmployeeAddress", function () {
            var HNo = $("#HNo").val();
            var AddressLineOne = $("#AddressLineOne").val();
            var AddressLineTwo = $("#AddressLineTwo").val();
            var CountryId = $("#CountryId").val();
            var StateId = $("#StateId").val();
            var CityId = $("#CityId").val();
            var Landmark = $("#Landmark").val();
            var Zipcode = $("#Zipcode").val();
            var address = {
                EmployeeAddressId: 0,
                EmployeeId: 0,
                HNo: HNo,
                AddressLineOne: AddressLineOne,
                AddressLineTwo: AddressLineTwo,
                Landmark: Landmark,
                CityId: parseInt(CityId),
                StateId: parseInt(StateId),
                CountryId: parseInt(CountryId),
                Zipcode: Zipcode,
                CreatedOn: new Date(),
                CreatedBy: 1,
                ModifiedOn: new Date(),
                ModifiedBy: 1,
                IsActive: true
            };
            self.employeeAddresses.push(address);
            employeeAddressGrid.clear().rows.add(self.employeeAddresses).draw();
            employeeAddressGrid.draw();

            $("#HNo").val("");
            $("#AddressLineOne").val("");
            $("#AddressLineTwo").val("");
            $('#CountryId').prop('selectedIndex', 0);
            $('#StateId').prop('selectedIndex', 0);
            $('#CityId').prop('selectedIndex', 0);
            $("#Zipcode").val("");
        });

        $(document).on("click", "#AddEmployement", function () {
            var CompanyName = $("#CompanyName").val();
            var Address = $("#Address").val();
            var Designation = $("#EmpDesignation").val();
            var Reason = $("#Reason").val();
            var ReportingManager = $("#ReportingManager").val();
            var HREmail = $("#HREmail").val();
            var Reference = $("#Reference").val();
            var StartedOn = $("#EmpStartedOn").val();
            var EndedOn = $("#EmpEndedOn").val();

            var employment = {
                EmployeeEmploymentId: 0,
                EmployeeId: 0,
                CompanyName: CompanyName,
                Address: Address,
                Designation: Designation,
                StartedOn: new Date(StartedOn),
                EndedOn: new Date(EndedOn),
                Reason: Reason,
                ReportingManager: ReportingManager,
                HREmail: HREmail,
                Reference: Reference,
                CreatedOn: new Date(),
                CreatedBy: 1,
                ModifiedOn: new Date(),
                ModifiedBy: 1,
                IsActive: true
            };
            self.employeeEmployments.push(employment);
            employmentGrid.clear().rows.add(self.employeeEmployments).draw();
            employmentGrid.draw();


            $("#CompanyName").val("");
            $("#Address").val("");
            $("#Designation").val("");
            $("#Reason").val("");
            $("#ReportingManager").val("");
            $("#HREmail").val("");
            $("#Reference").val("");
            $("#StartedOn").val("");
            $("#EndedOn").val("");

        });

        $(document).on("click", "#AddEditEducation", function () {
            var Degree = $("#Degree").val();
            var FeildOfStudy = $("#FieldOfStudy").val();
            var Institution = $("#Institution").val();
            var YearOfCompletion = $("#YearOfCompletion").val();
            var PercentageMarks = $("#PercentageMarks").val();
            var education = {
                EmployeeEducationId: 0,
                EmployeeId: 0,
                Degree: Degree,
                FeildOfStudy: FeildOfStudy,
                Institution: Institution,
                YearOfCompletion: new Date(YearOfCompletion),
                PercentageMarks: PercentageMarks,
                CreatedOn: new Date(),
                CreatedBy: 1,
                ModifiedOn: new Date(),
                ModifiedBy: 1,
                IsActive: true
            };

            self.employeeEducations.push(education);
            educationGrid.clear().rows.add(self.employeeEducations).draw();
            educationGrid.draw();


            $('#Degree').prop('selectedIndex', 0);
            $('#FieldOfStudy').prop('selectedIndex', 0);
            $("#Institution").val("");
            $("#YearOfCompletion").val("");
            $("#PercentageMarks").val("");
        });

        $(document).on("click", "#AddEditEmployee", function () {

            var Code = $("#Code").val();
            var FirstName = $("#FirstName").val();
            var LastName = $("#LastName").val();
            var MotherName = $("#MotherName").val();
            var FatherName = $("#FatherName").val();
            var Gender = $("#Gender").val();
            var DateOfBirth = $("#DateOfBirth").val();
            var Email = $("#Email").val();
            var Phone = $("#Phone").val();
            var Role = $("#Role").val();
            var Department = $("#Department").val();
            var Designation = $("#Designation").val();
            var StartedOn = $("#StartedOn").val();
            var EndedOn = $("#EndedOn").val();
            var ResignedOn = $("#ResignedOn").val();
            var LastWorkingDay = $("#LastWorkingDay").val();
            var OfferRelesedOn = $("#OfferRelesedOn").val();
            var OfferAcceptedOn = $("#OfferAcceptedOn").val();
            var OfferPrice = $("#OfferPrice").val();
            var CurrentPrice = $("#CurrentPrice").val();
            var JoiningBonus = $("#JoiningBonus").val();
            var PAN = $("#PAN").val();
            var Adhar = $("#Adhar").val();
            var BankAccount = $("#BankAccount").val();
            var BankName = $("#BankName").val();
            var IFSC = $("#IFSC").val();
            var UAN = $("#UAN").val();
            var PFNO = $("#PFNO").val();
            var employeeDetail = {
                EmployeeId: 0,
                EmployeeCode: Code,
                FirstName: FirstName,
                LastName: LastName,
                FatherName: FatherName,
                MotherName: MotherName,
                Gender: Gender,
                DateOfBirth: DateOfBirth ? new Date(DateOfBirth) : null,
                Email: Email,
                Phone: Phone,
                UserId: 0,
                RoleId: parseInt(Role),
                DepartmentId: parseInt(Department),
                DesignationId: parseInt(Designation),
                StartedOn: StartedOn ? new Date(StartedOn) : null,
                EndedOn: EndedOn ? new Date(EndedOn) : null,
                ResignedOn: ResignedOn ? new Date(ResignedOn) : null,
                LastWorkingDay: LastWorkingDay ? new Date(LastWorkingDay) : null,
                OfferRelesedOn: OfferRelesedOn ? new Date(OfferRelesedOn) : null,
                OfferAcceptedOn: OfferAcceptedOn ? new Date(OfferAcceptedOn) : null,
                OfferPrice: OfferPrice ? parseFloat(OfferPrice) : null,
                CurrentPrice: CurrentPrice ? parseFloat(CurrentPrice) : null,
                JoiningBonus: JoiningBonus ? parseFloat(JoiningBonus) : null,
                CreatedOn: new Date(),
                CreatedBy: 1,
                ModifiedOn: new Date(),
                ModifiedBy: 1,
                IsActive: true
            };

            var addEditEmployee = {
                employee: employeeDetail,
                employeeEducations: self.employeeEducations,
                employeeEmployments: self.employeeEmployments,
                employeeAddresses: self.employeeAddresses,
                employeeEmergencyContacts: self.employeeEmergencyContacts,
                PAN: PAN,
                Adhar: Adhar,
                BankAccount: BankAccount,
                BankName: BankName,
                IFSC: IFSC,
                UAN: UAN,
                PFNO: PFNO
            };

            $.ajax({
                url: '/Employee/InsertOrUpdateEmployee',
                data: JSON.stringify(addEditEmployee),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    if (response) {
                        $('#AddEditEmployeeDetailsModal').modal('hide');
                        self.resetMasterForm();
                    }

                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });

        });
        $('#toggleDrawer').click(function () {
            $('#AddEditEmployeeDetailsModal').modal('show');
        });

        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
        });

        $('#DateOfBirth,#OfferAcceptedOn,#OfferRelesedOn,#LastWorkingDay,#ResignedOn,#EndedOn,#StartedOn,#YearOfCompletion,#EmpStartedOn,#EmpEndedOn').datepicker({
            autoclose: true,
            endDate: 'today',
            orientation: 'bottom'
        });


        $(document).on("change", "#CountryId", function () {
            var selectedCountry = $(this).val();
            self.loadStatesBasedOnCountry(selectedCountry);
            ///clear cities when country changes
            var $dropdown = $('#CityId');
            $dropdown.empty();

            var $defaultOption = $('<option>', {
                value: '',
                text: 'Select an City'
            });
            $dropdown.append($defaultOption);
        });
        $(document).on("change", "#StateId", function () {
            var selectedState = $(this).val();
            self.loadCitiesByStates(selectedState);
        });

        $(document).on("click", ".fa-user-circle-o", function () {
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = employeeGrid.row(row).data();
            self.userAccess = dataItem;
            $("#userAccessFirstName").val(dataItem.FirstName);
            $("#userAccessLastName").val(dataItem.LastName);
            $("#userAccessEmail").val(dataItem.Email);
            $("#userAccessPhone").val(dataItem.Phone);
            $('#userAccessFirstName').prop('disabled', true);
            $("#userAccessLastName").prop('disabled', true);
            $("#CreateUserAccessModal").modal("show");
        });
        $(document).on("click", ".money-icon", function () {
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = employeeGrid.row(row).data();
            self.salaryHikeItem = dataItem;
            console.log(self.salaryHikeItem)
            $('#OrignalSalary').val(self.salaryHikeItem.CurrentPrice);
            $('#OrignalSalary').prop('disabled', true);
            $("#OrignalSalary").prop('disabled', true);
            $("#SalaryHike").modal("show");
        });
        $(document).on("click", "#confirmProcessRequest", function () {
            var latestSalary = $("#LatestSalary").val();
            var salaryHike = {
                EmployeeId: self.salaryHikeItem.EmployeeId,
                OrignalSalary: self.salaryHikeItem.CurrentPrice,
                LatestSalary: parseFloat(latestSalary),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date()
            };
            $.ajax({
                url: '/Employee/EmployeeSalaryHike',
                type: 'POST',
                data: JSON.stringify(salaryHike),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (responce) {
                    $('#OrignalSalary').val("");
                    $('#LatestSalary').val("");
                    $('#ProcessTimesheetMessage').val("");
                    self.salaryHikeItem = {};
                    $("#SalaryHike").modal("hide");
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });
        $(document).on("click", "#createUserAccess", function () {
            var userAccess = {
                Id: 0,
                EmployeeId: self.userAccess.EmployeeId,
                FirstName: self.userAccess.FirstName,
                LastName: self.userAccess.LastName,
                Email: $("#userAccessEmail").val(),
                Phone: $("#userAccessPhone").val(),
                RoleId: self.userAccess.RoleId,
                DepartmentId: self.userAccess.DepartmentId,
                Password: 'Admin@2021'
            };

            $.ajax({
                url: '/Tenant/RegisterUser',
                type: 'POST',
                data: JSON.stringify(userAccess),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (responce) {
                    self.employees = [];
                    if (responce && responce.employees) {
                        responce.employees.forEach(function (item) {
                            self.employees.push(item.employee);
                        });
                    }

                    self.tenants = [];
                    self.tenants = responce.users;

                    employeeGrid.clear().rows.add(self.employees).draw();
                    employeeGrid.draw();

                    var employeeCode = generateNextCode(self.employees);
                    $("#Code").val(employeeCode);
                    $('#Code').prop('disabled', true);

                    self.userAccess = {};
                    $("#CreateUserAccessModal").modal("hide");
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });

        self.resetMasterForm = function () {
            $("#Code").val("");
            $("#FirstName").val("");
            $("#LastName").val("");
            $("#MotherName").val("");
            $("#FatherName").val("");
            $('#Gender').prop('selectedIndex', 0);
            $("#DateOfBirth").val("");
            $("#Email").val("");
            $("#Phone").val("");
            $('#Role').prop('selectedIndex', 0);
            $('#Department').prop('selectedIndex', 0);
            $('#Designation').prop('selectedIndex', 0);
            $("#StartedOn").val("");
            $("#EndedOn").val("");
            $("#ResignedOn").val("");
            $("#LastWorkingDay").val("");
            $("#OfferRelesedOn").val("");
            $("#OfferAcceptedOn").val("");
            $("#OfferPrice").val("");
            $("#CurrentPrice").val("");
            $("#JoiningBonus").val("");
            $("#PAN").val("");
            $("#Adhar").val("");
            $("#BankAccount").val("");
            $("#BankName").val("");
            $("#IFSC").val("");

            self.employeeEducations = [];
            educationGrid.clear().rows.add(self.employeeEducations).draw();
            educationGrid.draw();

            self.employeeEmployments = [];
            employmentGrid.clear().rows.add(self.employeeEmployments).draw();
            employmentGrid.draw();

            self.employeeAddresses = [];
            employeeAddressGrid.clear().rows.add(self.employeeAddresses).draw();
            employeeAddressGrid.draw();

            self.employeeEmergencyContacts = [];
            contactsGrid.clear().rows.add(self.employeeEmergencyContacts).draw();
            contactsGrid.draw();
        };
    };

    function getStatesBasedOnCountry(country) {

        var states = [];
        if (country) {
            states = (self.states) ? self.states.filter(x => x.CountryId == country) : [];
        }
        else {
            states = self.states;
        }
        return states;
    };
    function getCitiesBasedOnState(state) {
        var cities = [];
        if (state) {
            cities = (self.citis) ? self.citis.filter(x => x.StateId == state) : [];
        }
        else {
            cities = self.citis;
        }
        return cities;
    }
    self.loadRoleDropdown = function (response) {
        var $dropdown = $('#Role');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an Role'
        });
        $dropdown.append($defaultOption);

        response.forEach(function (item) {
            var $option = $('<option>', {
                value: item.Id,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    };
    self.loadDepartmentsDropdown = function (response) {
        var $dropdown = $('#Department');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an Department'
        });
        $dropdown.append($defaultOption);

        response.forEach(function (item) {
            var $option = $('<option>', {
                value: item.DepartmentId,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    };
    self.loadDesignationsDropdown = function (response) {
        var $dropdown = $('#Designation');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an Designation'
        });
        $dropdown.append($defaultOption);

        response.forEach(function (item) {
            var $option = $('<option>', {
                value: item.DesignationId,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    };
    self.loadCountriesDropdown = function (response) {
        var $dropdown = $('#CountryId');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an Country'
        });
        $dropdown.append($defaultOption);

        response.forEach(function (item) {
            var $option = $('<option>', {
                value: item.Id,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    };
    self.loadStatesBasedOnCountry = function (country) {
        var states = getStatesBasedOnCountry(country);
        var $dropdown = $('#StateId');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an State'
        });
        $dropdown.append($defaultOption);

        states.forEach(function (item) {
            var $option = $('<option>', {
                value: item.StateId,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    }
    self.loadCitiesByStates = function (state) {

        var cities = getCitiesBasedOnState(state);
        var $dropdown = $('#CityId');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an City'
        });
        $dropdown.append($defaultOption);

        cities.forEach(function (item) {
            var $option = $('<option>', {
                value: item.Id,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    };
}