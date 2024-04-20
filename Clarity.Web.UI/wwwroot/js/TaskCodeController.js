function TaskCodeController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        var appuser = storageService.get('ApplicationUser');
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $('#addEditTaskCodeModal').modal({ backdrop: 'static', keyboard: false });
        var taskCodeGrid = $("#TaskCodeGrid").DataTable({
            responsive: false,
            serverSide: false,
            ajax: {
                url: '/TaskCode/LoadTaskCodes',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'TaskCodeId' },
                { data: 'Name' },
                { data: 'Code' },
                { data: 'CreatedOn' },
                { data: 'ModifiedOn' },
                { data: 'IsActive' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa-edit edit-icon  icon-padding-right" data-id="' + row.TaskCodeId + '"></i>' +
                            '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.TaskCodeId + '"></i>' +
                            '<i class="fas fa-eye eye-icon" data-id="' + row.TaskCodeId + '"></i>';
                    }
                }
            ],
            "order": [[0, "asc"]],
            "pageLength": 15
        });
        $(document).on("click", "#addTaskCode", function () {
            $("#addEditTaskCodeModalLabel").text("Add TaskCode");
            $('#addEditTaskCodeModal').modal('show');
        });

        $('#TaskCodeGrid').on('click', '.edit-icon', function () {
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = taskCodeGrid.row(row).data();
            self.isEdit = true;
            $('#Name').val(dataItem.Name);
            $('#Code').val(dataItem.Code);
            $("#TaskCodeId").val(dataItem.TaskCodeId);
            $('#addEditTaskCodeModal').modal('show');
            $("#addEditTaskCodeModalLabel").text("Edit TaskCode");
        });

        $(document).on("click", "#AddEditTaskCode", function () {
            var name = $("#Name").val();
            var code = $("#Code").val();
            var taskCodeId = $("#TaskCodeId").val();
            var task = {
                TaskCodeId: taskCodeId ? parseInt(taskCodeId) : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: true
            };

            $.ajax({
                url: '/TaskCode/AddEditTaskCode',
                data: JSON.stringify(task),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#addEditTaskCodeModal').modal('hide');
                    self.clearInputs();
                    taskCodeGrid.ajax.reload();
                }
            });
        });
    };
    self.clearInputs = function () {
        $("#Name").val("");
        $("#Code").val("");
        $("#TaskCodeId").val("");
    };
} 
