function AccountController() {
    var self = this;
    self.userResponceData = {};
    self.init = function () {
        var form = $('#formAuthentication');
        var signUpButton = $('#bthLogin');
        $('#ForgotPasswordModal').modal({ backdrop: 'static', keyboard: false });
        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();

        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
        $(document).on("click", "#bthLogin", function () {
            $(".se-pre-con").show();
            var email = $("#username").val();
            var password = $("#password").val();
            var model = {
                Username: email,
                Password: password
            };
            $.ajax({
                url: '/Account/Login',
                data: JSON.stringify(model),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {

                    if (response.status) {

                        var appUserInfo = storageService.get('ApplicationUser');
                        if (appUserInfo) {
                            storageService.remove('ApplicationUser');
                        }

                        var applicationUser = response.appUser;

                        storageService.set('ApplicationUser', applicationUser);

                        var redirectUrl = "";

                        if (applicationUser.RoleId === 1000)
                            redirectUrl = "/Employee/Index"
                        else
                            redirectUrl = "/UserDashBoard/Index";

                        $(".se-pre-con").hide();

                        window.location.href = redirectUrl;
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error);
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

        $(document).on("click", "#forgotPasswordPopup", function () {
            $('#ForgotPasswordModal').modal("show");
        });

        $(document).on("click", "#btnForgotPassword", function () {
            var email = $("#email").val();
            var phone = $("#phone").val();
            $.ajax({
                url: '/Password/ForgotPassword',
                type: 'GET',
                data: { email: JSON.stringify(email), phone: JSON.stringify(phone) },
                success: function (responce) {
                    self.userResponceData = responce && responce.data ? responce.data : {};
                    $('#ForgotPasswordModal').modal("hide");
                    $('#ResetpasswordModal').modal("show");
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });
        $(document).on("click", "#btnResetPassword", function () {
            var newPassword = $("#newPassword").val();
            var confirmPassword = $("#confirmPassword").val();
            var resetPassword = {
                UserId: self.userResponceData.Id,
                NewPassword: newPassword,
                ConfirmPassword: confirmPassword
            };
            $.ajax({
                url: '/Password/ForgotPassword',
                type: 'GET',
                data: JSON.stringify(resetPassword),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (responce) {
                    self.userResponceData = {};
                    $('#ForgotPasswordModal').modal("hide");
                    $('#ResetpasswordModal').modal("hide");
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });
    };
}