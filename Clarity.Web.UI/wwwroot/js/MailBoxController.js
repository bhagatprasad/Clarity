function MailBoxController() {
    var self = this;

    self.ApplicationUser = {};
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }

        console.log("MailBoxController..." + JSON.stringify(self.ApplicationUser));

        var mailboxGrid = $('#MailBoxGrid').DataTable({
            ajax: {
                url: '/MailBox/fetchAllMailBoxes',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'MailBoxId' },
                { data: 'Title' },
                { data: 'Subject' },
                { data: 'Message' },
                { data: 'MessageTypeId' },
                {
                    data: 'FromUser',
                    render: function (data, type, row) {

                        if (data.IsForAll) {

                            return data.FromUser;

                        }
                        else {
                            var fromAndToUser = data.FromUser + "/" + data.ToUser;

                            return fromAndToUser;
                        }

                    }
                },
                { data: 'IsForAll' },

                { data: 'IsActive' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-calendar icon-padding-right"  style="font-size:20px;color:green;padding-right:10px;" data-id="' + row.MailBoxId + '" ></i>' +
                            '<i class="fas fa fa-trash icon-padding-right"  style="font-size:20px;color:red" data-id="' + row.MailBoxId + '" ></i>';
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '500px'
        });
    }
}