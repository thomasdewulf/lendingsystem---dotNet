var view = {
    init: function() {
        $("#zoekenAjax").keyup(function() {
            $.post(this.action, $(this).serialize(),
                function(data) {
                    $("#producten").html(data);
                });
        });
    },
    checkbox: function() {
        $(".checkboxJS").click(function () {
           
            
                    $.post(this.action, $(this).serialize(),
                        function(data) {
                            $("#producten").html(data);

                        });
            
        });
    }
}
$(function () {
    view.init();
    view.checkbox();
});