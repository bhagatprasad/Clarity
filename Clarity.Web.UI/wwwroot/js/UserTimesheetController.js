function UserTimesheetController() {
    var self = this;
    self.ApplicationUser = {};
    var requests = [];
    var actions = [];
    self.Employees = [];
    self.CoreTakItems = [];
    self.CoreTaskItems = [];
    self.TaskCodes = [];
    self.TaskGridData = [];
    self.currentStatus = "";
    self.currentTimesheet = {};

    actions.push(serviceUrls.fetchAllTaskItemUser);
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        var urlWithUserId = '/UserTimesheet/fetchAllUserTimesheets?userId=' + appuser.Id;
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
            //if (responses[0][0] && responses[0][0].data) {
            //    responses[0][0].data.forEach(function (item) {
            //        self.Employees.push(item.employee);
            //    });
            //}
            self.CoreTaskItems = responses[0] && responses[0].data ? responses[0].data : [];
            bindTaskItems();
          //  console.log(self.CoreTaskItems);
        }).fail(function () {
          //  console.log('One or more requests failed.');
        });

        var timesheetGrid = $('#TimesheetGrid').DataTable({
            ajax: {
                url: urlWithUserId,
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
                        var icons = '';
                        if (parseInt(self.ApplicationUser.Id) === row.UserId && row.Status != "Approved" && row.Status != "Rejected" && row.Status != "Cancelled") {
                            icons += '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.Id + '" style="font-size: 20px;color: red;" title="Cancel/Delete Timesheet"></i>';
                        }
                        else {
                            if (row.Status != "Approved" && row.Status != "Rejected" && row.Status != "Cancelled") {
                                icons += '<i class="fas fa-check-circle approve-icon  icon-padding-right" data-id="' + row.Id + '" style="font-size: 20px;color: green;" title="Appove Timesheet"></i>' +
                                    '<i class="fas  fa-times-circle-o reject-icon icon-padding-right" data-id="' + row.Id + '" style="font-size: 20px; color: Red;padding-left: 5px;" title="Reject Timesheet"></i>';
                            }
                        }
                        return icons;
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

        $('#AddEditTimesheetModal,#AddTaskModal,#confirmModal').modal({ backdrop: 'static', keyboard: false });
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
                MondayHours: MondayHours ? parseInt(MondayHours) : 0,
                TuesdayHours: TuesdayHours ? parseInt(TuesdayHours) : 0,
                WednesdayHours: WednesdayHours ? parseInt(WednesdayHours) : 0,
                ThursdayHours: ThursdayHours ? parseInt(ThursdayHours) : 0,
                FridayHours: FridayHours ? parseInt(FridayHours) : 0,
                SaturdayHours: SaturdayHours ? parseInt(SaturdayHours) : 0,
                SundayHours: SundayHours ? parseInt(SundayHours) : 0,
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
        $(document).on("click", "#SaveInsertOrUpdateTimesheet", function () {
            var description = $("#Description").val();
            var fromDate = $("#FromDate").val();
            var toDate = $("#ToDate").val();
            var timesheetId = $("#Id").val();

            var timesheet = {
                Id: timesheetId ? parseInt(timesheetId) : 0,
                FromDate: new Date(fromDate),
                ToDate: new Date(toDate),
                Description: description,
                EmployeeId: 0,
                UserId: self.ApplicationUser.Id,
                Status: timesheetId ? self.currentStatus : "Submitted",
                AssignedOn: null,
                AssignedTo: null,
                ApprovedOn: null,
                ApprovedBy: null,
                ApprovedComments: null,
                CancelledOn: null,
                CancelledBy: null,
                CancelledComments: null,
                RejectedOn: null,
                RejectedBy: null,
                RejectedComments: null,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true,
                timesheetTasks: self.TaskGridData
            };
            $.ajax({
                url: '/UserTimesheet/InsertOrUpdateTimesheet',
                data: JSON.stringify(timesheet),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#AddEditTimesheetModal').modal('hide');
                    $("#AddEditTimesheetForm")[0].reset();
                    timesheetGrid.ajax.reload();
                    self.TaskGridData = [];
                    taskGrid.clear().rows.add(self.TaskGridData).draw();
                    taskGrid.draw();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                   // console.error(error);
                }
            });
        });

        $(document).on("click", ".approve-icon", function () {
            self.currentStatus = "Approved";
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = timesheetGrid.row(row).data();
           // console.log(dataItem);
            self.currentTimesheet = dataItem;
            $('#confirmModal').modal('show');
        });

        $(document).on("click", "#confirmProcessRequest", function () {
            var message = $("#ProcessTimesheetMessage").text();
            var processTimesheet = {
                TimesheetId: parseInt(self.currentTimesheet.Id),
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                Comment: message,
                ChangeType: self.currentStatus,
                Status: self.currentStatus
            };
            $.ajax({
                url: '/UserTimesheet/TimesheetStatusChangeProcess',
                data: JSON.stringify(processTimesheet),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#confirmModal').modal('hide');
                    $("#ProcessTimesheetMessage").text("")
                    timesheetGrid.ajax.reload();
                    self.currentTimesheet = {};
                    self.currentStatus = "";
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });

        });
       

        $(document).on("click", ".reject-icon", function () {
            self.currentStatus = "Rejected";
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = timesheetGrid.row(row).data();
            self.currentTimesheet = dataItem;
            $('#confirmModal').modal('show');
        });
        $(document).on("click", ".delete-icon", function () {
            self.currentStatus = "Cancelled";
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = timesheetGrid.row(row).data();
            self.currentTimesheet = dataItem;
            $('#confirmModal').modal('show');
        });
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