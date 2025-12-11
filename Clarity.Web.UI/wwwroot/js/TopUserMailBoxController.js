function TopUserMailBoxController() {
    var self = this;
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        $.ajax({
            url: '/UserMailBox/GetAllUserMailBoxes',
            data: { userId: appuser.Id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: true,
            cache: false,
            success: function (response) {
                console.log(response);
                var firstFiveRecords = response.data.slice(0, 5);
                firstFiveRecords.forEach(item => {
                    $('#notifications-container').append(createNotificationItem(item));
                });
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    }
    function createNotificationItem(data) {
        return `
             <div class="dropdown-divider"></div>
              <a class="dropdown-item preview-item">
                <div class="preview-thumbnail">
                    <img src="/assets/images/faces/face4.jpg" alt="image" class="profile-pic">
                </div>
                <div class="preview-item-content d-flex align-items-start flex-column justify-content-center">
                    <h6 class="preview-subject ellipsis mb-1 font-weight-normal">${data.mailBox.Subject}</h6>
                    <p class="text-gray mb-0">${getTimeAgo(data.CreatedOn)}</p>
                </div>
            </a>
            <div class="dropdown-divider"></div>
        `;
    }
    $(document).on("click", "#gotoMailBox", function () {
        window.location.href = '/UserMailBox/Index';
    })
    function getTimeAgo(dateTimeOffset) {
        // Parse the DateTimeOffset string
        const parsedDate = new Date(dateTimeOffset);

        // Get the current time
        const now = new Date();

        // Calculate the difference in milliseconds
        const diffMilliseconds = now - parsedDate;

        // Convert milliseconds to minutes and hours
        const diffMinutes = Math.floor(diffMilliseconds / 60000);
        const diffHours = Math.floor(diffMinutes / 60);

        // Determine the appropriate display string
        if (diffMinutes < 60) {
            return `${diffMinutes} Minutes ago`;
        } else {
            return `${diffHours} Hours ago`;
        }
    }
}