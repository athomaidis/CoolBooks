
    $(document).ready(function () {


        var maxLength = 2000;

        // When in edit change counter to remaining characters with existing text
        var len = $('#Description').val().length;
        len = maxLength - len
        $('#chars').text(len);
        

        // Remaining characters counter for description
        $('#Description').keyup(function () {
            var length = $(this).val().length;
            var length = maxLength - length;
            $('#chars').text(length);
        });

        // Clear fields on edit page when clear is clicked
        $('#ClearFieldsEdit').on('click', function () {
            $('#Description').text('');
            $('#Name').val("");
            
        });

        // Reset the character counter for description when clear clicked either on create or edit view
        $('.clearGenre').on('click', function () {
            $('#chars').text(maxLength);
           
        });

        // Custom validation for genre names
        $('#GenreForm').validate({
            rules: {
                Name: {
                    required: true,
                    minlength: 3,
                    maxlength: 20,
                    
                },

            },
            
            messages: {
                Name: {
                    required:   "Please add a genre name.",
                    minlength:  "Genre names cannot be shorter than 5 characters.",
                    maxlength:  "Genre names cannot be longer than 20 characthers.",

                },
              
            }
        });
   
});
