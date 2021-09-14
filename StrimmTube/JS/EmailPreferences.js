(function () {

    var GetEmailPreferencsForUser = "/WebServices/UserService.asmx/GetUserEmailPreferences";
    var UpdateUserEmailPreferences = "/WebServices/UserService.asmx/UpdateUserEmailPreferences";

    var email;
    var id;
    var groups;

    var EmailPrefs = {
        init: function (userEmail, userId) {
            email = userEmail;
            id = userId;

            $('#chkGreetings').prop('checked', true);
            $('#chkReminders').prop('checked', true);
            $('#chkSocial').prop('checked', true);
            $('#chkMarketing').prop('checked', true);
            $('#chkNews').prop('checked', true);
            $('#chkAll').prop('checked', false);

            this.loadPreferences(id);
        },

        loadPreferences: function (userId) {
            $.ajax({
                type: "POST",
                url: GetEmailPreferencsForUser,
                cashe: false,
                data: '{"userId":' + userId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var emailGroups = response.d;
                    var count = 0;
                    void 0
                    if (emailGroups) {
                        $.each(emailGroups, function (i, c) {
                            switch (c.EmailOptoutGroupId) {
                                case 2:
                                    $('#chkGreetings').prop('checked', !c.Unsubscribed);
                                    break;
                                case 3:
                                    $('#chkReminders').prop('checked', !c.Unsubscribed);
                                    break;
                                case 4:
                                    $('#chkMarketing').prop('checked', !c.Unsubscribed);
                                    break;
                                case 5:
                                    $('#chkSocial').prop('checked', !c.Unsubscribed);
                                    break;
                                case 6:
                                    $('#chkNews').prop('checked', !c.Unsubscribed);
                                    break;
                                default:
                                    break;
                            };
                            if (c.Unsubscribed) {
                                count++;
                            }
                        });

                        if (count == 6) {
                            $('#chkAll').prop('checked', true);
                        }
                    }
                },
                complete: function (response) {
                }
            });

        },

        updateSelection: function () {
            var unsubscribeFromGreetings = $('#chkGreetings').is(':checked');
            var unsubscribeFromReminders = $('#chkReminders').is(':checked');
            var unsubscribeFromSocial = $('#chkSocial').is(':checked');
            var unsubscribeFromNews = $('#chkNews').is(':checked');
            var unsubscribeFromMarketing = $('#chkMarketing').is(':checked');

            $('#chkAll').prop('checked', (unsubscribeFromGreetings == false && unsubscribeFromReminders == false && unsubscribeFromSocial == false && unsubscribeFromNews == false && unsubscribeFromMarketing == false));
        },

        processFullUnsubscribe: function () {
            $('#chkGreetings').prop('checked', false);
            $('#chkReminders').prop('checked', false);
            $('#chkSocial').prop('checked', false);
            $('#chkMarketing').prop('checked', false);
            $('#chkNews').prop('checked', false);
            $('#chkAll').prop('checked', true);
        },

        saveEmailPreferences: function () {
            var unsubscribeFromGreetings = $('#chkGreetings').prop('checked');
            var unsubscribeFromReminders = $('#chkReminders').prop('checked');
            var unsubscribeFromSocial = $('#chkSocial').prop('checked');
            var unsubscribeFromNews = $('#chkNews').prop('checked');
            var unsubscribeFromMarketing = $('#chkMarketing').prop('checked');
            
            var completedUnsubscribe = $('#chkAll').is(':checked');
            var prefData = '{"userId":' + id + ',"uGreetings":' + unsubscribeFromGreetings + ',"uReminders":' + unsubscribeFromReminders + ',"uSocial":' + unsubscribeFromSocial + ',"uNews":' + unsubscribeFromNews + ',"uMarketing":' + unsubscribeFromMarketing + '}';
            void 0
            $.ajax({
                type: "POST",
                url: UpdateUserEmailPreferences,
                cashe: false,
                data: prefData,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var success = response.d.IsSuccess;
                    if (success) {
                        if (completedUnsubscribe == true) {
                            location.href = "/emails-full-unsubscribe";
                        }
                        else {
                            location.href = "/preferences-update-success";
                        }
                    }
                    else {
                        alertify.error(response.d.Message);
                    }
                },
                complete: function (response) {
                }
            });
        }
    };

    getPreferences = function () {
        return EmailPrefs;
    };

})();