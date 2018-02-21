$(document).ready(function () {
    //Verifica qual radio button está selecionado (de acordo com o tipo da pessoa)
    //Depois dispara o evento click do radio button em questão, para que só sejam exibidos os campos relacionados
    if ($('#radioF :radio:checked').val()) {
        $("#radioF").click();
    }
    else if ($('#radioJ :radio:checked').val()) {
        $("#radioJ").click();
    }
});