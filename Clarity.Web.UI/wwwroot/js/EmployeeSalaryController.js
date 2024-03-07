function EmployeeSalaryController() {
    var self = this;
    self.init = function () {
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
                        return row.employee.EmployeeCode + "-" + row.employee.FirstName + " " + row.employee.LastName;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return row.employeeSalary.Title;
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
                        return row.employeeSalary.LOCATION;
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
                        return row.employeeSalary.LOPDAYS;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-file-pdf-o icon-padding-right"  style="font-size:25px;color:red" data-id="' + row.EmployeeId + '" ></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            "order": [[0, "asc"]],
            paging: false,
            scrollCollapse: true,
            scrollY: '500px'
        });
    };
}