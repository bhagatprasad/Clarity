function TimesheetController() {
    var self = this;
    self.ApplicationUser = {};
    var requests = [];
    var actions = [];
    self.Employees = [];
    self.CoreTakItems = [];
    self.CoreTaskItems = [];
    self.TaskCodes = [];
    self.TaskGridData = [];
    actions.push(serviceUrls.getUsers);
    actions.push(serviceUrls.fetchAllTaskItems);
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        console.log(33,self.ApplicationUser)

        var form = $('#AddEditTimesheetForm');
        var signUpButton = $('#SaveInsertOrUpdateTimesheet');
        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();

        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
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
            self.CoreTaskItems = responses[1][0] && responses[1][0].data ? responses[1][0].data : [];
            bindTaskItems();
            console.log(self.CoreTaskItems);
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        var timesheetGrid = $('#TimesheetGrid').DataTable({
            ajax: {
                url: '/Timesheet/fetchAllTimesheets',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'EmployeeId' },
                {
                    data: 'FromDate',
                    render: function (data, type, row) {
                        if (!data) return "";

                        var dateObject = new Date(data);
                        var options = { weekday: 'long', day: '2-digit', month: 'long', year: 'numeric' };

                        var formattedDate = dateObject.toLocaleDateString('en-US', options);

                        return formattedDate;
                    }
                },
                {
                    data: 'ToDate',
                    render: function (data, type, row) {
                        if (!data) return ""; // Handle case where data is null or undefined

                        var dateObject = new Date(data);
                        var options = { weekday: 'long', day: '2-digit', month: 'long', year: 'numeric' };

                        // Format the date using the toLocaleDateString method
                        var formattedDate = dateObject.toLocaleDateString('en-US', options);

                        return formattedDate;
                    }
                },
                { data: 'Description' },
                { data: 'Status' },

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

        var taskGrid = $('#TaskGrid').DataTable({
            data: self.TaskGridData,
            columns: [
                { data: 'TaskItemId' },
                { data: 'MondayHours' },
                { data: 'TuesdayHours' },
                { data: 'WednesdayHours' },
                { data: 'ThursdayHours' },
                { data: 'FridayHours' },
                { data: 'SaturdayHours' },
                { data: 'SundayHours' },
                {
                    data: null,
                    render: function (data, type, row) {

                        function sumHours() {
                            var totalHours = 0;
                            totalHours += parseInt(row.MondayHours) || 0;
                            totalHours += parseInt(row.TuesdayHours) || 0;
                            totalHours += parseInt(row.WednesdayHours) || 0;
                            totalHours += parseInt(row.ThursdayHours) || 0;
                            totalHours += parseInt(row.FridayHours) || 0;
                            totalHours += parseInt(row.SaturdayHours) || 0;
                            totalHours += parseInt(row.SundayHours) || 0;
                            return totalHours;
                        }

                        var totalHrs = sumHours();

                        return totalHrs;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-trash icon-padding-right"  style="font-size:25px;color:red" data-id="' + row.Id + '" ></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '100px'
        });

        $('#AddEditTimesheetModal,#AddTaskModal').modal({ backdrop: 'static', keyboard: false });
        $('#toggleDrawer').click(function () {
            $('#AddEditTimesheetModal').modal('show');
        });
        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
        });
        $('#FromDate,#ToDate').datepicker({
            autoclose: true,
            orientation: 'bottom'
        });

        $('#dropdownTaskItems').change(function () {
            var selectedTaskItemId = $(this).val();
            bindTaskCodes(selectedTaskItemId);
        });
        $('#addTaskItemToGrid').click(function () {
            $('#AddTaskModal').modal('show');
        });

        //Add to grid

        $(document).on("click", "#SaveTaskToGrid", function () {
            var TaskItemId = $("#dropdownTaskItems").val();
            var TaskCodeId = $("#dropdownTaskCodes").val();
            var MondayHours = $("#MondayHours").val();
            var TuesdayHours = $("#TuesdayHours").val();
            var WednesdayHours = $("#WednesdayHours").val();
            var ThursdayHours = $("#ThursdayHours").val();
            var FridayHours = $("#FridayHours").val();
            var SaturdayHours = $("#SaturdayHours").val();
            var SundayHours = $("#SundayHours").val();

            var timesheetTask = {
                Id: 0,
                TimesheetId: 0,
                TaskItemId: parseInt(TaskItemId),
                TaskCodeId: parseInt(TaskCodeId),
                MondayHours: parseInt(MondayHours),
                TuesdayHours: parseInt(TuesdayHours),
                WednesdayHours: parseInt(WednesdayHours),
                ThursdayHours: parseInt(ThursdayHours),
                FridayHours: parseInt(FridayHours),
                SaturdayHours: parseInt(SaturdayHours),
                SundayHours: parseInt(SundayHours),
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true
            };
            self.TaskGridData.push(timesheetTask);
            taskGrid.clear().rows.add(self.TaskGridData).draw();
            taskGrid.draw();
            $("#AddTaskForm")[0].reset();
            $('#AddTaskModal').modal('hide');
        });
        //raju start............

        $(document).on("click", "#SaveInsertOrUpdateTimesheet", function () {
            var fromDate = $("#FromDate").val();
            var toDate = $("#ToDate").val();
            var description = $("#Description").val();
            var id = $("#Id").val();

            var timesheet = {
                Id: id ? parseInt(id) : 0,
                FromDate: new Date(fromDate),
                ToDate: new Date (toDate),
                Description: description,
                Status: null,
                EmployeeId: null,
                UserId: self.ApplicationUser.Id,
                ApprovedOn: new Date(),
                ApprovedBy: null,
                ApprovedComments: null,
                CancelledOn: new Date(),
                CancelledBy: null,
                CancelledComments: null,
                RejectedOn: null,
                RejectedBy: null,
                RejectedComments:null,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: true,
                timesheetTasks: self.TaskGridData || []
            };

            var dataToSend = {
                timesheet: timesheet
            };

            console.log(1234567899, dataToSend);

            $.ajax({
                url: '/Timesheet/SaveInsertOrUpdateTimesheet',
                data: JSON.stringify(dataToSend),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('#AddEditTimesheetModal').modal('hide');
                    self.clearInputs();
                    timesheetGrid.ajax.reload();
                }
            });
        });


          //raju end........
    };
    self.clearInputs = function () {
        $("#FromDate").val("");
        $("#ToDate").val("");
        $("#Description").val("");
        $("#Status").val("");
        $("#Id").val("");
    };
    function bindTaskItems() {
        var taskItemDropdown = $('#dropdownTaskItems');
        taskItemDropdown.empty();
        taskItemDropdown.append($('<option>', {
            value: '',
            text: 'Select Task Item'
        }));
        self.CoreTaskItems.forEach(function (taskItem) {
            taskItemDropdown.append($('<option>', {
                value: taskItem.TaskItemId,
                text: taskItem.Name
            }));
        });
        taskItemDropdown.trigger('change');
    }

    function bindTaskCodes(taskItemId) {
        var taskCodeDropdown = $('#dropdownTaskCodes');
        taskCodeDropdown.empty();
        taskCodeDropdown.append($('<option>', {
            value: '',
            text: 'Select Task Code'
        }));
        var selectedTaskItem = self.CoreTaskItems.find(function (taskItem) {
            return taskItem.TaskItemId == taskItemId;
        });
        if (selectedTaskItem) {
            selectedTaskItem.taskCodes.forEach(function (taskCode) {
                taskCodeDropdown.append($('<option>', {
                    value: taskCode.TaskCodeId,
                    text: taskCode.Name
                }));
            });
        }
    }

}