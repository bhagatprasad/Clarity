function TaskItemController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        var appuser = storageService.get('ApplicationUser');
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $('#addEditTaskItemModal').modal({ backdrop: 'static', keyboard: false });
        var taskItemsGrid = $("#TaskItemsGrid").DataTable({
            responsive: false,
            serverSide: false,
            ajax: {
                url: '/TaskItem/LoadTaskItems',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'TaskItemId' },
                { data: 'Name' },
                { data: 'Code' },
                { data: 'CreatedOn' },
                { data: 'ModifiedOn' },
                { data: 'IsActive' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa-edit edit-icon  icon-padding-right" data-id="' + row.TaskItemId + '"></i>' +
                            '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.TaskItemId + '"></i>' +
                            '<i class="fas fa-eye eye-icon" data-id="' + row.TaskItemId + '"></i>';
                    }
                }
            ],
            "order": [[0, "asc"]],
            "pageLength": 15
        });
        $(document).on("click", "#addTaskItem", function () {
            $("#addEditTaskItemModalLabel").text("Add TaskItem");
            $('#addEditTaskItemModal').modal('show');
        });

        $('#TaskItemsGrid').on('click', '.edit-icon', function () {
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = taskItemsGrid.row(row).data();
            self.isEdit = true;
            $('#Name').val(dataItem.Name);
            $('#Code').val(dataItem.Code);
            $("#TaskItemId").val(dataItem.TaskItemId);
            $('#addEditTaskItemModal').modal('show');
            $("#addEditTaskItemModalLabel").text("Edit TaskItem");
        });

        $(document).on("click", "#AddEditTaskItem", function () {
            var name = $("#Name").val();
            var code = $("#Code").val();
            var taskItemId = $("#TaskItemId").val();
            var task = {
                TaskItemId: taskItemId ? parseInt(taskItemId) : 0,
                Name: name,
                Code: code,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: true
            };

            $.ajax({
                url: '/TaskItem/AddEditTaskItem',
                data: JSON.stringify(task),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#addEditTaskItemModal').modal('hide');
                    self.clearInputs();
                    taskItemsGrid.ajax.reload();
                }
            });
        });
    };
    self.clearInputs = function () {
        $("#Name").val("");
        $("#Code").val("");
        $("#TaskItemId").val("");
    };
} 
