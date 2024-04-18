function HolidayCallenderController() {
    var self = this;
    self.ApplicationUser = {};
    self.currentYear = new Date().getFullYear();
    self.startYear = self.currentYear - 20;
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $('#HolidayDate').datepicker({
            autoclose: true,
            orientation: 'bottom'
        });
        var form = $('#HolidayCallenderForm');
        var signUpButton = $('#addEditCallender');
        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();

        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
        var holidayCallenderGrid = $('#HolidayCallenderGrid').DataTable({
            ajax: {
                url: '/HolidayCallender/fetchAllHolidayCallenders',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'FestivalName' },
                {
                    data: 'HolidayDate',
                    render: function (data, type, row) {
                        var dateObject = new Date(data);
                        var monthNames = ["January", "February", "March", "April", "May", "June",
                            "July", "August", "September", "October", "November", "December"];
                        var dayName = dateObject.toLocaleDateString('en-US', { weekday: 'long' });

                        var monthName = monthNames[dateObject.getMonth()];
                        var year = dateObject.getFullYear();

                        var formattedDate = dayName + ' ' + monthName + ', ' + year;

                        return formattedDate;
                    }
                },
                { data: 'Year' },
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
        $('#HolidayCallenderModal').modal({ backdrop: 'static', keyboard: false });
        $('#toggleDrawer').click(function () {
            $('#HolidayCallenderModal').modal('show');
        });
        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
        });
        $(document).on("click", "#addEditCallender", function () {
            var fetsivalName = $("#FestivalName").val();
            var festDate = $("#HolidayDate").val();
            if (!festDate) {
                $("#HolidayDate").focus();
                return false;
            }
            var festivalDate = new Date(festDate);
            var callenderId = $("#Id").val();
            var holidayCallender = {
                Id: callenderId ? callenderId : 0,
                FestivalName: fetsivalName,
                HolidayDate: new Date(festivalDate),
                Year: festivalDate ? festivalDate.getFullYear() : new Date().getFullYear(),
                CreatedOn: new Date(),
                CreatedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: true
            };
            console.log(holidayCallender);
            $.ajax({
                url: '/HolidayCallender/InsertOrUpdateHolyDay',
                data: JSON.stringify(holidayCallender),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#HolidayCallenderModal').modal('hide');
                    $("#HolidayCallenderForm")[0].reset();
                    holidayCallenderGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });
    };
}