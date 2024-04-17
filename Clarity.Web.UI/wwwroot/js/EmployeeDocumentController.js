function EmployeeDocumentController() {
    var self = this;
    self.ApplicationUser = {};
    self.DocumentTypes = [];
    self.Employees = [];
    var requests = [];
    var actions = [];
    actions.push(serviceUrls.getUsers);
    actions.push(serviceUrls.fetchDocumentTypes);
    self.init = function () {
        $('#fileUploadModal').modal({ backdrop: 'static', keyboard: false });
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
            if (responses[0][0] && responses[0][0].data) {
                responses[0][0].data.forEach(function (item) {
                    self.Employees.push(item.employee);
                });
            }
            self.loadEmployeesDropdown(self.Employees);
            self.DocumentTypes = responses && responses[1][0] ? responses[1][0].data : [];
            self.loadDocumentTypeDropdown(self.DocumentTypes);
            console.log(responses);
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        var form = $('#fileUploadForm');
        var signUpButton = $('#uploadButton');
        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();

        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
        var employeeDocumentsGrid = $('#EmployeeDocumentsGrid').DataTable({
            ajax: {
                url: '/EmployeeDocument/fetchAllEmployeeDocuments',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                {
                    data: null,
                    render: function (data, type, row) {
                        var employee = self.Employees.find(function (item) {
                            return item.EmployeeId === row.EmployeeId;
                        });
                        return employee ? (employee.EmployeeCode + "-" + employee.FirstName + " " + employee.LastName) : "";
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var documentType = self.DocumentTypes.find(function (item) {
                            return item.Id === row.DocumentTypeId;
                        });
                        return documentType ? documentType.Name : "";
                    }
                },
                {
                    data: 'DocumentName',
                    render: function (data, type, row) {
                        return '<span class="donwloadfile" style="cursor:pointer;color:blue;">' + data + '</span>';
                    }
                },
                {
                    data: 'CreatedOn',
                    render: function (data, type, row) {

                        var dateObject = new Date(data);

                        var formattedDate = dateObject.toLocaleDateString('en-GB');

                        return formattedDate;
                    }
                },
                {
                    data: 'ModifiedOn',
                    render: function (data, type, row) {
                        var dateObject = new Date(data);

                        var formattedDate = dateObject.toLocaleDateString('en-GB');

                        return formattedDate;
                    }
                },
                { data: 'IsActive' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-calendar icon-padding-right"  style="font-size:20px;color:green;padding-right:10px;" data-id="' + row.Id + '" ></i>' +
                            '<i class="fas fa fa-trash icon-padding-right"  style="font-size:20px;color:red" data-id="' + row.Id + '" ></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '500px'
        });

        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
        });
        $(document).on("click", "#btnImportClose", function () {
            $("#fileUploadModal").modal('hide');
        });
        $(document).on("click", "#toggleDrawer,#importData", function () {
            $("#fileUploadModal").modal('show');
        });
        $(document).on("click", "#uploadButton", function () {
            var employeeId = $("#dropdownEmployees").val();
            var documentType = $("#dropdownDocumentType").val();
            var fileInput = $("#fileInput")[0];
            var file = fileInput.files[0];
            var _formData = new FormData();
            if (file) {
                _formData.append('file', file);
            }
            console.log("file...data.." + JSON.stringify(file));
            var eDocument = {
                Id:0,
                EmployeeId: parseInt(employeeId),
                DocumentTypeId: parseFloat(documentType),
                DocumentName: file.name,
                DocumentExtension:file.type,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true
            };

            $.ajax({
                url: '/EmployeeDocument/UploadEmployeeDocument',
                type: 'POST',
                data: _formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response.status) {
                        eDocument.DocumentPath = response.documentPath;
                        console.log(response);
                        $.ajax({
                            url: '/EmployeeDocument/upsertEmployementDocument',
                            data: JSON.stringify(eDocument),
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            processData: true,
                            cache: false,
                            success: function (response) {
                                if (response) {
                                    $("#fileUploadModal").modal('hide');
                                    $("#fileUploadForm")[0].reset();
                                    employeeDocumentsGrid.ajax.reload();
                                }
                            },
                            error: function (xhr, status, error) {
                                console.error("error in upserting data to server" + error);
                            }
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.error("error in uploading file" + error);
                }
            });
        });

        $(document).on("click", ".donwloadfile", function (event) {
            console.log(event);
            event.preventDefault();
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = employeeDocumentsGrid.row(row).data();
            $.ajax({
                url: '/EmployeeDocument/DownloadFile',
                type: 'GET',
                data: { relativeFilePath: dataItem.DocumentPath },
                success: function (status) {
                    window.location.href = "/EmployeeDocument/DownloadFile?relativeFilePath=" + dataItem.DocumentPath;
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        });
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
        self.loadDocumentTypeDropdown = function (response) {
            var $dropdown = $('#dropdownDocumentType');
            $dropdown.empty();

            var $defaultOption = $('<option>', {
                value: '',
                text: 'Select an DocumentTyoe'
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
    };
}