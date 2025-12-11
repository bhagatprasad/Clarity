function UserMailBoxController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        var serviceURL = '/UserMailBox/GetAllUserMailBoxes?userId=' + appuser.Id;
        var mailboxGrid = $('#MailBoxGrid').DataTable({
            responsive: false,
            serverSide: false,
            ajax: {
                url: serviceURL,
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                {
                    className: 'dt-control',
                    orderable: false,
                    data: null,
                    defaultContent: ''
                },
                {
                    data: 'MailBoxId',
                    render: function (data, type, row) {

                        return row.mailBox.Title;

                    }
                },
                {
                    data: 'MailBoxId',
                    render: function (data, type, row) {

                        return row.mailBox.Subject;

                    }
                },
                {
                    data: 'MailBoxId',
                    render: function (data, type, row) {

                        return row.mailBox.Message;

                    }
                },
                {
                    data: 'MailBoxId',
                    render: function (data, type, row) {

                        return row.mailBox.messageType.Name;

                    }
                },
                { data: 'IsRead' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-calendar icon-padding-right"  style="font-size:20px;color:green;padding-right:10px;" data-id="' + row.UserMailBoxId + '" ></i>' +
                            '<i class="fas fa fa-trash icon-padding-right"  style="font-size:20px;color:red" data-id="' + row.UserMailBoxId + '" ></i>';
                    }
                }
            ],
            paging: false,
            scrollY: '500px'
        });

        mailboxGrid.on('click', 'td.dt-control', function (e) {
            let tr = e.target.closest('tr');
            let row = mailboxGrid.row(tr);
            let dataItem = mailboxGrid.row(row).data();
            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
            }
            else {
                // Open this row
                row.child(format(dataItem)).show();
            }
           
        });
        function format(response) {
            // `d` is the original data object for the row

            return (
                '<div class="row">' +
                '<div class="col-12">' +
                '<div class="card"  style="width:100%;margin-top: 10px;padding: 5px; ">' +
                '<h4 class="card-title">Subject :' + response.mailBox.Subject+'</h4>' +
                '<ul class="list-group list-group-flush">' +
                '<li class="list-group-item"><text style="float: left;">Title : ' + response.mailBox.Title + '</text></li>' +
                '<li class="list-group-item"><text style="float: left;">Message Tyle : ' + response.mailBox.messageType.Name + '</text></li>' +
                '<li class="list-group-item"><text class="message" style="float: left;">Message: ' + response.mailBox.Message + '</text></li>' +
                '</ul>' +
                '</div>' +
                '</div>' +
                '</div>'
            );
        }
    }
}