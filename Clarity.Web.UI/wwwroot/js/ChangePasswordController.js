function ChangePasswordController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        var form = $('#ChangePasswordForm');
        var signUpButton = $('#btnChangePassword');
        $('#ChangePasswordModal').modal({ backdrop: 'static', keyboard: false });
        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();
        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $(document).on("click", "#btnChangePassword", function () {
            var newPassword = $("#newPassword").val();
            var confirmPassword = $("#confirmPassword").val();
            var changePassword = {
                UserId: self.ApplicationUser.Id,
                NewPassword: newPassword,
                ConfirmPassword: confirmPassword
            };
            $.ajax({
                url: '/Password/ChangePassword',
                type: 'POST',
                data: JSON.stringify(changePassword),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (responce) {
                    self.userResponceData = {};
                    $("#newPassword").val("");
                    $("#confirmPassword").val("");
                    $('#ChangePasswordModal').modal("hide");
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
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