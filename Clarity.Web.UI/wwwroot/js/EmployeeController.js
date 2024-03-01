function EmployeeController() {
    var self = this;
    self.roles = [];
    self.departments = [];
    self.designation = [];
    self.employees = [];
    var requests = [];
    var actions = [];
    actions.push(serviceUrls.getRoles);
    actions.push(serviceUrls.getDesignations);
    actions.push(serviceUrls.getDepartments);
    actions.push(serviceUrls.getCountries);
    actions.push(serviceUrls.getStates);
    actions.push(serviceUrls.getCities);
    actions.push(serviceUrls.getUsers);
    self.init = function () {
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);}).fail(function () {
            console.log('One or more requests failed.');
        });
    };

}