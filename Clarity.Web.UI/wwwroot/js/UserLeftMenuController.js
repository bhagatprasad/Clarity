function UserLeftMenuController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        $('#ChangePasswordModal').modal({ backdrop: 'static', keyboard: false });
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $("#employeeFullName").text(self.ApplicationUser.FirstName);
        $(document).on("click", "#ChangePassword", function () {
            $("#ChangePasswordModal").modal("show");
        });
        $(document).on("click", ".toggle-password", function () {
            var inputField = $(this).closest('.input-group').find('.form-control');
            var icon = $(this).find('i');

            if (inputField.attr('type') === 'password') {
                inputField.attr('type', 'text');
                icon.removeClass('fa-eye-slash').addClass('fa-eye');
            } else {
                inputField.attr('type', 'password');
                icon.removeClass('fa-eye').addClass('fa-eye-slash');
            }
        });
    };
}