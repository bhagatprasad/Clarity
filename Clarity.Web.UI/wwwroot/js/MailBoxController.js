function MailBoxController() {
    var self = this;
    self.ApplicationUser = {};
    self.MessageTypes = [];
    var requests = [];
    var actions = [];
    actions.push('/MessageType/GetAllMessageType');
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $('#AddMailModal').modal({ backdrop: 'static', keyboard: false });

        var form = $('#AddMailForm');
        var signUpButton = $('#ProcesssMail');
        form.on('input', 'input, select, textarea', checkFormValidity);

        checkFormValidity();

        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }


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
                {
                    data: 'MessageTypeId',
                    render: function (data, type, row) {

                        return row.messageType.Name;

                    }
                },
                {
                    data: 'FromUser',
                    render: function (data, type, row) {

                        if (row.IsForAll) {

                            return row.FromUser;

                        }
                        else {
                            var fromAndToUser = row.FromUser + "/" + row.ToUser;

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
        $(document).on("click", "#AddMail", function () {
            $('#AddMailModal').modal('show');
        });
        $(document).on("change", "#IsForAll", function () {
            if (this.checked) {
                $("#fromAndToUserSpecific").addClass("isForAll");
            } else {
                $("#fromAndToUserSpecific").removeClass("isForAll");
            }
        })
        for (var i = 0; i < actions.length; i++) {
            var ajaxConfig = {
                url: actions[i],
                method: 'GET',
            };
            requests.push($.ajax(ajaxConfig));
        }
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);
            self.MessageTypes = responses && responses[0].data ? responses[0].data : [];
            self.loadMessageTypesDropdown(self.MessageTypes);
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        $(document).on("click", "#ProcesssMail", function () {
            var messageType = $("#dropdownMessageTypes").val();
            var title = $("#Title").val();
            var subject = $("#Subject").val();
            var message = $("#Message").val();
            var htmlMessage = $("#HTMLMessage").val();
            var description = $("#Description").val();
            var isForAll = $("#IsForAll").val();
            var fromUser = $("#FromUser").val();
            var toUser = $("#ToUser").val();
            var mailBoxId = $("#MailBoxId").val();
            var forAll = isForAll === "on" ? true : false;

            var mailBox = {
                MailBoxId: mailBoxId ? parseInt(mailBoxId) : 0,
                MessageTypeId: parseInt(messageType),
                Title: title,
                Subject: subject,
                Description: description,
                Message: message,
                HTMLMessage: htmlMessage,
                IsForAll: forAll,
                FromUser: forAll ? "" : fromUser,
                ToUser: forAll ? "" : toUser,
                CreatedBy: self.ApplicationUser.Id,
                CreatedOn: new Date(),
                ModifiedBy: self.ApplicationUser.Id,
                ModifiedOn: new Date(),
                IsActive: true,
                messageType: {
                    Id: 0,
                    Name: '',
                    CreatedBy: 0,
                    CreatedOn: new Date(),
                    ModifiedBy: 0,
                    ModifiedOn: new Date(),
                    IsActive: false,
                }
            };
            $.ajax({
                url: '/MailBox/InsertMailMessage',
                data: JSON.stringify(mailBox),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#AddMailModal').modal('hide');
                    $("#AddMailForm")[0].reset();
                    mailboxGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });
        $('.datatable tbody td').each(function (index) {
            $this = $(this);
            var titleVal = $this.text();
            if (typeof titleVal === "string" && titleVal !== '') {
                $this.attr('title', titleVal);
            }
        });
    }
    self.loadMessageTypesDropdown = function (response) {
        var $dropdown = $('#dropdownMessageTypes');
        $dropdown.empty();

        var $defaultOption = $('<option>', {
            value: '',
            text: 'Select an Message Type'
        });
        $dropdown.append($defaultOption);

        response.forEach(function (item) {
            var $option = $('<option>', {
                value: item.Id,
                text: item.Name
            });
            $dropdown.append($option);
        });
        $dropdown.dropdown();
    };
}