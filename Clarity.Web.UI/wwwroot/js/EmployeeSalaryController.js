function EmployeeSalaryController() {
    var self = this;
    self.Employees = [];
    self.currentYear = new Date().getFullYear();
    self.startYear = self.currentYear - 20;
    self.currectSelectedEmployeeSalary = {};
    self.init = function () {
        var employees = storageService.get('employees');
        if (employees) {
            self.Employees = employees;
        }
        makeFormGeneric('#EmployeeSalaryForm', '#btnsubmit');

        var dropdownMonths = $("#SalaryMonth");
        var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        months.forEach(function (month) {
            dropdownMonths.append($('<option>', {
                value: month,
                text: month
            }));
        });

        var dropdownYears = $("#SalaryYear");
        for (var year = self.currentYear; year >= self.startYear; year--) {
            dropdownYears.append($('<option>', {
                value: year,
                text: year
            }));
        }
        var dropdownLocation = $("#LOCATION");

        var metroCities = [
            "Mumbai",
            "Delhi",
            "Kolkata",
            "Chennai",
            "Bengaluru",
            "Hyderabad",
            "Pune",
            "Ahmedabad"
        ];
        metroCities.forEach(function (location) {
            dropdownLocation.append($('<option>', {
                value: location,
                text: location
            }));
        });
        var employeeSalaryGrid = $('#EmployeeSalaryGrid').DataTable({
            ajax: {
                url: '/EmployeeSalary/FetchAllEmployeeSalaries',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                {
                    data: null,
                    render: function (data, type, row) {
                        var employeeCode = self.Employees.find(function (employee) {
                            return employee.EmployeeId === row.employeeSalary.EmployeeId;
                        });
                        if (employeeCode) {
                            return employeeCode.EmployeeCode + "-" + employeeCode.FirstName + " " + employeeCode.LastName;
                        }
                        return null;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {

                        return row.employeeSalary.SalaryMonth + "-" + row.employeeSalary.SalaryYear;

                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.employeeSalary.Earning_Montly_GROSSEARNINGS;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.employeeSalary.Deduction_Montly_GROSSSDeduction;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.employeeSalary.NETPAY;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.employeeSalary.STDDAYS;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.employeeSalary.WRKDAYS;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        // PDF icon
                        var pdfIcon = '<i class="fas fa-file-pdf-o icon-padding-right" style="font-size:25px;color:red;cursor:pointer;" data-id="' + row.EmployeeId + '"></i>';

                        // Edit icon
                        var editIcon = '<i class="fas fa-edit icon-padding-right" style="font-size:25px;color:blue;cursor:pointer;" data-id="' + row.employeeSalary.EmployeeSalaryId + '"></i>';

                        // Combine both icons
                        return pdfIcon + ' ' + editIcon;
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '500px'
        });

        $(document).on("click", ".fa-edit", function (event) {
            var data = $(this);
            var row = data.closest('tr');
            var _dataItem = employeeSalaryGrid.row(row).data();

            var dataItem = _dataItem.employeeSalary;

            self.currectSelectedEmployeeSalary = dataItem;

            $('#EmployeeSalaryId').val(dataItem.EmployeeSalaryId);
            $('#EmployeeId').val(dataItem.EmployeeId);
            $('#MonthlySalaryId').val(dataItem.MonthlySalaryId);
            $('#Title').val(dataItem.Title);
            $('#SalaryMonth').val(dataItem.SalaryMonth);
            $('#SalaryYear').val(dataItem.SalaryYear);
            $('#LOCATION').val(dataItem.LOCATION);
            $('#STDDAYS').val(dataItem.STDDAYS);
            $('#WRKDAYS').val(dataItem.WRKDAYS);
            $('#LOPDAYS').val(dataItem.LOPDAYS);
            $('#Earning_Monthly_Basic').val(dataItem.Earning_Monthly_Basic);
            $('#Earning_YTD_Basic').val(dataItem.Earning_YTD_Basic);
            $('#Earning_Montly_HRA').val(dataItem.Earning_Montly_HRA);
            $('#Earning_YTD_HRA').val(dataItem.Earning_YTD_HRA);
            $('#Earning_Montly_CONVEYANCE').val(dataItem.Earning_Montly_CONVEYANCE);
            $('#Earning_YTD_CONVEYANCE').val(dataItem.Earning_YTD_CONVEYANCE);
            $('#Earning_Montly_MEDICALALLOWANCE').val(dataItem.Earning_Montly_MEDICALALLOWANCE);
            $('#Earning_YTD_MEDICALALLOWANCE').val(dataItem.Earning_YTD_MEDICALALLOWANCE);
            $('#Earning_Montly_SPECIALALLOWANCE').val(dataItem.Earning_Montly_SPECIALALLOWANCE);
            $('#Earning_YTD_SPECIALALLOWANCE').val(dataItem.Earning_YTD_SPECIALALLOWANCE);
            $('#Earning_Montly_SPECIALBONUS').val(dataItem.Earning_Montly_SPECIALBONUS);
            $('#Earning_YTD_SPECIALBONUS').val(dataItem.Earning_YTD_SPECIALBONUS);
            $('#Earning_Montly_STATUTORYBONUS').val(dataItem.Earning_Montly_STATUTORYBONUS);
            $('#Earning_YTD_STATUTORYBONUS').val(dataItem.Earning_YTD_STATUTORYBONUS);
            $('#Earning_Montly_GROSSEARNINGS').val(dataItem.Earning_Montly_GROSSEARNINGS);
            $('#Earning_YTD_GROSSEARNINGS').val(dataItem.Earning_YTD_GROSSEARNINGS);
            $('#Earning_Montly_OTHERS').val(dataItem.Earning_Montly_OTHERS);
            $('#Earning_YTD_OTHERS').val(dataItem.Earning_YTD_OTHERS);
            $('#Deduction_Montly_PROFESSIONALTAX').val(dataItem.Deduction_Montly_PROFESSIONALTAX);
            $('#Deduction_YTD_PROFESSIONALTAX').val(dataItem.Deduction_YTD_PROFESSIONALTAX);
            $('#Deduction_Montly_ProvidentFund').val(dataItem.Deduction_Montly_ProvidentFund);
            $('#Deduction_YTD_ProvidentFund').val(dataItem.Deduction_YTD_ProvidentFund);
            $('#Deduction_Montly_GroupHealthInsurance').val(dataItem.Deduction_Montly_GroupHealthInsurance);
            $('#Deduction_YTD_GroupHealthInsurance').val(dataItem.Deduction_YTD_GroupHealthInsurance);
            $('#Deduction_Montly_OTHERS').val(dataItem.Deduction_Montly_OTHERS);
            $('#Deduction_YTD_OTHERS').val(dataItem.Deduction_YTD_OTHERS);
            $('#Deduction_Montly_GROSSSDeduction').val(dataItem.Deduction_Montly_GROSSSDeduction);
            $('#Deduction_YTD_GROSSSDeduction').val(dataItem.Deduction_YTD_GROSSSDeduction);
            $('#NETPAY').val(dataItem.NETPAY);
            $('#NETTRANSFER').val(dataItem.NETTRANSFER);
            $('#INWords').val(dataItem.INWords);
            console.log(dataItem);
            $('#EmployeeSalaryFormModel').modal('show');
        });

        $("#EmployeeSalaryForm").on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#EmployeeSalaryForm');
            var employeeSalary = addCommonProperties(formData);
            makeAjaxRequest({
                url: "/EmployeeSalary/InsertOrUpdateEmployeeSalary",
                data: employeeSalary,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        $('#EmployeeSalaryForm')[0].reset();
                        $('#EmployeeSalaryFormModel').modal('hide');
                        $('#drawer').removeClass('show');
                        employeeSalaryGrid.ajax.reload();
                        self.currectSelectedEmployeeSalary = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });

        });
        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
            $('#EmployeeSalaryFormModel').modal('hide');
            self.currectSelectedEmployeeSalary = {};
        });

        $(document).on("click", ".fa-file-pdf-o", function (event) {
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = employeeSalaryGrid.row(row).data();
            console.log(dataItem);
            $.ajax({
                url: '/EmployeeSalary/DownloadPaySlip',
                type: 'GET',
                data: { employeeSalaryId: JSON.stringify(dataItem.employeeSalary.EmployeeSalaryId) },
                success: function (data, status, xhr) {
                    console.log(status);
                    window.location.href = "/EmployeeSalary/DownloadPaySlip?employeeSalaryId=" + dataItem.employeeSalary.EmployeeSalaryId;
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });
    };
}