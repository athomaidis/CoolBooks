$(document).ready(function () {

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-z]+$/i.test(value);
    }, "Letters only please"); 

    // Custom validation for genre names
    $('#RegisterForm').validate({
        rules: {
            FirstName: {
                required: true,
                minlength: 2,
                maxlength: 20,
                lettersonly: true
            },

            LastName: {
                required: true,
                minlength: 2,
                maxlength: 20,
                lettersonly: true
            },

            Email: {
                required: true,
                email: true
            },

            Password: {
                required: true,
                minlength: 6

            },

            ConfirmPassword: {
                required: true,
                minlength: 6,
                equalTo: "#Password"
            }
        },

        messages: {
            FirstName: {
                required: "Please enter your first name!",
                minlength: "Names cannot be shorter than 2 characters!",
                maxlength: "Names cannot be longer than 25 characthers!",
                lettersonly: "Names can only include letters!"

            },

            LastName: {
                required: "Please enter your first name!",
                minlength: "Last names cannot be shorter than 2 characters!",
                maxlength: "Last names cannot be longer than 25 characthers!",
                lettersonly: "Last names can only include letters!"

            },

            Email: {
                required: "Please enter a valid Email address!",
            },

            Password: {
                required: "Please enter a password!",
                minlength: "Passwords cannot be shorter than 6 characters",
            },

            ConfirmPassword: {
                required: "Please confirm your password!",
                minlength: "Passwords cannot be shorter than 6 characters",
                equalTo: "Passwords should match!"
            }
        }
    });

});
