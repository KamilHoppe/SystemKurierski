
    function hideOnLoad() {
        $('#PaypalButton').hide();
        $('#EcheckForm').hide();
    }
$(document).ready(function () {
    hideOnLoad(); // add this line
    $('#PaymentId').change(function () {
        var value = $(this).val();
        if (value == '1') {
            $('#PaypalButton').show();
            $('#EcheckForm').hide();
        } else if (value == '2') {
            $('#PaypalButton').hide();
            $('#EcheckForm').show();
        } else {
            hideOnLoad();
        }
    });
});
