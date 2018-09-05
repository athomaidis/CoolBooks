
// Client Side Validation
$(document).ready(function () {
    $(".authorForm").validate({
        rules: {
            FirstName: {
                required: true,
                minlength: 3,
                maxlength: 15
            },
            LastName: {
                required: true,
                minlength: 3,
                maxlength: 15
            }
        },
        messages: {
            FirstName: {
                required: "Enter your first name.",
                minlength: "Your first name sould be at least 3 characters",
                maxlength: "Your first name sould not be greater than 15 characters"
            },
            LastName: {
                required: "Enter your last name.",
                minlength: "Your last name sould be at least 3 characters",
                maxlength: "Your last name sould not be greater than 15 characters"
            }
        },
        highlight: function (element) {
            $(element).parent().prev('span').addClass("error");

        },
        unhighlight: function (element) {
            $(element).parent().prev('span').removeClass("error");
        }
    });

    // Restrict the user can not enter number
    $(".FirstName, .LastName").keypress(function (e) {
        var key = e.keyCode;
        if (key >= 48 && key <= 57) {
            e.preventDefault();
        }
    });

});

