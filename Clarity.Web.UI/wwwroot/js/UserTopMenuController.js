function UserTopMenuController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $("#employeeFullName").text(self.ApplicationUser.FirstName);
    };
}