function TimesheetController() {
    var self = this;
    self.ApplicationUser = {};
    var requests = [];
    var actions = [];
    self.Employees = [];
    actions.push(serviceUrls.getUsers);
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
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
            if (responses[0] && responses[0].data) {
                responses[0].data.forEach(function (item) {
                    self.Employees.push(item.employee);
                });
            }
        }).fail(function () {
            console.log('One or more requests failed.');
        });
    };
}