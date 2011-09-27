/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery-ui-1.8.16.js" />

var Grid = {
    UrlController: '',
    TableId: '',
    SeletorTabela: '#grid',

    Iniciar: function (urlController) {
        var that = this;
        that.UrlController = urlController;
    },

    CarregarLista: function () {
        var that = this;
        $.ajax({
            type: 'GET',
            url: that.UrlController + "/Lista",
            cache: false,
            dataType: 'json',
            success: function (data) {
                $(that.SeletorTabela + ' tbody').empty();
                $.each(data.Lista, function (i, item) {
                    var css = (i % 2 == 0) ? "grid_cte_list_line1" : "grid_cte_list_line2";
                    var linhas = that.CriarLinha(item, css);
                    if ($.isArray(linhas))
                        $.each(linhas, function (i, linha) {
                            tbody.append(linha);
                        });
                    else
                        tbody.append(linhas);
                });
            }
        });
    },

    CriarLinha: function () {
    }

}