function MonthlySalaryController() {
    var self = this;
    self.ApplicationUser = {};
    self.currentYear = new Date().getFullYear();
    self.startYear = self.currentYear - 20;
    self.init = function () {

        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        var dropdownMonths = $("#dropdownMonths");
        var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        months.forEach(function (month) {
            dropdownMonths.append($('<option>', {
                value: month,
                text: month
            }));
        });

        var dropdownYears = $("#dropdownYears");
        for (var year = self.currentYear; year >= self.startYear; year--) {
            dropdownYears.append($('<option>', {
                value: year,
                text: year
            }));
        }

        var dropdownLocation = $("#dropdownLocation");
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
        $("#LopDays").val(0);
        $('#LopDays').prop('disabled', true);

        $("#WrkDays").val(31);
        $('#WrkDays').prop('disabled', true);

        $("#StdDays").val(31);
        $('#StdDays').prop('disabled', true);

        var monthlySalaryGrid = $('#MonthlySalaryGrid').DataTable({
            ajax: {
                url: '/MonthlySalary/fetchAllMonthlySalaries',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'Title' },
                { data: 'SalaryMonth' },
                { data: 'SalaryYear' },
                { data: 'Location' },
                { data: 'StdDays' },
                { data: 'WrkDays' },
                { data: 'LopDays' }
            ],
            responsive: false,
            serverSide: false,
            "order": [[0, "asc"]],
            "pageLength": 20
        });
        $('#PublishMontlySalaryModal').modal({ backdrop: 'static', keyboard: false });
        $('#toggleDrawer').click(function () {
            $('#PublishMontlySalaryModal').modal('show');
        });
        $('#closeDrawer').click(function () {
            $('#drawer').removeClass('show');
        });
        $(document).on("change", "#dropdownMonths", function () {
            var year = $("#dropdownYears").val();
            var selectedMonth = $(this).val();
            var selectedMonthIndex = months.indexOf(selectedMonth);
            if (selectedMonthIndex !== -1) {
                var date = new Date(year, selectedMonthIndex, 1);
                var numberOfDays = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
                $("#WrkDays,#StdDays").val(numberOfDays);
                console.log("Number of days in the selected month: " + numberOfDays);
            } else {
                console.error("Invalid month selected: " + selectedMonth);
            }
        });
        $(document).on("change", "#dropdownYears", function () {
            var month = $("#dropdownMonths").val();
            var selectedYear = $(this).val();
            var selectedMonthIndex = months.indexOf(month);
            if (selectedMonthIndex !== -1) {
                var date = new Date(selectedYear, selectedMonthIndex, 1);
                var numberOfDays = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
                $("#WrkDays,#StdDays").val(numberOfDays);
                console.log("Number of days in the selected month: " + numberOfDays);
            } else {
                console.error("Invalid month selected: " + selectedMonth);
            }
        });


        $(document).on("click", "#PublishMonthlySalary", function () {
            $(".se-pre-con").show();
            var Title = $("#Title").val();
            var WrkDays = $("#WrkDays").val();
            var StdDays = $("#StdDays").val();
            var location = $("#dropdownLocation").val();
            var LopDays = $("#LopDays").val();
            var year = $("#dropdownYears").val();
            var month = $("#dropdownMonths").val();

            var salary = {
                MonthlySalaryId: 0, 
                Title: Title,
                SalaryMonth: month,
                SalaryYear: year,
                Location: location,
                StdDays: StdDays,
                WrkDays: WrkDays,
                LopDays: LopDays,
                CreatedOn: new Date(),
                CreatedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                IsActive: true
            };
            $.ajax({
                url: '/MonthlySalary/PublishMonthlySalary',
                data: JSON.stringify(salary),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#PublishMontlySalaryModal').modal('hide');
                    $("#PublishMontlySalaryForm")[0].reset(); 
                    monthlySalaryGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });
    };

}