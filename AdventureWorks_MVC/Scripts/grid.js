/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery-ui-1.8.16.js" />

var objData = 0;

var Grid = {
    UrlController: '',
    TableId: '',
    SeletorTabela: '#grid',
    TableTemplate: '#tableTemplate',
    DialogId: '#dialog',
    Skip: 1,
    Top: 10,

    TemplatePaginacao: 0,

    Iniciar: function (urlController, IdBotaoCriarRegistro) {
        this.UrlController = urlController;
        if (IdBotaoCriarRegistro) {
            $(IdBotaoCriarRegistro).bind('click', this, Grid.CriarRegistro);
        }
    },

    CarregarLista: function (skip, top) {
        var parametros = {};
        if (skip) parametros.skip = skip;
        if (top) parametros.top = top;
        var that = this;
        $.ajax({
            type: 'GET',
            url: this.UrlController + "/Lista",
            data: parametros,
            cache: false,
            dataType: 'json',
            success: function (data) {
                objData = data;
                var tbody = $(that.SeletorTabela + ' tbody');
                tbody.empty();
                $(that.TableTemplate).tmpl(data.Lista).appendTo(tbody);
                that.MontarPaginacao(data.Pagina);
            }
        });
    },

    CriarLinha: function () {
    },

    MontarPaginacao: function (pagina) {
        Grid.Skip = pagina.NumeroDaPagina;
        Grid.Top = pagina.RegistrosPorPagina;
        pagina.NumeroDePaginas = Math.ceil(pagina.TotalDeRegistrosEncontrados / pagina.RegistrosPorPagina);
        var tfoot = $(this.SeletorTabela + ' tfoot tr th');
        tfoot.empty();

        if (!this.TemplatePaginacao) {
            var tmplPaginacao =
            '{{if NumeroDaPagina > 1}} <a href="javascript:Grid.CarregarLista(1)">Primeiro</a> | <a href="javascript:Grid.CarregarLista(${NumeroDaPagina-1})">Anterior</a> ' +
            '{{else}} Primeiro | Anterior {{/if}} ' +
            'Página ${NumeroDaPagina} de ${NumeroDePaginas}' +
            '{{if NumeroDaPagina < NumeroDePaginas}} <a href="javascript:Grid.CarregarLista(${NumeroDaPagina+1})">Próximo</a> | <a href="javascript:Grid.CarregarLista(${NumeroDePaginas})">Último</a> ' +
            '{{else}} Próximo | Último {{/if}} ';

            this.TemplatePaginacao = $.template(null, tmplPaginacao);
        }

        $.tmpl(this.TemplatePaginacao, pagina).appendTo(tfoot);
    },

    CriarRegistro: function (event) {
        var that = event.data;
        if (!$(that.DialogId).length) {
            $('body').append('<div id="' + that.DialogId.substr(1) + '"></div>');
            $(that.DialogId).dialog({ autoOpen: false });
        }
        $(that.DialogId).dialog({ title: "Criar Novo Registro", modal: true });

        $(that.DialogId).empty();
        $.ajax({
            type: 'GET',
            url: that.UrlController + "/Create",
            cache: true,
            dataType: 'html',
            success: function (data) {
                $(that.DialogId).append(data);
                $(that.DialogId).dialog('open');
            }
        });
    },

    Salvar: function (form) {
        var that = Grid;
        if ($(form).valid()) {
            $.ajax({
                type: 'POST',
                url: that.UrlController + "/Create",
                cache: true,
                data: $(form).serialize(),
                success: function (data) {
                    if (data.result) {
                        Grid.CarregarLista(Grid.Skip, Grid.Top);
                        $(Grid.DialogId).dialog('close');
                    }
                    else if (data.msg) {
                        alert(data.msg);
                    }
                }
            });
        }
    }

};