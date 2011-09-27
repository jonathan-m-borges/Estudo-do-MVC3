
$(document).ready(function () {
    $('#idMarca').change(function () {
        $.ajax({
            url: "/Alugar/CarregarCarros",
            type: "GET",
            dataType: 'json',
            data: { idMarcas: $('#idMarca').val() },
            success: function (data) {
                $('#Carro').children().remove();
                $.each(data, function (index, carro) {
                    $('#Carro').append(new Option(carro.Modelo, carro.Id));
                });
            }
        });
    });
});

