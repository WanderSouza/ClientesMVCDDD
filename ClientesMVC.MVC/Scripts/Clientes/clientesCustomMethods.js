//Executa as funções após os elementos da página sere carregados
$(document).ready(function () {
    mascaraCPFCNPJ();
    fisicaJurica();
    cepChange();
    dropdownUFChange();
    formSubmit();

    //Atribuindo máscaras para CPF e CNPJ
    function mascaraCPFCNPJ() {
        $('#CPF').mask('999.999.999-99', { placeholder: "___.___.___-__", autoclear: false });
        $('#CNPJ').mask('99.999.999/9999-99', { placeholder: "__.___.___/____-__", autoclear: false });
    }

    //Trata o form para exibir dados de pesso física ou jurídica de acordo com a seleção do radio button
    function fisicaJurica() {
        //Desabilita e oculta os atributos da pessoa jurídica
        $("#NomeFantasia").attr("disabled", true);
        $("#RazaoSocial").attr("disabled", true);
        $("#CNPJ").attr("disabled", true);
        $(".NomeFantasia").hide();
        $(".RazaoSocial").hide();
        $(".CNPJ").hide();        

        //Tratamento do radio button de pessoa física
        $("#radioF").on("click", function () {
            //Habilita e exibe os atributos da pessoa física
            $(".Nome").show();
            $(".CPF").show();
            $(".DataNasc").show();
            $(".Sobrenome").show();
            $("#Nome").attr("disabled", false);
            $("#CPF").attr("disabled", false);
            $("#DataNasc").attr("disabled", false);
            $("#Sobrenome").attr("disabled", false);

            //Desabilita e oculta os atributos da pessoa jurídica
            $("#NomeFantasia").attr("disabled", true);
            $("#RazaoSocial").attr("disabled", true);
            $("#CNPJ").attr("disabled", true);
            $(".NomeFantasia").hide();
            $(".RazaoSocial").hide();
            $(".CNPJ").hide();            
        });

        //Tratamento do radio button de pessoa jurídica
        $("#radioJ").on("click", function () {
            //Habilita e exibe os atributos da pessoa jurídica
            $(".NomeFantasia").show();
            $(".RazaoSocial").show();
            $(".CNPJ").show();
            $("#NomeFantasia").attr("disabled", false);
            $("#RazaoSocial").attr("disabled", false);
            $("#CNPJ").attr("disabled", false);

            //Desabilita e oculta os atributos da pessoa física
            $("#Nome").attr("disabled", true);
            $("#CPF").attr("disabled", true);
            $("#DataNasc").attr("disabled", true);
            $("#Sobrenome").attr("disabled", true);
            $(".Nome").hide();
            $(".CPF").hide();
            $(".DataNasc").hide();
            $(".Sobrenome").hide();            
        });
    }
    
    //Tratamento para o evento 'change' do input CEP
    function cepChange() {
        $("#CEP").change(function () {
            //Limpa os campos        
            $("#Bairro").empty();
            $("#Logradouro").empty();
            $("#Complemento").empty();
            //Faz um get para a URL relativa a api de ceps
            $.ajax({
                type: 'GET',
                url: 'https://viacep.com.br/ws/' + $("#CEP").val() + '/json/', //Passando o cep digitado como parâmetro
                dataType: 'json',
                //Se o get obteve sucesso, trata o retorno (json)
                success: function (data) {
                    //Seleciona o estado                    
                    selectOptionUF(data.uf);
                    //Chama o change para carregar as cidades
                    $("#UFID").change();
                    //Atribui os valores do json para os demais campos
                    $("#Bairro").val(data.bairro);
                    $("#Logradouro").val(data.logradouro);
                    $("#Complemento").val(data.complemento);
                },
                //Recarrega os dropdowns e faz o log do erro, caso ocorra
                error: function (ex) {
                    selectOptionUF("- Selecionar UF -");
                    $("#UFID").change();
                    console.log('Não foi possível recuperar os dados do cep informado: ' + ex);
                }
            })//Após a execução da promise, carrega a cidade relativa ao json
                .done(function (data) {                       
                    $("#CidadeID option:contains(" + data.localidade + ")").attr('selected', 'selected');
                });
            //Evita o comportamento padrão do browser
            return false;
        });
    }

    //Tratamento para o evento 'change' do dropdownlist de UFs
    function dropdownUFChange() {
        $("#UFID").change(function () {            
            //Remove os item do dropdownlist de Cidades
            $("#CidadeID").empty();
            //Faz um post para a URL relativa ao JsonResult 'getCities'
            $.ajax({
                type: 'POST',
                url: $("#getCitiesUrl").val(), //Campo oculto da view que contém a url
                dataType: 'json',
                data: { id: $("#UFID").val() }, //Id do UF a ser utilizado na pesquisa           
                //Se o post obteve sucesso, trata o retorno (json)
                success: function (cities) {
                    //Percorre cada item do json
                    $.each(cities, function (i, city) {
                        //Adiciona o item corrente ao dropdownlist
                        $("#CidadeID").append('<option value="' + city.Value + '">' + city.Text + '</option>');
                    });
                },
                //Faz o log do erro, caso ocorra
                error: function (ex) {
                    console.log('Erro ao efetuar a requisição "getCities"');
                }
            });
            //Evita o comportamento padrão do browser
            return false;
        });
    }

    //Trata o submit, para que caso o model não esteja válido, não cause um refresh do browser
    function formSubmit() {
        $('#frmCliente').submit(function () {
            $(this).validate();

            if (!$(this).valid()) {
                return false;
            }
        });
    }

    //Seleciona o item passado como parâmetro no dropdownlist UF
    function selectOptionUF(option) {
        $("#UFID option").each(function () {
            this.selected = $(this).text() === option;
        });
    }
    
});