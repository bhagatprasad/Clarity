function TutionFeeController() {
    var self = this;
    self.EmployeNames = [];
    var requests = [];
    var actions = [];
    self.TutionFees = [];
    self.ApplicationUser = [];
    actions.push(serviceUrls.getUsers);

    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        //Grid Binding
        self.tutionFeeGrid = $('#TutionFeeGrid').DataTable({
            ajax: {
                url: '/TutionFee/fetchAllTutionFees',
                type: 'GET',
                dataSrc: 'data'
            },
           
            columns: [
                {
                    data: 'EmployeeFullName',
                    render: function (data, type, row) {
                        return row.EmployeeFullName;
                    }
                },
                {
                    data: 'ActualFee',
                    render: function (data, type, row) {
                        return row.ActualFee;
                    }
                },
                {
                    data: 'FinalFee',
                    render: function (data, type, row) {
                        return row.FinalFee;
                    }
                },
                {
                    data: null, 
                    render: function (data, type, row) {
                        return row.FinalFee - row.PaidFee;
                    },
                    title: 'RemainingFee' 
                },
                {
                    data: 'PaidFee',
                    render: function (data, type, row) {
                        return row.PaidFee;
                    }
                },
                {
                    data: 'CreatedOn',
                    render: function (data, type, row) {
                        return row.CreatedOn;
                    }
                }
            ]
        })
        setTimeout(function () {
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
                if (responses[0] && responses[0].data) {
                    responses[0].data.forEach(function (item) {
                        self.EmployeNames.push(item.employee);
                    });
                }
                console.log(self.EmployeNames);
                self.bindEmployeNames();
            }).fail(function () {
                console.log('One or more requests failed.');
            });
        }, 1000);  

        $(document).on("click", "#addTution", function () {
            $("#tutionFeeModel").modal("show");
        });
    };
    //Save Grid button start
    $(document).on("click", "#SaveTutionFeeToGrid", function ()
        {
        var employeeId = $('#dropdownEmployee').val();
        var employeeName = $("#dropdownEmployee").val();
        var tutionFee = $("#TutionFee").val();
        var actualFee = $("#ActualFee").val();
        var finalFee = $("#FinalFee").val();
        var paidFee = $("#PaidFee").val();
        var CreatedOn = $("#CreatedOn").val();

        var tutionFees = {           
            EmployeeId: parseInt(employeeId),
            employeeName: dropdownEmployee,
            TutionFee: tutionFee,
            ActualFee: actualFee,
            FinalFee: finalFee,
            PaidFee: paidFee,
            CreatedBy: self.ApplicationUser.Id,
            CreatedOn: new Date(),
            ModifiedBy: self.ApplicationUser.Id,
            ModifiedOn: new Date(),
            IsActive: true,

        };
        $.ajax({
            url: '/TutionFee/InsertOrUpdateTutionFee',
            data: JSON.stringify(tutionFees),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: true,
            cache: false,
            success: function (responce) {
                $('#tutionFeeModel').modal('hide');
                $("#TutionFeeForm")[0].reset();
                self.tutionFeeGrid.ajax.reload();
                $(".se-pre-con").hide();
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
        });
    //end Grid save
    //Employee Name dropdown Binding
    self.bindEmployeNames = function () {

        var employeeNameDropdown = $('#dropdownEmployee');

        employeeNameDropdown.empty();
        employeeNameDropdown.append($('<option>', {
            value: '',
            text: 'Select Employee Name'
        }));
        if (self.EmployeNames.length > 0) {
            var otherEmployeeIds = $.map(self.tutionFeeGrid.data(), function (item) {
                return item.EmployeeId;
            });

            var nonMatchingRecords = $.grep(self.EmployeNames, function (item) {
                return $.inArray(item.EmployeeId, otherEmployeeIds) === -1;
            });
            $.each(nonMatchingRecords, (function (index, employee) {
                employeeNameDropdown.append($('<option>', {
                    value: employee.EmployeeId,
                    text: employee.FirstName + " " + employee.LastName + " (" + employee.EmployeeCode + "-" + employee.Email + " )"
                }));
            }));
            employeeNameDropdown.trigger('change');
        }

    }
};