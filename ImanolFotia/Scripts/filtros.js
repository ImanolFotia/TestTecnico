//# sourceMappingURL=filtros.js.map

var listaUsers = ko.observableArray();
var ArmarTabla = function(){
    var self = this;
    self.usuarios = listaUsers;
    
    self.headers = [
        {title:'#',sortPropertyName:'Id', asc: true, active: false},
        {title:'Nombre',sortPropertyName:'Nombre', asc: true, active: false},
        {title:'Apellido',sortPropertyName:'Apellido', asc: true, active: false}
    ];
    self.filters = [
        {title:'Ingrese su busqueda...', filter: null},
        {title:'', filter: function(item){return item.Nombre.contains($('#txtFiltro').text) || item.Apellido.contains($('#txtFiltro').text) ||  item.Id.contains($('#txtFiltro').text);}},
    ];
    
    //Definir una funcion de ordenamiento por defecto
    self.activeSort = ko.observable(function(){return 0;});

    self.sort = function(header, event){
        //Invertir la direccion del ordenamiento si se clickea por segunda vez
        if(header.active) {
            header.asc = !header.asc; 
        }
        //Desactivar el resto de las cabezeras
        ko.utils.arrayForEach(self.headers, function(item){ item.active = false; } );

        //Activar la cabezera clickeada
        header.active = true;


        var prop = header.sortPropertyName;
        var asc = function(a,b){ return a[prop] < b[prop] ? -1 : a[prop] > b[prop] ? 1 : a[prop] == b[prop] ? 0 : 0; };
        var desc = function(a,b){ return a[prop] > b[prop] ? -1 : a[prop] < b[prop] ? 1 : a[prop] == b[prop] ? 0 : 0; };
        var sortFunc = header.asc ? asc : desc;
        
        console.log(sortFunc);

        self.activeSort(sortFunc);
    };
    
    self.activeFilter = ko.observable(self.filters[0].filter);//Definir una funcion de ordenamiento por defecto  
    self.setActiveFilter = function(model,event){
        self.activeFilter(model.filter);
    };
    
    self.obtenerFiltros = ko.computed(function(){
        var result;
        if(self.activeFilter()){
            result = ko.utils.arrayFilter(self.usuarios(), self.activeFilter());
        } else {
            result = self.usuarios();
        }
        return result.sort(self.activeSort());
    });
}
var $rows = $('table tr');
$('#txtFiltro').keyup(function() {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    $rows.show().filter(function() {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
});

ko.applyBindings(new ArmarTabla());