function EmployeePaymentController() {
    var self = this;
    var requests = [];
    var actions = [];
    self.EmployeNames = []; //getUsers or employee names
    self.PaymentTypes = [];
    self.PaymentMethods = [];
    self.ApplicationUser = {};    
    actions.push(serviceUrls.fetchPaymentMethods);
    actions.push(serviceUrls.fetchPaymentTypes);
    actions.push(serviceUrls.getUsers);
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);
            self.PaymentMethods = responses[0][0] && responses[0][0].data ? responses[0][0].data : [];
            self.PaymentTypes = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];

            if (responses[2][0] && responses[2][0].data) {
                responses[2][0].data.forEach(function (item) {
                    self.EmployeNames.push(item.employee);
                });
            }
           /* self.EmployeNames = responses[2][0] && responses[2][0].data ? responses[2][0].data : [];*/
            self.bindPaymentTypes();
            self.bindPaymentMethods();
            self.bindEmployeNames();
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        //-------------------------GridBidnging--------------------//
        var employeePaymentGrid = $('#EmployeePayment').DataTable({
            ajax: {
                url: '/EmployeePayment/GetAllEmployeePayments',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                {
                    data: 'EmployeeFullName',
                    render: function (data, type, row)
                    {
                        return row.EmployeeFullName;
                    }
                    
                },
                {
                    data: 'PaymentMethodName',
                    render: function (data, type, row)
                    {
                        return row.PaymentMethodName;
                    }
                },
                {
                    data: 'PaymentTypeName',
                    render: function (data, type, row)
                    {
                        return row.PaymentTypeName;
                    }
                },
                {
                    data: 'Amount',
                    render: function (data, type, row) {
                        return row.Amount;
                    }
                },
                {
                    data: 'PaymentMessage',
                    render: function (data, type, row)
                    {
                        return row.PaymentMessage;
                    }
                },
                {
                    data: 'CreatedOn',
                    render: function (data, type, row)
                    {
                        return row.CreatedOn;
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '500px'
        });

        $(document).on("click", "#addPayment", function () {
            $("#empoyeePaymentModel").modal("show");
        });

        // save Grid 
      
        $(document).on("click", "#SaveEmployeePaymentsToGrid", function () {
            var employeeId = $("#dropdownEmployee").val();
            var employeeName = $("dropdownEmployee").val();
            var paymentTypeId = $("#dropdownPaymentTypes").val();
            var paymentMethodId = $("#dropdownPaymentMethods").val();
            var amount = $("#Amount").val();
            var paymentMessage = $("#PaymentMessage").val();


            var employePayment = {
                Id: 0,
                EmployeeId: parseInt(employeeId),
                PaymentMethodId: parseInt(paymentMethodId),
                PaymentTypeId: parseInt(paymentTypeId),
                PaymentMessage: paymentMessage,
                employeeName: dropdownEmployee,
                Amount: amount,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true,
            };
            $.ajax({
                url: '/EmployeePayment/InsertEmployeePayments',
                data: JSON.stringify(employePayment),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#empoyeePaymentModel').modal('hide');
                    $("#EmployeePaymentForm")[0].reset();
                    employeePaymentGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });
        //end
    };
    self.bindPaymentTypes = function () {
        var paymentTypeDropdown = $('#dropdownPaymentTypes');

        paymentTypeDropdown.empty();
        paymentTypeDropdown.append($('<option>', {
            value: '',
            text: 'Select Payment Type'
        }));
        if (self.PaymentTypes.length > 0) {
            $.each(self.PaymentTypes, (function (index, paymentType) {
                paymentTypeDropdown.append($('<option>', {
                    value: paymentType.Id,
                    text: paymentType.Name
                }));
            }));
            paymentTypeDropdown.trigger('change');
        }


    }
    self.bindPaymentMethods = function () {
        var paymentMethodsDropdown = $('#dropdownPaymentMethods');

        paymentMethodsDropdown.empty();
        paymentMethodsDropdown.append($('<option>', {
            value: '',
            text: 'Select Payment Method'
        }));
        if (self.PaymentMethods.length > 0) {
            $.each(self.PaymentMethods, (function (index, paymenMethod) {
                paymentMethodsDropdown.append($('<option>', {
                    value: paymenMethod.Id,
                    text: paymenMethod.Name
                }));
            }));
            paymentMethodsDropdown.trigger('change');
        }

    }
    //employeeName
    self.bindEmployeNames = function () {
       
        var employeeNameDropdown = $('#dropdownEmployee');

        employeeNameDropdown.empty();
        employeeNameDropdown.append($('<option>', {
            value: '',
            text: 'Select Employee Name'
        }));
        if (self.EmployeNames.length > 0) {
            $.each(self.EmployeNames, (function (index, employee) {
                employeeNameDropdown.append($('<option>', {
                    value: employee.EmployeeId,
                    text: employee.FirstName + " " + employee.LastName + " (" + employee.EmployeeCode + "-" + employee.Email + " )"
                }));
            }));
            employeeNameDropdown.trigger('change');
        }

    }
}