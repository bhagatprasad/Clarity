function AccountController() {
    var self = this;
    self.init = function () {
        var form = $('#formAuthentication');
        var signUpButton = $('#bthLogin');

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

                        var redirectUrl = "/Employee/Index";

                      
                        $(".se-pre-con").hide();

                        window.location.href = redirectUrl;
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });

        });

    };
}