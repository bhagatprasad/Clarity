function MessageTypeController() {

    var self = this;

    self.init = function () {
        console.log("welocme to message type jsvascript controller");
        var messageTypeGrid = $('#MessageTypeGrid').DataTable({
            ajax: {
                url: '/MessageType/GetAllMessageType',
                type: 'GET',
                dataSrc: 'data'
            },
            responsive: false,
            serverSide: false,
            paging: false,
            scrollY: '500px',
            columns: [
                { data: 'Id' },
                { data: 'Name' },
                { data: 'CreatedOn' },
                { data: 'ModifiedOn' },
                { data: 'IsActive' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<i class="fas fa fa-calendar icon-padding-right"  style="font-size:20px;color:green;padding-right:10px;" data-id="' + row.Id + '" ></i>' +
                            '<i class="fas fa fa-trash icon-padding-right"  style="font-size:20px;color:red" data-id="' + row.Id + '" ></i>';
                    }
                }
            ],
        });
    }
}